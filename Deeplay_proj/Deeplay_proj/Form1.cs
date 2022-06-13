using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Deeplay_proj
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        //int selectedRow;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Событие подключения при загрузке формы

            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBdp"].ConnectionString); //хранение в appconfig
            sqlConnection.Open();
            if (sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключено!");
            }
            else
                MessageBox.Show("Онет! Я потерял базу данных");

            //запрос вывода таблиц в DataGridView через DataAdapter

            //Таблица меню работников

            SqlDataAdapter adapterMain = new SqlDataAdapter
                ("SELECT [emp_id] as 'Номер_Работника' ," +
                "[first_name] as 'Имя'," +
                "[last_name] as 'Фамилия'," +
                "[gender] as 'Пол'," +
                "[birthday] as 'День_рождения'," +
                "[phone] as 'Телефон'," +
                "[post_id] as 'Номер_Должности' FROM employees",
                sqlConnection);
            DataSet dbmain = new DataSet();
            adapterMain.Fill(dbmain);
            dataGridView6.DataSource = dbmain.Tables[0];

            //Таблица Распредления работников

            SqlDataAdapter adapterDepart1 = new SqlDataAdapter
               ("SELECT [emp_id] as 'Номер_Работника',[dept_id] as 'Номер Отдела' FROM P_emp", sqlConnection);
            DataSet db_depart1 = new DataSet();
            adapterDepart1.Fill(db_depart1);
            dataGridView9.DataSource = db_depart1.Tables[0];

            //Таблица Распределения контролёров

            SqlDataAdapter adapterDepart2 = new SqlDataAdapter
               ("SELECT [emp_id] as 'Номер_Работника',[dept_id] as 'Номер Отдела' FROM P_ctrl", sqlConnection);
            DataSet db_depart2 = new DataSet();
            adapterDepart2.Fill(db_depart2);
            dataGridView8.DataSource = db_depart2.Tables[0];

            //Taблица распределения менеджеров

            SqlDataAdapter adapterDepart3 = new SqlDataAdapter
               ("SELECT [emp_id] as 'Номер_Работника',[dept_id] as 'Номер Отдела' FROM P_manager", sqlConnection);
            DataSet db_depart3 = new DataSet();
            adapterDepart3.Fill(db_depart3);
            dataGridView7.DataSource = db_depart3.Tables[0];

        }

        //фильтр фамилии по строке
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            //DataSourse через метод DataTable
            (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Фамилия LIKE '%{textBox1.Text}%'"; 
        }

        //фильтр по отделу
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Номер_Должности = 1";

                    break;

                case 1:
                    (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Номер_Должности = 2";

                    break;

                case 2:
                    (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Номер_Должности = 3";

                    break;

                case 3:
                    (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Номер_Должности = 4";

                    break;

            }

        }

        //отчистка от фильров
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = "";
        }

        //фильтр по должностям
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Номер_Должности = 1";

                    break;

                case 1:
                    (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Номер_Должности = 2";

                    break;

                case 2:
                    (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Номер_Должности = 3";

                    break;

                case 3:
                    (dataGridView6.DataSource as DataTable).DefaultView.RowFilter = $"Номер_Должности = 4";

                    break;

            }
        }

        //переход на окно cоздания нового рабочего
        private void button5_Click(object sender, EventArgs e)
        {
            INSERT_EMP worker_insert = new INSERT_EMP();

            worker_insert.Show();

        }

        //переход на окно cоздания нового контроёлра
        private void button2_Click(object sender, EventArgs e)
        {
            INSERT_Ctrl Ctrl_insert = new INSERT_Ctrl();

            Ctrl_insert.Show();
        }

        //переход на окно cоздания нового менеджера
        private void button3_Click(object sender, EventArgs e)
        {
            INSERT_Manager Ctrl_manager = new INSERT_Manager();

            Ctrl_manager.Show();
        }

        //переход на окно cоздания нового директора
        private void button4_Click(object sender, EventArgs e)
        {
            INSERT_dir Ctrl_dir = new INSERT_dir();

            Ctrl_dir.Show();
        }

        //пуреход на окно смены должности
        private void button1_Click(object sender, EventArgs e)
        {
            Post_Change post_Change = new Post_Change();

            post_Change.Show();

        }

        //удаление сотрудника
        private void button6_Click(object sender, EventArgs e)
        {
            Delete_Stuff del_stuff = new Delete_Stuff();

            del_stuff.Show();
        }

        //окно смены личной информации
        private void button7_Click(object sender, EventArgs e)
        {
            Change_emp_info change_Emp_Info = new Change_emp_info();

            change_Emp_Info.Show();
        }

        //доп.инф - фио главы отдела (рабочий)
        private void button8_Click(object sender, EventArgs e)
        {
            //с помощью ID рабочего определяется ID отдела, в котором он работает, а затем ID начальника, что привзян к отделу
            SqlCommand FindeManager = new SqlCommand(
                $"SELECT (last_name) FROM employees WHERE [emp_id] = (SELECT (emp_id) FROM P_manager WHERE (dept_id = (SELECT (dept_id) FROM P_emp WHERE (emp_id = '{textBox2.Text}') ) ))", sqlConnection);
            SqlDataReader DR1 = FindeManager.ExecuteReader();
            
            while (DR1.Read())
            {
                textBox3.Text = DR1.GetValue(0).ToString();
            }
            DR1.Close();
            
        }
        //доп.инф - полномоичия (контролёр)
        private void button9_Click(object sender, EventArgs e)
        {
            SqlCommand FindeInspection = new SqlCommand(
                $"SELECT inspect FROM P_ctrl WHERE emp_id = '{textBox5.Text}'",sqlConnection);
            SqlDataReader DR2 = FindeInspection.ExecuteReader();

            while (DR2.Read())
            {
                textBox4.Text = DR2.GetValue(0).ToString();
            }
            DR2.Close();

        }

        //доп инф - отдел (руководитель)
        private void button10_Click(object sender, EventArgs e)
        {

           //определяется к какому номеру отдела привязан менеджер, а далее происходит обращене к таблице хранения имён отделов посредством id отдела
            SqlCommand FindeDepart = new SqlCommand(
                $"SELECT dept_name FROM Departments WHERE dept_id = (SELECT (dept_id) FROM P_emp WHERE emp_id = '{textBox7.Text}' )", sqlConnection);
            SqlDataReader DR3 = FindeDepart.ExecuteReader();

            while (DR3.Read())
            {
                textBox6.Text = DR3.GetValue(0).ToString();
            }
            DR3.Close();

        }

        //доп инфа - всего сотрудников (директор)
        private void button11_Click(object sender, EventArgs e)
        {
            SqlCommand CountEmp = new SqlCommand(
                $"SELECT COUNT(*) FROM employees", sqlConnection);
            SqlDataReader DR4 = CountEmp.ExecuteReader();

            while (DR4.Read())
            {
                textBox8.Text = DR4.GetValue(0).ToString();
            }
            DR4.Close();
        }

        /* private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView6.Rows[selectedRow];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();
            }
        }*/
    }
}

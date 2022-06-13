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
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Delected
    }
    public partial class Change_emp_info : Form
    {
        private SqlConnection sqlConnection = null;
        int selectedRow;
        public Change_emp_info()
        {
            InitializeComponent();
        }

        private void Change_emp_info_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBdp"].ConnectionString); //хранение в appconfig
            sqlConnection.Open();

            //подключение dataGridView 
            SqlDataAdapter adapterMain = new SqlDataAdapter
                ("SELECT [emp_id] as 'Номер_Работника' ," +
                "[first_name] as 'Имя'," +
                "[last_name] as 'Фамилия'," +
                "[gender] as 'Пол'," +
                "[birthday] as 'День_рождения'," +
                "[phone] as 'Телефон'," +
                "[post_id]" +
                "FROM employees",
                sqlConnection);
            DataSet dbChange = new DataSet();
            adapterMain.Fill(dbChange);
            dataGridView1.DataSource = dbChange.Tables[0];
            this.dataGridView1.Columns["post_id"].Visible = false;
        }

        //отображение элементов в textbox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox6.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                textBox4.Text = row.Cells[4].Value.ToString();
                textBox5.Text = row.Cells[5].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ввод личных данных сотрудника в employees

            SqlCommand Updatecommand1 = new SqlCommand(
              $"Update [employees] " +
              $"SET first_name = @first_name, last_name = @last_name, gender = @gender, birthday = @birthday, phone = @phone " +
              $"WHERE emp_id = '{textBox6.Text}' ",
                sqlConnection);
            try
            {
                DateTime date = DateTime.Parse(textBox4.Text);

                //cвязка ключей с данными ввода в табилце emloyeers
                Updatecommand1.Parameters.AddWithValue("first_name", textBox1.Text);
                Updatecommand1.Parameters.AddWithValue("last_name", textBox2.Text);
                Updatecommand1.Parameters.AddWithValue("gender", textBox3.Text);
                Updatecommand1.Parameters.AddWithValue("birthday", $"{date.Month}.{date.Day}.{date.Year}");
                Updatecommand1.Parameters.AddWithValue("phone", textBox5.Text);

                MessageBox.Show("Данные введены.", Updatecommand1.ExecuteNonQuery().ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}

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
    public partial class INSERT_Manager : Form
    {
        private SqlConnection sqlConnection = null;
        public INSERT_Manager()
        {
            InitializeComponent();
        }

        private void INSERT_Manager_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBdp"].ConnectionString); //хранение в appconfig
            sqlConnection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ввод личных данных сотрудника в employees
            SqlCommand INSERTcommand1 = new SqlCommand(
                $"INSERT INTO [employees] (first_name, last_name, gender, birthday, phone, post_id) " +
                $"Values (@first_name, @last_name, @gender, @birthday, @phone, '3')",
                sqlConnection);

            //ввод ID сотрудника и ID отдела в p_emp
            SqlCommand INSERTcommand2 = new SqlCommand(
                $"INSERT INTO [P_manager] (emp_id) " +
                $"SELECT[emp_id] FROM[employees]" +
                $"WHERE[emp_id] = (SELECT MAX([emp_id]) FROM[employees]) " +
                $"UPDATE P_manager " +
                $"SET dept_id = '{textBox6.Text}' " +
                $"WHERE[emp_id] = (SELECT MAX([emp_id]) FROM[employees])", sqlConnection);

            //приведение строки к типу даты
            DateTime date = DateTime.Parse(textBox4.Text);
            try
            {
                //cвязка ключей с данными ввода в табилце emloyeers
                INSERTcommand1.Parameters.AddWithValue("first_name", textBox1.Text);
                INSERTcommand1.Parameters.AddWithValue("last_name", textBox2.Text);
                INSERTcommand1.Parameters.AddWithValue("gender", textBox3.Text);
                INSERTcommand1.Parameters.AddWithValue("birthday", $"{date.Month}.{date.Day}.{date.Year}");
                INSERTcommand1.Parameters.AddWithValue("phone", textBox5.Text);

                //ввод в p_emp
                INSERTcommand2.ExecuteNonQuery();

                MessageBox.Show("Руководитель отдела Добавлен", INSERTcommand1.ExecuteNonQuery().ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }


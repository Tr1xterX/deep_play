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
    public partial class Delete_Stuff : Form
    {
        private SqlConnection sqlConnection = null;
        public Delete_Stuff()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
            //удаление из всех таблиц сотрудника с подобныым ID
            SqlCommand DelComand = new SqlCommand(
            $"Delete FROM employees where emp_id = '{textBox1.Text}' " +
            $"Delete FROM P_ctrl where emp_id = '{textBox1.Text}'" +
            $"Delete FROM P_manager where emp_id = '{textBox1.Text}'" +
            $"Delete FROM P_director where emp_id = '{textBox1.Text}'" +
            $"Delete FROM P_emp where emp_id = '{textBox1.Text}'",sqlConnection);

            DelComand.ExecuteNonQuery();

                MessageBox.Show("Сотрудник покинул базу данных, как и вашу компанию", DelComand.ExecuteNonQuery().ToString());
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Delete_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBdp"].ConnectionString); //хранение в appconfig
            sqlConnection.Open();
        }
    }
}

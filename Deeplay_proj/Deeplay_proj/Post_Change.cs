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
    public partial class Post_Change : Form
    {
        private SqlConnection sqlConnection = null;
        public Post_Change()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //удаление из всех таблиц должностей сотрудника с подобныым ID
            SqlCommand DelComand = new SqlCommand(
                        $"Delete FROM P_ctrl where emp_id = '{textBox1.Text}'" +
                        $"Delete FROM P_manager where emp_id = '{textBox1.Text}'" +
                        $"Delete FROM P_director where emp_id = '{textBox1.Text}'" +
                        $"Delete FROM P_emp where emp_id = '{textBox1.Text}'", sqlConnection);
            DelComand.ExecuteNonQuery();

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    try
                    {
                        //выставление должности "работяга"
                        SqlCommand Updatecommand1 = new SqlCommand($"INSERT INTO [P_emp] (emp_id,dept_id) VALUES ('{textBox1.Text}','{textBox2.Text}')",sqlConnection);
                        SqlCommand Changecommand1 = new SqlCommand($"Update [employees] SET post_id = 1 WHERE emp_id = '{textBox1.Text}'",sqlConnection);

                        Updatecommand1.ExecuteNonQuery();
                        Changecommand1.ExecuteNonQuery();

                        MessageBox.Show("Сотрудник стал работягой", Updatecommand1.ExecuteNonQuery().ToString());

                    }catch (Exception ex) { MessageBox.Show(ex.Message); }

                    break;

                case 1:
                    try
                    {
                        //выставление должности "контролёр"
                        SqlCommand Updatecommand2 = new SqlCommand($"INSERT INTO [P_ctrl] (emp_id,dept_id,inspect) VALUES ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}')", sqlConnection);
                        SqlCommand Changecommand2 = new SqlCommand($"Update [employees] SET post_id = 2 WHERE emp_id = '{textBox1.Text}' ", sqlConnection);

                        Updatecommand2.ExecuteNonQuery();
                        Changecommand2.ExecuteNonQuery();

                        MessageBox.Show("Сотрудник стал контролёром", Updatecommand2.ExecuteNonQuery().ToString());
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                    break;

                case 2:
                    try
                    {
                        //выставление должности "Менеджер"
                        SqlCommand Updatecommand3 = new SqlCommand($"INSERT INTO [P_manager] (emp_id,dept_id) VALUES ('{textBox1.Text}','{textBox2.Text}')", sqlConnection);
                        SqlCommand Changecommand3 = new SqlCommand($"Update [employees] SET post_id = 3 WHERE emp_id = '{textBox1.Text}' ", sqlConnection);


                        Updatecommand3.ExecuteNonQuery();
                        Changecommand3.ExecuteNonQuery();

                        MessageBox.Show("Сотрудник стал руководителем отдела", Updatecommand3.ExecuteNonQuery().ToString());

                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                    break;

                case 3:
                    try
                    {
                        //выставление должности "Директор"
                        SqlCommand Updatecommand4 = new SqlCommand($"INSERT INTO [P_director] (emp_id) VALUES ('{textBox1.Text}')", sqlConnection);
                        SqlCommand Changecommand4 = new SqlCommand($"Update [employees] SET post_id = 4 WHERE emp_id = '{textBox1.Text}' ", sqlConnection);


                        Updatecommand4.ExecuteNonQuery();
                        Changecommand4.ExecuteNonQuery();

                        MessageBox.Show("Сотрудник стал директором", Updatecommand4.ExecuteNonQuery().ToString());

                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                    break;

            }
        }

        private void Post_Change_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBdp"].ConnectionString); //хранение в appconfig
            sqlConnection.Open();
        }
    }
}

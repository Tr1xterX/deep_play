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

namespace project_deepplay
{
    private SqlConnection sqlConnection = null;
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //событие подключения 
        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

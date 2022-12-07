using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvestmentManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(DBconnector.CONNECTION_STRING);

            String id = textBox1.Text;
            String pwd = textBox2.Text;

            if(id.Length==0 || pwd.Length == 0)
            {
                MessageBox.Show("Please fill all fields","Important");
            }
            else
            {
                string query = "select * from manager where id='"+id+"' and password='"+pwd+"'";

                SqlConnection conn = new SqlConnection(DBconnector.CONNECTION_STRING);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    SingletonDataAccess.managerId = id;
                    MessageBox.Show("Login Successfull");
                    ManagerDashboard form = new ManagerDashboard();
                    this.Hide();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials");
                }
                conn.Close();
                da.Dispose();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientRegister form = new ClientRegister();
            this.Hide();
            form.Show();
        }
    }
}

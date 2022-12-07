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
    public partial class ManagerDashboard : Form
    {
        public ManagerDashboard()
        {
            InitializeComponent();
            ut.Text = SingletonDataAccess.managerId;
            string query = "select count(*) as count from idea";

            SqlConnection conn = new SqlConnection(DBconnector.CONNECTION_STRING);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    it.Text = row.Field<Int32>(0).ToString();
                }
            }
            else
            {
                
            }
            conn.Close();

            query = "select count(*) as count from client";
            cmd = new SqlCommand(query, conn);
            conn.Open();
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ct.Text = row.Field<Int32>(0).ToString();
                }
            }
            else
            {

            }
            conn.Close();

            query = "select count(*) as count from products";
            cmd = new SqlCommand(query, conn);
            conn.Open();
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    pt.Text = row.Field<Int32>(0).ToString();
                }
            }
            else
            {

            }
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SingletonDataAccess.managerId = "";
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManagerClients form = new ManagerClients();
            this.Hide();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ManagerIdeas form = new ManagerIdeas();
            this.Hide();
            form.Show();
        }
    }
}

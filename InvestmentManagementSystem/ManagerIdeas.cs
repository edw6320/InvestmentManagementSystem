using System;
using System.Collections;
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
    public partial class ManagerIdeas : Form
    {
        static String clientID = "";
        static String ideaID = "";
        static String productID = "";
        public ManagerIdeas()
        {
            InitializeComponent();
            ut.Text = SingletonDataAccess.managerId;


        }

        private void ManagerIdeas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'investmentdbDataSet3.idea' table. You can move, or remove it, as needed.
            this.ideaTableAdapter.Fill(this.investmentdbDataSet3.idea);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SingletonDataAccess.managerId = "";
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ideaID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            string query = "select * from products where type<=(select type from idea where id='" + ideaID + "') and region=(select region from idea where id='" + ideaID + "') and sector=(select sector from idea where id='" + ideaID + "')";

            SqlConnection conn = new SqlConnection(DBconnector.CONNECTION_STRING);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView3.DataSource = dt;
            conn.Close();

            query = "select * from client where risk_capability>=(select risk_rating from idea where id='" + ideaID + "') and region=(select region from idea where id='" + ideaID + "') and currency=(select currency from idea where id='" + ideaID + "')";

            conn = new SqlConnection(DBconnector.CONNECTION_STRING);
            cmd = new SqlCommand(query, conn);
            conn.Open();

            reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            dataGridView2.DataSource = dt;
            conn.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ideaID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            clientID = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

            string query = "select * from products where type<=(select type from idea where id='" + ideaID + "') and region=(select region from idea where id='" + ideaID + "') and sector=(select sector from idea where id='" + ideaID + "') and min_investment<=(select investment_capability from client where email='" + clientID + "')";

            SqlConnection conn = new SqlConnection(DBconnector.CONNECTION_STRING);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView3.DataSource = dt;
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ArrayList prods = new ArrayList();
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                prods.Add(row.Cells[0].Value.ToString());

            if (clientID.Equals("") || ideaID.Equals("") || prods.Count == 0)
            {
                MessageBox.Show("Please select Client, Idea and Products to continue");
                return;
            }


            SqlConnection con = new SqlConnection(DBconnector.CONNECTION_STRING);
            String id = DateTime.Now.ToString("yyyyMMddhhmmss");
            String query = "INSERT INTO suggestions (id,manager_id,client_id,idea_id) VALUES ('" + id + "','" + SingletonDataAccess.managerId + "','" + clientID + "','" + ideaID + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Suggestion Idea Inserted");

                foreach (String p in prods)
                {
                    query = "INSERT INTO suggestion_products (suggestion_id,product_id) VALUES ('" + id + "','" + p + "')";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Suggestion Prods Inserted");
                }
                MessageBox.Show("Suggestions Saved");
                ManagerClients form = new ManagerClients();
                this.Hide();
                form.Show();

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error Generated. Details: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManagerClients form = new ManagerClients();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManagerDashboard form = new ManagerDashboard();
            this.Hide();
            form.Show();
        }
    }
}

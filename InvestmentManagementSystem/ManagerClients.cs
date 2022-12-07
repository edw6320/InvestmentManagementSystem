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
    public partial class ManagerClients : Form
    {
        static String clientID="";
        static String ideaID="";
        static String productID="";
        public ManagerClients()
        {
            InitializeComponent();
            ut.Text = SingletonDataAccess.managerId;

            //dataGridView1.Rows[0].Selected=true;
            
        }

        private void ManagerClients_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'investmentdbDataSet2.suggestions' table. You can move, or remove it, as needed.
            //this.suggestionsTableAdapter.Fill(this.investmentdbDataSet2.suggestions);
            // TODO: This line of code loads data into the 'investmentdbDataSet.client' table. You can move, or remove it, as needed.
            this.clientTableAdapter.Fill(this.investmentdbDataSet.client);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManagerDashboard form = new ManagerDashboard();
            this.Hide();
            form.Show();
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
            clientID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            string query = "select * from idea where risk_rating<=(select risk_capability from client where email='"+clientID+ "') and region=(select region from client where email='" + clientID + "') and currency=(select currency from client where email='" + clientID + "')";

            SqlConnection conn = new SqlConnection(DBconnector.CONNECTION_STRING);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView2.DataSource = dt;
            conn.Close();

            query = "select * from suggestions where client_id='"+clientID+"';";

            conn = new SqlConnection(DBconnector.CONNECTION_STRING);
            cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            dataGridView4.DataSource = dt;
            conn.Close();

            dataGridView5.DataSource = null;


        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            clientID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            string query = "select * from idea where risk_rating<=(select risk_capability from client where email='"+clientID+ "') and region=(select region from client where email='" + clientID + "') and currency=(select currency from client where email='" + clientID + "')";

            SqlConnection conn = new SqlConnection(DBconnector.CONNECTION_STRING);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView2.DataSource = dt;
            conn.Close();

            query = "select * from suggestions where client_id=" + clientID + ";";

            conn = new SqlConnection(DBconnector.CONNECTION_STRING);
            cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            dataGridView4.DataSource = dt;
            conn.Close();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ideaID = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

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

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ArrayList prods = new ArrayList();
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                //MessageBox.Show("values- " + row.Cells[0].Value.ToString());
                prods.Add(row.Cells[0].Value.ToString());

            if(clientID.Equals("") || ideaID.Equals("") || prods.Count == 0)
            {
                MessageBox.Show("Please select Client, Idea and Products to continue");
                return;
            }


            SqlConnection con = new SqlConnection(DBconnector.CONNECTION_STRING);
            String id = DateTime.Now.ToString("yyyyMMddhhmmss");
            String query = "INSERT INTO suggestions (id,manager_id,client_id,idea_id) VALUES ('"+id+"','"+ SingletonDataAccess.managerId + "','"+ clientID + "','"+ ideaID + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Suggestion Idea Inserted");

                foreach (String p in prods)
                {
                    query = "INSERT INTO suggestion_products (suggestion_id,product_id) VALUES ('"+id+"','"+p+"')";
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

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ideaID = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

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

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String suggestionID = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();

            string query = "select s.*, p.title, p.sector, p.region from suggestion_products s inner join products p on s.product_id = p.id where suggestion_id='" + suggestionID + "'";

            SqlConnection conn = new SqlConnection(DBconnector.CONNECTION_STRING);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView5.DataSource = dt;
            conn.Close();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ManagerIdeas form = new ManagerIdeas();
            this.Hide();
            form.Show();
        }
    }
}

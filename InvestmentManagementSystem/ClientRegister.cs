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
    public partial class ClientRegister : Form
    {
        public ClientRegister()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String email = textBox1.Text.ToString();
            String name = textBox2.Text.ToString();
            String contact = textBox3.Text.ToString();
            String address = textBox4.Text.ToString();
            String region = textBox5.Text.ToString();
            String profession = textBox6.Text.ToString();
            int amt = Convert.ToInt32(numericUpDown1.Value.ToString());
            String currency = comboBox1.SelectedItem.ToString();
            String risk = comboBox2.SelectedItem.ToString();


            SqlConnection con = new SqlConnection(DBconnector.CONNECTION_STRING);
            String query = "INSERT INTO client VALUES ('" + email + "','" + name + "','" + contact + "','" + address + "','" + region + "','" + risk + "','" + currency + "','" + profession + "','" + amt + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Suggestion Idea Inserted");

                MessageBox.Show("New Client Registered Successfully","Successful");
                Form1 form = new Form1();
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
    }
}

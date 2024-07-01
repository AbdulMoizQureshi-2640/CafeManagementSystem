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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CafeManagementSystem
{
    public partial class Admin_AddTable : Form
    {
        private function fn = new function();
        private string query;
        private DataSet ds;
        int id;
        string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";


        public Admin_AddTable()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Type correct Data.  An error occurred while filtering customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterCustomers()
        {
            try
            {

                if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    string filterExpression = $"tableID = {textBox1.Text.Trim()}";
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = filterExpression;
                    dataGridView1.DataSource = dv;
                }
                else
                {
                    // If the text box is empty, load all customers
                    LoadCustomers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Type correct Data.  An error occurred while filtering customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Admin_AddTable_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            query = "select * from [Table]";
            ds = fn.getData(query);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("No records found.");
            }
        }



        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string location = locationbox.Text;
            string capacityText = capacitybox.Text;

            if (string.IsNullOrWhiteSpace(location) || string.IsNullOrWhiteSpace(capacityText))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Check if capacity is an integer
            int capacity;
            if (!int.TryParse(capacityText, out capacity))
            {
                MessageBox.Show("Capacity must be a valid integer.");
                return;
            }

            // SQL query to insert table information into the database
            string query = "INSERT INTO [Table] (Capacity, Location) " +
                           "VALUES (@cap, @loc)";

            // Create a SQL connection and command
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to the query to prevent SQL injection
                command.Parameters.AddWithValue("@cap", capacity);
                command.Parameters.AddWithValue("@loc", location);

                try
                {
                    // Open connection and execute the query
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Clear all text boxes
                    capacitybox.Text = "";
                    locationbox.Text = "";
                    MessageBox.Show("Table Added Successfully.");
                    LoadCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        /*
        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string location = capacitybox.Text;
            string capacity = locationbox.Text;

            if (string.IsNullOrWhiteSpace(location) || string.IsNullOrWhiteSpace(capacity))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }


            // Check if capacity is an integer
            int capacitycheck;
            if (!int.TryParse(capacityText, out capacity))
            {
                MessageBox.Show("Capacity must be a valid integer.");
                return;
            }

            // SQL query to insert customer information into the database
            string query = "INSERT INTO [Table] (Capacity, Location) " +
                           "VALUES (@loc, @cap)";




            // Create a SQL connection and command
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to the query to prevent SQL injection
                command.Parameters.AddWithValue("@loc", location);
                command.Parameters.AddWithValue("@cap", capacity);

                try
                {
                    // Open connection and execute the query
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Clear all text boxes
                    capacitybox.Text = "";
                    locationbox.Text = "";
                    MessageBox.Show("Table Added Successfully.");
                    LoadCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        */
        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();

            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_AddUser admin_AddUser = new Admin_AddUser();
            admin_AddUser.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_UpdateUser admin_UpdateUser = new Admin_UpdateUser();
            admin_UpdateUser.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_DeleteUser admin_DeleteUser = new Admin_DeleteUser();
            admin_DeleteUser.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_UpdateTable admin_UpdateTable = new Admin_UpdateTable();
            admin_UpdateTable.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_DeleteTable admin_DeleteTable = new Admin_DeleteTable();
            admin_DeleteTable.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_SalesReport admin_SalesReport = new Admin_SalesReport();
            admin_SalesReport.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_ManageInventory admin_ManageInventory = new Admin_ManageInventory();
            admin_ManageInventory.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_LoginForm admin_LoginForm = new Admin_LoginForm();
            admin_LoginForm.Show();
        }
    }
}

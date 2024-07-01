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

namespace CafeManagementSystem
{
    public partial class Staff_AddItem : Form
    {
        string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";

        public Staff_AddItem()
        {
            InitializeComponent();
            PopulateCategories();
        }


        private void PopulateCategories()
        {
            string query = "SELECT name FROM Category";
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);  // Create a command object
                    SqlDataReader reader = cmd.ExecuteReader();  // Execute the command

                    // Clear existing items
                    comboBox1.Items.Clear();

                    // Read each record
                    while (reader.Read())
                    {
                        string categoryName = reader["name"].ToString();  // Get the name from the reader
                        comboBox1.Items.Add(categoryName);  // Add name to the ComboBox
                    }
                    comboBox1.Items.Add("No categories found");  // Handle no categories

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to data source: " + ex.Message);
            }
        }


        private void SignUpButton_Click(object sender, EventArgs e)
        {

            string itemName = itemnamebox.Text;
            string itemCategory = comboBox1.Text;
            string itemDesc = itemdescbox.Text;
            string priceText = itempricebox.Text;

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(itemName) || string.IsNullOrWhiteSpace(itemCategory) ||
                string.IsNullOrWhiteSpace(itemDesc) || string.IsNullOrWhiteSpace(priceText))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Parse the price string to float
            if (!float.TryParse(priceText, out float price))
            {
                MessageBox.Show("Invalid price format. Please enter a valid number.");
                return;
            }

            // SQL query to insert customer information into the database
            string query = "INSERT INTO Item (name, price, description, categoryID) " +
                           "VALUES (@itemName, @price, @itemDesc, " +
                           "(SELECT categoryID FROM Category WHERE name = @itemCategory))";

            // Create a SQL connection and command
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to the query to prevent SQL injection
                command.Parameters.AddWithValue("@itemName", itemName);
                command.Parameters.AddWithValue("@itemCategory", itemCategory);
                command.Parameters.AddWithValue("@itemDesc", itemDesc);
                command.Parameters.AddWithValue("@price", price);

                try
                {
                    // Open connection and execute the query
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Clear all text boxes
                    itemnamebox.Text = "";
                    itemdescbox.Text = "";
                    itempricebox.Text = "";
                    comboBox1.Text = "";

                    MessageBox.Show("Item Added Successfully.");
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

            string itemName = itemnamebox.Text;
            string itemCategory = comboBox1.Text;
            string itemDesc = itemdescbox.Text;
            string priceText = itempricebox.Text;

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(itemName) || string.IsNullOrWhiteSpace(itemCategory) ||
                string.IsNullOrWhiteSpace(itemDesc) || string.IsNullOrWhiteSpace(priceText))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Parse the price string to float
            if (!float.TryParse(priceText, out float price))
            {
                MessageBox.Show("Invalid price format. Please enter a valid number.");
                return;
            }

            // SQL query to insert customer information into the database
            string query = "INSERT INTO Item (name, price, description, category) " +
                           "VALUES (@itemName, @price, @itemDesc, @itemCategory)";

            // Create a SQL connection and command
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to the query to prevent SQL injection
                command.Parameters.AddWithValue("@itemName", itemName);
                command.Parameters.AddWithValue("@itemCategory", itemCategory);
                command.Parameters.AddWithValue("@itemDesc", itemDesc);
                command.Parameters.AddWithValue("@price", price);

                try
                {
                    // Open connection and execute the query
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Clear all text boxes
                    itemnamebox.Text = "";
                    itemcategorybox.Text = "";
                    itemdescbox.Text = "";
                    itempricebox.Text = "";

                    MessageBox.Show("Item Added Successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }
        */
        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_Dashboard staff_Dashboard = new Staff_Dashboard();
            staff_Dashboard.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_UpdateItem staff_UpdateItem = new Staff_UpdateItem();
            staff_UpdateItem.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_DeleteItem staff_DeleteItem = new Staff_DeleteItem();
            staff_DeleteItem.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
               Staff_HandleOrder staff_HandleOrder = new Staff_HandleOrder();
            staff_HandleOrder.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
              Staff_Handle_Feedback staff_HandleFeedback= new Staff_Handle_Feedback();
            staff_HandleFeedback.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_TableReservation staff_TableReservation = new Staff_TableReservation();
            staff_TableReservation.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_CustomerTrends staff_CustomerTrends= new Staff_CustomerTrends();
            staff_CustomerTrends.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_ItemTrends staff_ItemTrends=new Staff_ItemTrends();   
            staff_ItemTrends.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            this.Hide();
            Staff_LoginForm staff_LoginForm = new Staff_LoginForm();
            staff_LoginForm.Show();
        }
    }
    }


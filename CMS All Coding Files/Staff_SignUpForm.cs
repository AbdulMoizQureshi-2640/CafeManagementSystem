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
    public partial class Staff_SignUpForm : Form
    {

        string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";

        public Staff_SignUpForm()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string firstName = firstnamebox.Text;
            string lastName = lastnamebox.Text;
            string phone = phonebox.Text;
            string email = emailbox.Text;
            string password = passwordbox.Text;
            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }


            // SQL query to insert customer information into the database
            string query = "INSERT INTO Staff (firstname, lastname, email, password, phone) " +
                           "VALUES (@FirstName, @LastName,  @Email, @Password, @Phone)";

            // Create a SQL connection and command
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to the query to prevent SQL injection
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Phone", phone);


                try
                {
                    // Open connection and execute the query
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show("Sign up successful. Welcome to Cafe Management System!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Open SignUp form
            Staff_LoginForm LoginForm = new Staff_LoginForm();
            LoginForm.Show();
            // Close the login form
            this.Hide();

        }
    }
}

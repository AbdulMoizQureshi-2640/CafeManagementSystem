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
    public partial class Staff_LoginForm : Form
    {

        string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";



        private string GetActualPassword()
        {
            string actualPassword = "";
            foreach (char c in PasswordBox.Text)
            {
                if (c != '*')
                    actualPassword += c;
            }
            return actualPassword;
        }


        public Staff_LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Collect email and password from text boxes
            string email = EmailBox.Text;
            string password = GetActualPassword(); // Retrieve actual password without asterisks

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in both email and password fields.");
                return;
            }

            // SQL query to check if the email and password exist in the database
            string query = "SELECT COUNT(*) FROM [Staff] WHERE email = @Email AND password = @Password";

            // Create a SQL connection and command
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to the query to prevent SQL injection
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                try
                {
                    // Open connection and execute the query
                    connection.Open();

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        // Reset the command
                        command.CommandText = "SELECT firstname, lastname FROM [Staff] WHERE email = @Email";

                        // Use ExecuteReader to get the user details
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string firstName = reader.GetString(0); // Reader index corrected
                                string lastName = reader.GetString(1); // Reader index corrected

                                // Close the connection
                                connection.Close();

                                // Show success message
                                //MessageBox.Show($"Login successful. Welcome to Cafe Management System, {firstName}  {lastName}!");
                                Staff_Dashboard dashboard = new Staff_Dashboard();
                                dashboard.Show();
                                this.Hide();
                                // You can add further actions here if needed, such as opening a new form or closing this one.
                            }
                            else
                            {
                                MessageBox.Show("Invalid email or password.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Open SignUp form
            Staff_SignUpForm signUpForm = new Staff_SignUpForm();
            signUpForm.Show();
            // Close the login form
            this.Hide();

        }
    }
}

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
    public partial class customer_SignUpForm : Form
    {


        string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";

        public customer_SignUpForm()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string firstName = firstnamebox.Text;
            string lastName = lastnamebox.Text;
            string phoneText = phonebox.Text;
            string email = emailbox.Text;
            string password = passwordbox.Text;

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(phoneText) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!int.TryParse(phoneText, out int phone))
            {
                MessageBox.Show("Invalid phone number. Please enter a valid numeric phone number.");
                return;
            }

            string query = "INSERT INTO Customer (firstname, lastname, email, password, phone, loyaltyPoints) " +
                            "VALUES (@FirstName, @LastName, @Email, @Password, @Phone, @LoyaltyPoints)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@LoyaltyPoints", 0);  // Assuming new customers start with 0 loyalty points

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sign up successful. Welcome to Cafe Management System!");
                    }
                    else
                    {
                        MessageBox.Show("Sign up failed. No data was inserted.");
                    }
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
            Customer_LoginForm LoginForm = new Customer_LoginForm();
            LoginForm.Show();
            // Close the login form
            this.Hide();
        }
    }
}

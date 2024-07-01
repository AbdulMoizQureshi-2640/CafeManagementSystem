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

    public partial class Admin_AddUser : Form
    {
        string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";

        public Admin_AddUser()
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
            string salary = salarybox.Text;

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(salary))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Check if phone is an integer
            int parsedPhone;
            if (!int.TryParse(phone, out parsedPhone))
            {
                MessageBox.Show("Phone must be a valid integer.");
                return;
            }

            // Check if salary is an integer
            int parsedSalary;
            if (!int.TryParse(salary, out parsedSalary))
            {
                MessageBox.Show("Salary must be a valid integer.");
                return;
            }

            // Check if email contains "@"
            if (!email.Contains("@"))
            {
                MessageBox.Show("Email must contain '@' symbol.");
                return;
            }

            // SQL query to insert customer information into the database
            string query = "INSERT INTO Staff (firstname, lastname, phone, email, password, salary) " +
                           "VALUES (@FirstName, @LastName, @Phone, @Email, @Password, @Salary)";

            // Create a SQL connection and command
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to the query to prevent SQL injection
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Phone", parsedPhone); // Use parsedPhone instead of phone
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Salary", parsedSalary); // Use parsedSalary instead of salary

                try
                {
                    // Open connection and execute the query
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Clear all text boxes
                    firstnamebox.Text = "";
                    lastnamebox.Text = "";
                    emailbox.Text = "";
                    passwordbox.Text = "";
                    phonebox.Text = "";
                    salarybox.Text = "";
                    MessageBox.Show("Staff Member Added Successfully.");
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
            string firstName = firstnamebox.Text;
            string lastName = lastnamebox.Text;
            string phone = phonebox.Text;
            string email = emailbox.Text;
            string password = passwordbox.Text;
            string salary = salarybox.Text;
            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(salary))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }


            // SQL query to insert customer information into the database
            string query = "INSERT INTO Staff (firstname, lastname, phone, email, password, salary) " +
                           "VALUES (@FirstName, @LastName, @Phone, @Email, @Password, @Salary)";


            // Create a SQL connection and command
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to the query to prevent SQL injection
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Salary", salary);


                try
                {
                    // Open connection and execute the query
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // Clear all text boxes
                    firstnamebox.Text = "";
                    lastnamebox.Text = "";
                    emailbox.Text = "";
                    passwordbox.Text = "";
                    phonebox.Text = "";
                    salarybox.Text = "";
                    MessageBox.Show("Staff Member Added Successfull.");
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_AddTable admin_AddTable   = new Admin_AddTable();
            admin_AddTable.Show();
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

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_LoginForm admin_LoginForm = new Admin_LoginForm();
            admin_LoginForm.Show();
        }
    }
}

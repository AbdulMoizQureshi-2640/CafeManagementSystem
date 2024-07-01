using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace CafeManagementSystem
{
    public partial class Admin_UpdateUser : Form
    {
        private function fn = new function();
        private string query;
        private DataSet ds;
        int id;

        string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";



        public Admin_UpdateUser()
        {
            InitializeComponent();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void Admin_UpdateUser_Load(object sender, EventArgs e)
        {
            // Load all customers initially
            LoadCustomers();

        }

        private void LoadCustomers()
        {
            query = "select * from Staff";
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

        private void LoadInventory()
        {
            query = "select * from Staff";
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
                    string filterExpression = $"staffID = {textBox1.Text.Trim()}";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String fname = (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            String lname = (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            String email = (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            String pass= (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            String phone= (dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            firstnamebox.Text = fname;

            //MessageBox.Show("Sign up successful"+id);

        }



        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string firstName = firstnamebox.Text;
            string lastName = lastnamebox.Text;
            string email = emailbox.Text;
            string password = passwordbox.Text;
            string phone = phonebox.Text;
            string salary = salarybox.Text;

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(salary))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Check if email contains "@"
            if (!email.Contains("@"))
            {
                MessageBox.Show("Email must contain '@' symbol.");
                return;
            }

            // Check if phone is an integer
            if (!int.TryParse(phone, out _))
            {
                MessageBox.Show("Phone must be a valid integer.");
                return;
            }

            // Check if salary is an integer
            if (!int.TryParse(salary, out _))
            {
                MessageBox.Show("Salary must be a valid integer.");
                return;
            }

            // SQL query to update staff information in the database
            string query = "UPDATE Staff SET firstname='" + firstName + "', lastname='" + lastName + "', email='" + email + "', password='" + password + "', phone='" + phone + "', salary='" + salary + "' WHERE staffID=" + id;

            // Call setData method to execute the query
            fn.setData(query);

            // Clear all text boxes
            firstnamebox.Text = "";
            lastnamebox.Text = "";
            emailbox.Text = "";
            passwordbox.Text = "";
            phonebox.Text = "";
            salarybox.Text = "";

            // Reload customers data
            LoadCustomers();
        }



        /*
        private void SignUpButton_Click(object sender, EventArgs e)
        {
            //query = "update Customer set firstname='"+firstnamebox.Text+"',lastname = '"+lastnamebox.Text + "',email = '" + emailbox.Text + "',password = '" + passwordbox.Text + "',phone = '" + phonebox.Text+" where customerID= "+id+";";
            string query = "UPDATE Staff SET firstname='" + firstnamebox.Text + "', lastname='" + lastnamebox.Text + "', email='" + emailbox.Text + "', password='" + passwordbox.Text + "', phone='" + phonebox.Text + "', salary= '"+ salarybox.Text+"' WHERE staffID=" + id + ";";

            fn.setData(query);

            // Clear all text boxes
            firstnamebox.Text = "";
            lastnamebox.Text = "";
            emailbox.Text = "";
            passwordbox.Text = "";
            phonebox.Text = "";
            salarybox.Text = "";
            LoadCustomers() ;

        }
        */
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            //MessageBox.Show("Sign up successful" + id);



            String fname = (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            String lname = (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            String email = (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            String pass = (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            String phone = (dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
            String salary = (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());

            firstnamebox.Text = fname;
            lastnamebox.Text = lname;
            emailbox.Text = email;
            passwordbox.Text = pass;
            phonebox.Text = phone;
            salarybox.Text = salary;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_DeleteUser admin_DeleteUser = new Admin_DeleteUser();
            admin_DeleteUser.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_AddTable admin_AddTable = new Admin_AddTable();
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

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_LoginForm admin_LoginForm = new Admin_LoginForm();
            admin_LoginForm.Show();
        }
    }
    internal class function
    {
        string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";

        internal DataSet getData(string query)
        {
                    // string connectionString = "Data Source=DESKTOP-LGL7S0Q\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";


        // Create a new DataSet
        DataSet ds = new DataSet();

            try
            {
                // Create a SqlConnection using the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlDataAdapter to fetch data from the database
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    // Fill the DataSet with the data
                    adapter.Fill(ds);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return ds;
        }

        internal void setData(string query)
        {
            try
            {
                // Create a SqlConnection using the connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand to execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data Updates Successfully !");

                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
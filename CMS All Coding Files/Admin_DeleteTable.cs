using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class Admin_DeleteTable : Form
    {
        private function fn = new function();
        private string query;
        private DataSet ds;
        int id;
        string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";

        public Admin_DeleteTable()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void Admin_DeleteTable_Load(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            String location = (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            String capacity = (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());


            locationbox.Text = location;
            capacitybox.Text = capacity;
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            query = "DELETE from [Table] where tableID='"+id+"'";
            fn.setData(query);
            // Clear all text boxes
            locationbox.Text = "";
            capacitybox.Text = "";
            LoadCustomers();

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

            Admin_AddTable admin_AddTable = new Admin_AddTable();
            admin_AddTable.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_UpdateTable admin_UpdateTable = new Admin_UpdateTable();
            admin_UpdateTable.Show();
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

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_LoginForm admin_LoginForm = new Admin_LoginForm();
            admin_LoginForm.Show();
        }
    }
}

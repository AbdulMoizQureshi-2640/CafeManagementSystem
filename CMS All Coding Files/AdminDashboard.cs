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
    public partial class AdminDashboard : Form
    {
        private function fn = new function();
        private string query;
        private DataSet ds1;
        private DataSet ds2;

        int id;

        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_UpdateUser UpdateUserScreen = new Admin_UpdateUser();
            UpdateUserScreen.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_AddUser AddUserScreen=new Admin_AddUser();
            AddUserScreen.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_DeleteUser DeleteUserScreen = new Admin_DeleteUser();
            DeleteUserScreen.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_AddTable AddTableScreen = new Admin_AddTable();
            AddTableScreen.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_UpdateTable UpdateTableScreen = new Admin_UpdateTable();
            UpdateTableScreen.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_DeleteTable DeleteTableScreen = new Admin_DeleteTable();
            DeleteTableScreen.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_SalesReport SalesReportScreen = new Admin_SalesReport();
            SalesReportScreen.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Admin_AddUser AddUserScreen = new Admin_AddUser();
            AddUserScreen.ShowDialog();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadInventory();
        }
        private void LoadCustomers()
        {
            query = "select * from Staff";
            ds1 = fn.getData(query);
            if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds1.Tables[0];
            }
            else
            {
                MessageBox.Show("No records found.");
            }
        }

        private void LoadInventory()
        {
            query = "\r\nSELECT inv.inventoryID, inv.itemID, i.name AS ItemName,inv.quantity AS Quantity, inv.supplierID, s.CompanyName AS SupplierCompany,s.ContactPhone AS SupplierPhone\r\nFROM Inventory inv\r\nJOIN Item i ON inv.itemID = i.itemID\r\nJOIN Supplier s ON inv.supplierID = s.supplierID;";
            ds2 = fn.getData(query);
            if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                dataGridView2.DataSource = ds2.Tables[0];
            }
            else
            {
                MessageBox.Show("No records found.");
            }
        }


        private void FilterCustomers()
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    string filterExpression = $"staffID = {textBox1.Text.Trim()}";
                    DataView dv = ds1.Tables[0].DefaultView;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //FilterCustomers();
            try
            {
                FilterCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Type correct Data.  An error occurred while filtering customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Type correct Data.  An error occurred while filtering inventory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterInventory()
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text.Trim()))
                {
                    string filterExpression = $"inventoryID = {textBox2.Text.Trim()}";
                    DataView dv = ds2.Tables[0].DefaultView;
                    dv.RowFilter = filterExpression;
                    dataGridView2.DataSource = dv;
                }
                else
                {
                    // If the text box is empty, load all inventory
                    LoadInventory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Type correct Data. An error occurred while filtering inventory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_ManageInventory ManageInventoryScreen = new Admin_ManageInventory();
            ManageInventoryScreen.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_LoginForm admin_LoginForm = new Admin_LoginForm();
            admin_LoginForm.Show();
        }
    }
}

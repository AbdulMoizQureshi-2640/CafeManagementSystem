using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeManagementSystem
{
    public partial class Admin_ManageInventory : Form
    {
        private function fn = new function();
        private string query;
        private DataSet ds;
        int id;
        public Admin_ManageInventory()
        {
            InitializeComponent();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());

            //MessageBox.Show("Sign up successful" + id);


            String iid = (dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());

            String item_id = (dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());

            String item_name = (dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString());
            String supp_name = (dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString());

            String quantity = (dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString());

            itemidbox.Text = item_id;
            itemnamebox.Text = item_name;
            suppliernamebox.Text = supp_name;
            quantitybox.Text = quantity;
            invidbox.Text = iid;


        }

        private void Admin_ManageInventory_Load(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void LoadInventory()
        {
            query = "\r\nSELECT inv.inventoryID, inv.itemID, i.name AS ItemName,inv.quantity AS Quantity, inv.supplierID, s.CompanyName AS SupplierCompany,s.ContactPhone AS SupplierPhone\r\nFROM Inventory inv\r\nJOIN Item i ON inv.itemID = i.itemID\r\nJOIN Supplier s ON inv.supplierID = s.supplierID;";
            ds = fn.getData(query);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dataGridView2.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("No records found.");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterInventory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Type correct Data.  An error occurred while filtering customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FilterInventory()
        {
            try
            {


                if (!string.IsNullOrEmpty(textBox2.Text.Trim()))
                {
                    string filterExpression = $"inventoryID = {textBox2.Text.Trim()}";
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = filterExpression;
                    dataGridView2.DataSource = dv;
                }
                else
                {
                    // If the text box is empty, load all customers
                    LoadInventory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Type correct Data.  An error occurred while filtering customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Inventory SET quantity='" + quantitybox.Text + "' WHERE inventoryID=" + id + ";";
            fn.setData(query);
            itemidbox.Text = "";
            itemnamebox.Text = "";
            suppliernamebox.Text = "";
            quantitybox.Text = "";
            invidbox.Text = "";
            LoadInventory();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string query = "DELETE from Inventory WHERE inventoryID=" + id + ";";
            fn.setData(query);
            itemidbox.Text = "";
            itemnamebox.Text = "";
            suppliernamebox.Text = "";
            quantitybox.Text = "";
            invidbox.Text = "";
            LoadInventory();
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

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_LoginForm admin_LoginForm = new Admin_LoginForm();
            admin_LoginForm.Show();
        }
    }
}

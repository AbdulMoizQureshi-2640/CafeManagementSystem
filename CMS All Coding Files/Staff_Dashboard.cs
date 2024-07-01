using System;
using System.Collections;
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
    public partial class Staff_Dashboard : Form
    {
        private function fn = new function();
        private string query;
        private DataSet ds1;
        private DataSet ds2;
        public Staff_Dashboard()
        {
            InitializeComponent();
            LoadItems();
            LoadInventory();
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

        private void LoadItems()
        {
            query = "\r\nSELECT \r\n    i.itemID,\r\n    i.name AS ItemName,\r\n    i.price AS ItemPrice,\r\n    c.name AS CategoryName\r\nFROM \r\n    Item i\r\nJOIN \r\n    Category c ON i.categoryID = c.categoryID;\r\n";
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_AddItem staff_AddItem = new Staff_AddItem();
            staff_AddItem.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            Staff_HandleOrder staff_HandleOrder= new Staff_HandleOrder();
            staff_HandleOrder.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_Handle_Feedback staff_Handle_Feedback = new Staff_Handle_Feedback();
            staff_Handle_Feedback.Show();
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
            Staff_ItemTrends staff_ItemTrends = new Staff_ItemTrends();
            staff_ItemTrends.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Staff_Dashboard_Load(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_LoginForm staff_LoginForm = new Staff_LoginForm();
            staff_LoginForm.Show();
        }
    }
}

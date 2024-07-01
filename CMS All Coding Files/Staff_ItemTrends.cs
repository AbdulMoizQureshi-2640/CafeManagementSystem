using System;
using System.Collections;
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
    public partial class Staff_ItemTrends : Form
    {
        private function fn = new function();
        private string query;
        private DataSet ds;
        public Staff_ItemTrends()
        {
            InitializeComponent();
        }

        private void Staff_ItemTrends_Load(object sender, EventArgs e)
        {
            LoadHighestOrderedItem();
            LoadCatagoryWithHighestItems();
            LoadSuppliers();
            LoadHighestOrderedItemByCustomer();
        }
        private void LoadHighestOrderedItem()
        {
            query = "SELECT \r\n    i.itemID, \r\n    i.name AS item_name,\r\n    COUNT(od.orderID) AS total_orders\r\nFROM \r\n    OrderDetails od\r\nJOIN \r\n    Item i ON od.itemID = i.itemID\r\nGROUP BY \r\n    i.itemID, i.name\r\nHAVING \r\n    COUNT(od.orderID) = (\r\n        SELECT MAX(order_count)\r\n        FROM (\r\n            SELECT COUNT(orderID) AS order_count\r\n            FROM OrderDetails\r\n            GROUP BY itemID\r\n        ) AS max_orders\r\n    );";
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

        private void LoadCatagoryWithHighestItems()
        {
            query = "\r\nSELECT TOP 5\r\n    c.name AS CategoryName,\r\n    COUNT(i.itemID) AS TotalProducts\r\nFROM\r\n    Category c\r\nJOIN\r\n    Item i ON c.categoryID = i.categoryID\r\nGROUP BY\r\n    c.name\r\nORDER BY\r\n    COUNT(i.itemID) DESC;\r\n";
            ds = fn.getData(query);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dataGridView4.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("No records found.");
            }
        }

        private void LoadSuppliers()
        {
            query = "\r\n\tSELECT TOP 5\r\n    s.CompanyName AS SupplierName,\r\n    i.name AS ItemName,\r\n    SUM(inv.quantity) AS TotalQuantity,\r\n    MAX(i.price) AS MaxItemPrice\r\nFROM\r\n    Supplier s\r\nJOIN\r\n    Inventory inv ON s.supplierID = inv.supplierID\r\nJOIN\r\n    Item i ON inv.itemID = i.itemID\r\nJOIN\r\n    OrderDetails od ON i.itemID = od.itemID\r\nGROUP BY\r\n    s.CompanyName, i.name\r\nORDER BY\r\n    MAX(i.price) DESC;\r\n";
            ds = fn.getData(query);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dataGridView3.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("No records found.");
            }
        }

        private void LoadHighestOrderedItemByCustomer()
        {
            query = "\r\n\tSELECT TOP 5\r\n    i.name AS ItemName,\r\n    c.firstname + ' ' + c.lastname AS CustomerName,\r\n    COUNT(od.orderDetailsID) AS TotalOrders\r\nFROM\r\n    Item i\r\nJOIN\r\n    OrderDetails od ON i.itemID = od.itemID\r\nJOIN\r\n    [Order] o ON od.orderID = o.orderID\r\nJOIN\r\n    Customer c ON o.customerID = c.customerID\r\nGROUP BY\r\n    i.name, c.firstname, c.lastname\r\nORDER BY\r\n    COUNT(od.orderDetailsID) DESC;";
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_AddItem staff_AddItem = new Staff_AddItem();
            staff_AddItem.Show();
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
            Staff_Handle_Feedback staff_HandleFeedback = new Staff_Handle_Feedback();
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
            Staff_CustomerTrends staff_CustomerTrends = new Staff_CustomerTrends();
            staff_CustomerTrends.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            this.Hide();
            Staff_LoginForm staff_LoginForm = new Staff_LoginForm();
            staff_LoginForm.Show();
        }
    }
}

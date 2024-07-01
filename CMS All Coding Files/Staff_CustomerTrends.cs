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
    public partial class Staff_CustomerTrends : Form
    {
        private function fn = new function();
        private string query;
        private DataSet ds;
        public Staff_CustomerTrends()
        {
            InitializeComponent();
        }

        private void Staff_CustomerTrends_Load(object sender, EventArgs e)
        {
            LoadTopCustomersWithMostSpent();
            LoadHighestOrderValueCustomer();
            LoadTotalRevenueByEachCustomer();
            LoadTotalNoOfFeedbackByEachCustomer();
            LoadHighestAvgQuantityPerOrder();
        }
        private void LoadTopCustomersWithMostSpent()
        {
            query = "\r\nSELECT TOP 5 customerID,firstname, lastname, loyaltyPoints, (\r\n    SELECT SUM(totalAmount)\r\n    FROM [Order] o\r\n    WHERE o.customerID = c.customerID\r\n) AS Total_Spent\r\nFROM Customer c\r\nORDER BY total_spent DESC;\r\n";
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

        private void LoadHighestOrderValueCustomer()
        {
            query = "\r\nSELECT c.customerID, c.firstname, c.lastname, c.loyaltyPoints, customer_avg.avg_order_value\r\nFROM Customer c\r\nJOIN (\r\n    SELECT o.customerID, AVG(o.totalAmount) AS avg_order_value\r\n    FROM [Order] o\r\n    GROUP BY o.customerID\r\n) AS customer_avg ON c.customerID = customer_avg.customerID\r\nWHERE customer_avg.avg_order_value = (\r\n    SELECT MAX(avg_order_value)\r\n    FROM (\r\n        SELECT AVG(totalAmount) AS avg_order_value\r\n        FROM [Order]\r\n        GROUP BY customerID\r\n    ) AS avg_orders\r\n);";
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

        private void LoadTotalRevenueByEachCustomer()
        {
            query = "\r\nSELECT c.customerID,c.firstname, c.lastname, total_revenue\r\nFROM Customer c\r\nJOIN (\r\n    SELECT o.customerID, SUM(o.totalAmount) AS total_revenue\r\n    FROM [Order] o\r\n    GROUP BY o.customerID\r\n) AS customer_revenue ON c.customerID = customer_revenue.customerID;\r\n";
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


        private void LoadTotalNoOfFeedbackByEachCustomer()
        {
            query = "\r\nSELECT\r\n    firstname+lastname AS Name,\r\n    (SELECT COUNT(*) FROM Feedback f WHERE f.customerID = c.customerID) AS total_reviews\r\nFROM\r\n    Customer c;\r\n";
            ds = fn.getData(query);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                dataGridView5.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("No records found.");
            }
        }

        private void LoadHighestAvgQuantityPerOrder()
        {
            query = "\r\n\tSELECT c.firstname, c.lastname, AVG(od.quantity) AS Avg_Quantity\r\nFROM Customer c\r\nJOIN [Order] o ON c.customerID = o.customerID\r\nJOIN OrderDetails od ON o.orderID = od.orderID\r\nGROUP BY c.customerID, c.firstname, c.lastname\r\nHAVING AVG(od.quantity) = (\r\n    SELECT MAX(avg_quantity)\r\n    FROM (\r\n        SELECT AVG(quantity) AS avg_quantity\r\n        FROM OrderDetails\r\n        GROUP BY orderID\r\n    ) AS max_avg_quantity\r\n);\r\n";
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

        private void label8_Click(object sender, EventArgs e)
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
            Staff_UpdateItem staff_UpdateItem=new Staff_UpdateItem();
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

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_ItemTrends staff_ItemTrends = new Staff_ItemTrends();
            staff_ItemTrends.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            this.Hide();
            Staff_LoginForm staff_LoginForm = new Staff_LoginForm();
            staff_LoginForm.Show();
        }
    }
}

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
    public partial class Staff_HandleOrder : Form
    {

        private function fn = new function();
        private string query;
        private DataSet ds;
        int id;
        public Staff_HandleOrder()
        {
            InitializeComponent();
        }

        private void Staff_HandleOrder_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }


        private void LoadCustomers()
        {
            //query = "SELECT o.orderID AS order_number,c.firstname AS first_name, c.lastname AS last_name, i.name AS item_ordered, o.status AS order_status\r\nFROM Customer c\r\nJOIN [Order] o ON c.customerID = o.customerID\r\nJOIN OrderDetails od ON o.orderID = od.orderID\r\nJOIN Item i ON od.itemID = i.itemID;\r\n";

            query = "\tSELECT \r\n    o.orderID AS order_number,\r\n    c.firstname AS first_name, \r\n    c.lastname AS last_name, \r\n    i.name AS item_ordered, \r\n    o.totalAmount AS order_amount,\r\n    o.status AS order_status,\r\n    cb.amount AS bill_amount,\r\n    cb.status AS bill_status\r\nFROM \r\n    Customer c\r\nJOIN \r\n    [Order] o ON c.customerID = o.customerID\r\nJOIN \r\n    OrderDetails od ON o.orderID = od.orderID\r\nJOIN \r\n    Item i ON od.itemID = i.itemID\r\nLEFT JOIN \r\n    CustomerBill cb ON o.orderID = cb.orderID;";
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
        /*
        private void FilterCustomers()
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                string filterExpression = $"order_number = {textBox1.Text.Trim()}";
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
        */
        private void FilterCustomers()
        {
            string filterExpression = "";

            // Filter by order ID
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                filterExpression += $"order_number = {textBox1.Text.Trim()}";
            }

            // Filter by order status
            if (comboBox1.SelectedIndex != -1)
            {
                if (!string.IsNullOrEmpty(filterExpression))
                {
                    filterExpression += " AND ";
                }
                filterExpression += $"order_status = '{comboBox1.SelectedItem.ToString()}'";
            }

            // Apply the filter
            if (!string.IsNullOrEmpty(filterExpression))
            {
                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = filterExpression;
                dataGridView1.DataSource = dv;
            }
            else
            {
                // If both filters are empty, load all customers
                LoadCustomers();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            String idd = (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            orderIDValue.Text = idd;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void orderIDValue_Click(object sender, EventArgs e)
        {

        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            query = "UPDATE [Order] SET status = '" + orderstatusdropdown.Text + "' WHERE OrderID = " + id + ";";
            fn.setData(query);
            orderstatusdropdown.Text = "";
            orderIDValue.Text = "00";
            LoadCustomers();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterCustomers();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FilterCustomers();
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
            Staff_UpdateItem staff_UpdateItem=new Staff_UpdateItem();
            staff_UpdateItem.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_DeleteItem staff_DeleteItem = new Staff_DeleteItem();
            staff_DeleteItem.Show();
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

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Staff_ItemTrends staff_ItemTrends = new Staff_ItemTrends();
            staff_ItemTrends.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            this.Hide();
            Staff_LoginForm staff_LoginForm = new Staff_LoginForm();
            staff_LoginForm.Show();
        }
    }
}

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
    public partial class Staff_Handle_Feedback : Form
    {

        private function fn = new function();
        private string query;
        private DataSet ds;
        int id;

        public Staff_Handle_Feedback()
        {
            InitializeComponent();
        }

        private void Staff_Handle_Feedback_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadAnalyzeFeedback();
        }
        private void LoadCustomers()
        {
            /*ALTER TABLE Feedback
    ADD status VARCHAR(50) DEFAULT 'Under Notice';

UPDATE Feedback
SET status = 'Under Notice';*/

            query = "\r\n\r\nSELECT f.feedbackID,c.firstname+c.lastname AS Customer_Name,\r\n        f.rating, f.comments,f.status,\r\n        i.name AS item_ordered\r\nFROM Feedback f\r\nJOIN [Order] o ON f.orderID = o.orderID\r\nJOIN OrderDetails od ON o.orderID = od.orderID\r\nJOIN Item i ON od.itemID = i.itemID\r\nJOIN Customer c ON f.customerID = c.customerID;";
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

        private void LoadAnalyzeFeedback()
        {
            query = "SELECT rating AS Rating, COUNT(*) AS No_of_Feedbacks\r\nFROM Feedback\r\nGROUP BY rating;\r\n";
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


        private void FilterCustomers()
        {
            string filterExpression;


                
                filterExpression = $"status = '{comboBox1.SelectedItem.ToString()}'";
            

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterCustomers();
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            query = "UPDATE Feedback SET status = '" + orderstatusdropdown.Text + "' WHERE feedbackID = " + id + ";";
            fn.setData(query);
            orderstatusdropdown.Text = "";
            orderIDValue.Text = "00";
            LoadCustomers();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            String idd = (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            orderIDValue.Text = idd;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadAnalyzeFeedback();
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
            Staff_UpdateItem staff_UpdateItem=  new Staff_UpdateItem();
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


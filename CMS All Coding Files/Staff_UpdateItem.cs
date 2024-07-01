using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CafeManagementSystem
{
    public partial class Staff_UpdateItem : Form
    {
        private function fn = new function();
        private string query;
        private DataSet ds;
        int id;

        public Staff_UpdateItem()
        {
            InitializeComponent();
        }

      
        private void Staff_UpdateItem_Load(object sender, EventArgs e)
        {

            // Load all customers initially
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            query = "select * from Item";
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
            FilterCustomers();
        }
        private void FilterCustomers()
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                string filterExpression = $"itemID = {textBox1.Text.Trim()}";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            //MessageBox.Show("Sign up successful" + id);



            String name = (dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            String price = (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            String desc = (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            String category = (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            itemnamebox.Text = name;
            itemcategorybox.Text = category;
            itemdescbox.Text =desc;
            itempricebox.Text = price;

        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            //query = "update Customer set firstname='"+firstnamebox.Text+"',lastname = '"+lastnamebox.Text + "',email = '" + emailbox.Text + "',password = '" + passwordbox.Text + "',phone = '" + phonebox.Text+" where customerID= "+id+";";
            string query = "UPDATE Item SET name='" + itemnamebox.Text +    "' ,price='" + itempricebox.Text + "' WHERE itemID=" + id + ";";

            fn.setData(query);
            itemnamebox.Text = "";
            itemcategorybox.Text = "";
            itemdescbox.Text = "";
            itempricebox.Text = "";
            LoadCustomers();
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
            Staff_AddItem staff_AddItem= new Staff_AddItem();
            staff_AddItem.Show();
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

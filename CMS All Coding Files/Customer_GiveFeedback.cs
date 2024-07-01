using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CafeManagementSystem
{
    public partial class Customer_GiveFeedback : Form
    {
        // Assuming connectionString is properly defined
        private string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";

        public Customer_GiveFeedback()
        {
            InitializeComponent();
            LoadOrderData();
            this.Order_Details_dataGridView.SelectionChanged += new System.EventHandler(this.Order_Details_dataGridView_SelectionChanged);


        }

        private void LoadOrderData()
        {
            string query = "SELECT orderID, customerID, date, TotalAmount, status FROM [Order]";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            Order_Details_dataGridView.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve data: " + ex.Message);
                }
            }
        }
       
        
        private int selectedOrderId;
        private string selectedCustomerId;

        private void Order_Details_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (Order_Details_dataGridView.CurrentRow != null)
            {
                DataGridViewRow row = Order_Details_dataGridView.CurrentRow;
                selectedOrderId = Convert.ToInt32(row.Cells["orderID"].Value);  // Check if this value is retrieved correctly
                selectedCustomerId = row.Cells["customerID"].Value.ToString(); // Check if this value is retrieved correctly
          
            }
        }


        private void Feedback_Description_richTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string feedbackText = Feedback_Description_richTextBox.Text;
            int rating = int.Parse(txtRating.Text);
            if (rating > 0 && rating <= 5)
            {
                if (selectedOrderId > 0 && !string.IsNullOrEmpty(selectedCustomerId))
                {
                    string insertQuery = "INSERT INTO Feedback (customerID, orderID, rating, comments, feedbackDate) " +
                                         "VALUES (@customerID, @orderID, @rating, @comments, @feedbackDate)";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@customerID", selectedCustomerId);
                                cmd.Parameters.AddWithValue("@orderID", selectedOrderId);
                                cmd.Parameters.AddWithValue("@rating", rating);
                                cmd.Parameters.AddWithValue("@comments", feedbackText);
                                cmd.Parameters.AddWithValue("@feedbackDate", DateTime.Now);

                                int result = cmd.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    MessageBox.Show("Feedback submitted successfully.");
                                }
                                else
                                {
                                    MessageBox.Show("Failed to submit feedback.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select an order and ensure customer ID is available.");
                }
            }
            else
            {
                MessageBox.Show("rating must be between 0-5: " );
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Customer_Dashboard dashboard = new Customer_Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Customer_PlaceOrder placeOrder = new Customer_PlaceOrder();
            placeOrder.Show();
            this.Hide();
        }

  

        private void btnManageOrder_Click(object sender, EventArgs e)
        {
            Customer_CancelOrder customer_ManageOrders = new Customer_CancelOrder();
            customer_ManageOrders.Show();
            this.Hide();

        }


        private void btnReserveTable_Click(object sender, EventArgs e)
        {
            Customer_ReserveTable reserveTable = new Customer_ReserveTable();
            reserveTable.Show();
            this.Hide();
        }


    
    }
}


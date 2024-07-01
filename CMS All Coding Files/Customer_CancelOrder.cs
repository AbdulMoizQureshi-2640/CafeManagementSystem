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
using System.Windows.Forms.VisualStyles;

namespace CafeManagementSystem
{
    public partial class Customer_CancelOrder : Form
    {
        private string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";
        
        public Customer_CancelOrder()
        {
            InitializeComponent();
            LoadOrderData();
            Orders_dataGridView.CellClick += Orders_dataGridView_CellClick; // Add this line
        }

        private int selectedOrderId; // To hold the selected order's ID
        private string selectedOrderStatus; // To hold the selected order's status

        private void Orders_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Make sure a valid row is clicked
            {
                DataGridViewRow row = Orders_dataGridView.Rows[e.RowIndex];
                selectedOrderId = Convert.ToInt32(row.Cells["orderID"].Value);
                selectedOrderStatus = row.Cells["status"].Value.ToString();
            }
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
                            Orders_dataGridView.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve data: " + ex.Message);
                }
            }
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (selectedOrderId > 0)
            {
                string cancelQuery = "UPDATE [Order] SET status = 'Cancelled' WHERE orderID = @orderID";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(cancelQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@orderID", selectedOrderId);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Order cancelled successfully.");
                            LoadOrderData(); // Reload data to reflect changes
                        }
                        else
                        {
                            MessageBox.Show("Failed to cancel the order.");
                        }
                    }
                }
            }
            else
            {


                MessageBox.Show("Please select an order to cancel.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer_PlaceOrder placeOrder = new Customer_PlaceOrder();
            placeOrder.Show();
            this.Hide();
        }

      


        private void btnGiveFeedback_Click(object sender, EventArgs e)
        {
            Customer_GiveFeedback giveFeedback = new Customer_GiveFeedback();
            giveFeedback.Show();
            this.Hide();
        }




        private void btnReserveTable_Click(object sender, EventArgs e)
        {
            Customer_ReserveTable reserveTable = new Customer_ReserveTable();
            reserveTable.Show();
            this.Hide();
        }

    
        private void button9_Click(object sender, EventArgs e)
        {
            Customer_Dashboard dashboard = new Customer_Dashboard();
            dashboard.Show();
            this.Hide();
        }
    }
}

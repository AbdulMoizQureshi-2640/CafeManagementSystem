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

namespace CafeManagementSystem
{
    public partial class Customer_ReserveTable : Form
    {

        private string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";

        public Customer_ReserveTable()
        {
            InitializeComponent();
            LoadOrderData();
          
        

        }

     

        private void LoadOrderData()
        {
            string query = "SELECT tableID, Capacity, Location FROM [Table]";
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
                            AvailableTable_dataGridView.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve data: " + ex.Message);
                }
            }
        }

        
        private int TableId;
        private string selectedCustomerId;


        private void AvailableTable_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (AvailableTable_dataGridView.CurrentRow != null)
            {
                DataGridViewRow row = AvailableTable_dataGridView.CurrentRow;
                TableId = Convert.ToInt32(row.Cells["tableID"].Value);  // Check if this value is retrieved correctly
                int a = 1;
                selectedCustomerId = a.ToString(); // Check if this value is retrieved correctly

            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime justDate = dateTimePicker.Value.Date;
            TimeSpan justTime = dateTimePicker.Value.TimeOfDay;

            if (TableId > 0 && !string.IsNullOrEmpty(selectedCustomerId))
                {
                    string insertQuery = "INSERT INTO Reservation (customerID, tableID, reservationTime, reservationDate)  " +
                                         "VALUES (@customerID, @TableId, @reservationTime, @reservationDate)";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@customerID", selectedCustomerId);
                                cmd.Parameters.AddWithValue("@tableID", TableId);
                                cmd.Parameters.AddWithValue("@reservationTime", justTime);
                                cmd.Parameters.AddWithValue("@reservationDate", justDate);
                       

                                int result = cmd.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    MessageBox.Show("Table Reserved Successfully.");
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
        private void button5_Click(object sender, EventArgs e)
        {
            Customer_Dashboard dashboard = new Customer_Dashboard();
            dashboard.Show();
            this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Customer_PlaceOrder placeOrder = new Customer_PlaceOrder();
            placeOrder.Show();
            this.Hide();
        }
      

       

        private void button3_Click(object sender, EventArgs e)
        {
            Customer_GiveFeedback giveFeedback = new Customer_GiveFeedback();
            giveFeedback.Show();
            this.Hide();
        }

        private void btnManageOrder_Click(object sender, EventArgs e)
        {
            Customer_CancelOrder customer_ManageOrders = new Customer_CancelOrder();
            customer_ManageOrders.Show();
            this.Hide();
        }
    }
}

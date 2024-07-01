using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CafeManagementSystem
{
    public partial class Customer_Dashboard : Form
    {
        private string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";

        public Customer_Dashboard()
        {
            InitializeComponent();
            LoadOrderData1();
            LoadCustomerOrderDetails(1);


        }
        private void LoadOrderData1()
        {
            int CustomerID = 1;

            //2 join Query

            string query = "select f.feedbackID as FeedbackID,f.orderID as OrderID,f.rating as Rating,f.comments as Comments,f.feedbackDate as FeedbackDate from customer c join Feedback f on c.customerID = f.feedbackID where f.customerID = @CustomerID; ";  
             string query1 ="SELECT r.reservationID, r.reservationTime, t.TableID, t.location FROM Reservation r JOIN [Table] t ON r.tableID = t.tableID WHERE r.customerID = @CustomerID";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            my_Feedback_dataGridView.DataSource = dt;
                        }
                    }
                   
                    using (SqlCommand cmd = new SqlCommand(query1, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            Reservation_Details_dataGridView2.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve data: " + ex.Message);
                }
            }
        }

        public void LoadCustomerOrderDetails(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT o.orderID, o.date AS OrderDate, o.status AS OrderStatus, i.name AS ItemName, i.description AS ItemDescription, i.price AS ItemPrice, od.quantity, (od.quantity * i.price) AS TotalItemCost FROM Customer c JOIN [Order] o ON c.customerID = o.customerID LEFT JOIN OrderDetails od ON o.orderID = od.orderID LEFT JOIN Item i ON od.itemID = i.itemID  WHERE c.customerID = @CustomerID   ORDER BY o.date DESC;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Item_Details_Order_dataGridView1.DataSource = dataTable;
            }
        }





        private void Customer_Dashboard_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
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

        private void btnManageOrder_Click(object sender, EventArgs e)
        {
            Customer_CancelOrder customer_ManageOrders = new Customer_CancelOrder();
            customer_ManageOrders.Show();
            this.Hide();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage homepage = new Homepage(); 
            homepage.Show();

        }
    }
}

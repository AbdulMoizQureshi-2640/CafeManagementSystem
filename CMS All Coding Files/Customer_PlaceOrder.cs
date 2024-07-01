using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace CafeManagementSystem
{
    public partial class Customer_PlaceOrder : Form
    {
       
        string connectionString = "Data Source=WALEED\\SQLEXPRESS;Initial Catalog=CafeManagementSystem;Integrated Security=True";
      

        public Customer_PlaceOrder()
        {
            InitializeComponent();
            listBox1.Items.Clear();
            this.PopulateCategories();
        }

        private void PopulateCategories()
        {
            string query = "SELECT name FROM Category";
            try
            {
               
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();  
                    SqlCommand cmd = new SqlCommand(query, conn);  // Create a command object
                    SqlDataReader reader = cmd.ExecuteReader();  // Execute the command

                    // Clear existing items
                    comboBox1.Items.Clear();

                    // Read each record
                    while (reader.Read())
                    {
                        string categoryName = reader["name"].ToString();  // Get the name from the reader
                        comboBox1.Items.Add(categoryName);  // Add name to the ComboBox
                    }
                    comboBox1.Items.Add("No categories found");  // Handle no categories
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to data source: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlConnection conn = new SqlConnection(connectionString);
            string category = comboBox1.Text;
            String query = "  select i.name from Item i join Category c on i.categoryID = c.categoryID where c.name = '"+category+"'";



            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            DataSet ds =new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlConnection conn = new SqlConnection(connectionString);
            string category = comboBox1.Text;
            string query = "SELECT i.name FROM Item i JOIN Category c ON i.categoryID = c.categoryID WHERE c.name = '" + category + "' AND i.name LIKE '%" + txtSearch.Text + "%'";



            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            txtQuantity.ResetText();
            txtTotal.Clear();
            string txt = listBox1.GetItemText(listBox1.SelectedItem);

            txtItemName.Text = txt;

            string query="select price from item where name= '"+txt+"'";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            txtPrice.Text =ds.Tables[0].Rows[0][0].ToString();

        }

        private void txtQuantity_ValueChanged(object sender, EventArgs e)
        {
            int quan = Convert.ToInt32(txtQuantity.Value);
            if (float.TryParse(txtPrice.Text, out float price))
            {
                txtTotal.Text = (quan * price).ToString("0.00"); 
            }
            else
            {
                MessageBox.Show("Please enter a valid price.");
                txtPrice.Focus(); 
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        protected decimal total =0;
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (txtTotal.Text != "0" && txtTotal.Text != "")
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = txtItemName.Text;
                dataGridView2.Rows[n].Cells[1].Value = txtPrice.Text;
                dataGridView2.Rows[n].Cells[2].Value = txtQuantity.Text;
                dataGridView2.Rows[n].Cells[3].Value = txtTotal.Text;

                if (decimal.TryParse(txtTotal.Text, out decimal parsedValue))
                {
                    total += parsedValue;  // Add parsedValue to total if the parsing is successful
                    lblGrandTotal.Text = "RS. " + total.ToString("0.00");  // Update the label with the new total formatted
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a numeric value.");  // Shows an error message if parsing fails
                }
            }
            else
            {
                MessageBox.Show("Quantity need to be more than 0", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        protected decimal amount;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                amount = decimal.Parse(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
            catch { }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            try
            {
                dataGridView2.Rows.RemoveAt(this.dataGridView2.SelectedRows[0].Index);
            }
            catch { }
            total -= amount;
            lblGrandTotal.Text = "RS. " + total;



        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            int newOrderID = 0;  // This will store the newly created order ID
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Insert into Order table and retrieve the new orderID
                    string orderInsertQuery = "INSERT INTO [Order] (customerID, date, totalAmount, status) OUTPUT INSERTED.orderID VALUES (@customerID, @date, @TotalAmount, @status);";

                    using (SqlCommand cmd = new SqlCommand(orderInsertQuery, conn))
                    {
                        int customerID = 1;  // Example value, ensure this is dynamically retrieved or validated
                        string status = "Pending";  // Example status

                        cmd.Parameters.AddWithValue("@customerID", customerID);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@TotalAmount", total);
                        cmd.Parameters.AddWithValue("@status", status);

                        // Execute and retrieve the new orderID
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            newOrderID = Convert.ToInt32(result);
                            MessageBox.Show("Order saved successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No order was saved. Check your data.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save order: " + ex.Message);
            }

            if (newOrderID > 0)  // Check if we have a valid orderID
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Begin transaction
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Insert each item in the cart into OrderDetails
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                string itemName = row.Cells[0].Value.ToString();
                                float itemPrice = float.Parse(row.Cells[1].Value.ToString());
                                int quantity = int.Parse(row.Cells[2].Value.ToString());
                                float subtotal = float.Parse(row.Cells[3].Value.ToString());

                                // First, get the itemID based on the itemName
                                SqlCommand getItemIdCmd = new SqlCommand("SELECT itemID FROM Item WHERE name = @name", conn, transaction);
                                getItemIdCmd.Parameters.AddWithValue("@name", itemName);
                                int itemID = (int)getItemIdCmd.ExecuteScalar();

                                // Now, insert into OrderDetails
                                SqlCommand orderDetailCmd = new SqlCommand("INSERT INTO OrderDetails (orderID, itemID, quantity, subtotal) VALUES (@orderID, @itemID, @quantity, @subtotal)", conn, transaction);
                                orderDetailCmd.Parameters.AddWithValue("@orderID", newOrderID);
                                orderDetailCmd.Parameters.AddWithValue("@itemID", itemID);
                                orderDetailCmd.Parameters.AddWithValue("@quantity", quantity);
                                orderDetailCmd.Parameters.AddWithValue("@subtotal", subtotal);

                                orderDetailCmd.ExecuteNonQuery();
                            }
                        }

                        // Commit transaction
                        transaction.Commit();

                        MessageBox.Show("Order details saved successfully.");


                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction on error
                        transaction.Rollback();
                        MessageBox.Show("Failed to save order details: " + ex.Message);
                    }

                }
                try
                {

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        // Insert into CustomerBill table using the new orderID
                        string billInsertQuery = "INSERT INTO CustomerBill (orderID, amount, status) VALUES (@orderID, @amount, @status);";

                        using (SqlCommand cmd = new SqlCommand(billInsertQuery, conn))
                        {
                            decimal amount = total;  // Assuming 'total' is a decimal and already defined
                            string status = "Not Paid";  // Default status, could be dynamic

                            cmd.Parameters.AddWithValue("@orderID", newOrderID);
                            cmd.Parameters.AddWithValue("@amount", amount);
                            cmd.Parameters.AddWithValue("@status", status);

                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                MessageBox.Show("Bill saved successfully. Rows affected: " + result);
                            }
                            else
                            {
                                MessageBox.Show("Failed to save bill. Check your data.");
                            }
                        }
                    }

                
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to save bill: " + ex.Message);
                }

                PrintOrder(); // Call the print function
            }
            else
            {
                MessageBox.Show("Order ID not generated. Bill cannot be saved.");
            }

            // Optionally reset the total and other relevant fields after saving/printing
            total = 0;
            lblGrandTotal.Text = "RS. " + total.ToString("0.00");
        }

        private void PrintOrder()
        {
            DGVPrinterHelper.DGVPrinter printer = new DGVPrinterHelper.DGVPrinter();
            printer.Title = "Customer BILL";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.ToShortDateString());
            printer.Footer = "Total Payable Amount is: " + lblGrandTotal.Text;
            printer.FooterSpacing = 15;
            printer.PrintDataGridView(dataGridView2);

            // Optionally reset the total and other relevant fields after saving/printing
            total = 0;
            lblGrandTotal.Text = "RS. " + total.ToString("0.00");


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
        private void button9_Click(object sender, EventArgs e)
        {
            Customer_Dashboard dashboard = new Customer_Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

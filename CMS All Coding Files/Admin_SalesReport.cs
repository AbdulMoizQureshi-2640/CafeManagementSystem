using DGVPrinterHelper;
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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace CafeManagementSystem
{
    public partial class Admin_SalesReport : Form
    {
        private function fn = new function();
        private string query;
        private DataSet ds;

        public Admin_SalesReport()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Admin_SalesReport_Load(object sender, EventArgs e)
        {
            LoadSalesByItem();
            LoadTotalSales();
            LoadFeedbackTrend();
            LoadSalesByCategory();
            LoadSalesOfJune2023();

            // Set tags for DataGridViews
            dataGridView1.Tag = "Sales By Item";
            dataGridView2.Tag = "Total Sales";
            dataGridView3.Tag = "Feedback Trend";
            dataGridView4.Tag = "Sales By Category";
            dataGridView5.Tag = "Sales Of June 2023";
        }

        private void LoadSalesByItem()
        {
            query = "\r\nSELECT i.name AS ItemName, SUM(od.quantity) AS TotalQuantity, SUM(od.subtotal) AS TotalRevenue\r\nFROM OrderDetails od\r\nJOIN Item i ON od.itemID = i.itemID\r\nGROUP BY i.name;\r\n";
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
        private void LoadTotalSales()
        {
            query = "SELECT SUM(totalAmount) AS TotalSales\r\nFROM [Order];\r\n";
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
        private void LoadFeedbackTrend()
        {
            query = "SELECT rating AS Rating, COUNT(*) AS No_of_Feedbacks\r\nFROM Feedback\r\nGROUP BY rating;\r\n";
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

        private void LoadSalesByCategory()
        {
            query = "\r\nSELECT c.name AS category, SUM(od.quantity) AS TotalQuantity, SUM(od.subtotal) AS TotalRevenue\r\nFROM OrderDetails od\r\nJOIN Item i ON od.itemID = i.itemID\r\nJOIN Category c ON i.categoryID = c.categoryID\r\nGROUP BY c.name;\r\n";
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



        private void LoadSalesOfJune2023()
        {
            query = "\r\nSELECT YEAR(o.date) AS Year, MONTH(o.date) AS Month, SUM(od.subtotal) AS TotalRevenue\r\nFROM [Order] o\r\nJOIN OrderDetails od ON o.orderID = od.orderID\r\nWHERE YEAR(o.date) = 2023\r\n  AND MONTH(o.date) = 6\r\nGROUP BY YEAR(o.date), MONTH(o.date);";
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
        
        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 || dataGridView2.Rows.Count > 0 ||
                dataGridView3.Rows.Count > 0 || dataGridView4.Rows.Count > 0 ||
                dataGridView5.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Sales_Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf"; // Include date and time in the file name
                bool ErrorMessage = false;

                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Unable to write data to disk: " + ex.Message);
                        }
                    }

                    if (!ErrorMessage)
                    {
                        try
                        {
                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();

                                // Add blank paragraph with some spacing
                                document.Add(new Paragraph(" "));
                                // Add report heading
                                document.Add(new Paragraph("Sales Report"));
                                // Add current date and time
                                document.Add(new Paragraph("Date and Time: " + DateTime.Now.ToString()));
                                // Add blank paragraph with some spacing
                                document.Add(new Paragraph(" "));
                                document.Add(new Paragraph(" "));

                                // Add table data
                                AddTableToDocument(dataGridView1, document);
                                // Add blank paragraph with some spacing
                                document.Add(new Paragraph(" "));
                                document.Add(new Paragraph(" "));

                                AddTableToDocument(dataGridView2, document);
                                // Add blank paragraph with some spacing
                                document.Add(new Paragraph(" "));
                                document.Add(new Paragraph(" "));

                                AddTableToDocument(dataGridView3, document);
                                // Add blank paragraph with some spacing
                                document.Add(new Paragraph(" "));
                                document.Add(new Paragraph(" "));

                                AddTableToDocument(dataGridView4, document);
                                // Add blank paragraph with some spacing
                                document.Add(new Paragraph(" "));
                                document.Add(new Paragraph(" "));

                                AddTableToDocument(dataGridView5, document);
                                // Add blank paragraph with some spacing
                                document.Add(new Paragraph(" "));
                                document.Add(new Paragraph(" "));

                                document.Close();
                                fileStream.Close();
                            }

                            MessageBox.Show("Report Downloaded Successfully", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while exporting Data: " + ex.Message, "Error");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Records Found", "Info");
            }
        }
        
        private void AddTableToDocument(DataGridView dataGridView, Document document)
        {
            if (dataGridView.Rows.Count > 0)
            {
                // Add table heading
                document.Add(new Paragraph(dataGridView.Tag.ToString())); // Use tag to identify table
                document.Add(new Paragraph(" "));

                PdfPTable pTable = new PdfPTable(dataGridView.Columns.Count);
                pTable.DefaultCell.Padding = 2;
                pTable.WidthPercentage = 100;
                pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                // Add column headers to the PDF table
                foreach (DataGridViewColumn col in dataGridView.Columns)
                {
                    PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                    pTable.AddCell(pCell);
                }

                // Add rows and cells to the PDF table
                foreach (DataGridViewRow viewRow in dataGridView.Rows)
                {
                    foreach (DataGridViewCell dcell in viewRow.Cells)
                    {
                        if (dcell.Value != null)
                            pTable.AddCell(dcell.Value.ToString());
                        else
                            pTable.AddCell(string.Empty); // Handle null values
                    }
                }

                document.Add(pTable);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();

            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_AddUser admin_AddUser = new Admin_AddUser();
            admin_AddUser.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_UpdateUser admin_UpdateUser = new Admin_UpdateUser();
            admin_UpdateUser.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_DeleteUser admin_DeleteUser = new Admin_DeleteUser();
            admin_DeleteUser.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_AddTable admin_AddTable   = new Admin_AddTable();
            admin_AddTable.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_UpdateTable admin_UpdateTable = new Admin_UpdateTable();
            admin_UpdateTable.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_DeleteTable admin_DeleteTable = new Admin_DeleteTable();
            admin_DeleteTable.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_ManageInventory admin_ManageInventory = new Admin_ManageInventory();
            admin_ManageInventory.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_LoginForm admin_LoginForm = new Admin_LoginForm();
            admin_LoginForm.Show();
        }
    }
}

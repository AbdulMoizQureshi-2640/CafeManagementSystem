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
    public partial class LoadingPage : Form
    {
        string check;
        public LoadingPage(string check)
        {
            InitializeComponent();
            this.check = check;
            progressBar1.ForeColor = Color.DimGray;
         }

        private void timer1_Tick(object sender, EventArgs e)
        {
            


            progressBar1.Increment(2);
            if (progressBar1.Value == 100)
            {
                this.Hide();

                timer1.Stop();
                if(check=="Admin")
                {
                    AdminDashboard adminDashboard = new AdminDashboard();
                    adminDashboard.Show();
                }
                else if (check=="Staff")
                {
                    Staff_Dashboard staff_Dashboard = new Staff_Dashboard();
                    staff_Dashboard.Show();
                }

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadingPage_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}

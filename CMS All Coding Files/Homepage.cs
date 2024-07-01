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
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Admin_LoginForm loginForm = new Admin_LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            Staff_LoginForm staff_LoginForm = new Staff_LoginForm();
            staff_LoginForm.Show();
            this.Hide();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            Customer_LoginForm customer_SignUpForm = new Customer_LoginForm();
            customer_SignUpForm.Show();
            this.Hide();
        }
    }
}

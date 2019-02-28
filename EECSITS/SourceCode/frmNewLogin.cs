using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewITS
{
    public partial class frmNewLogin : Form
    {
        DBA db = DBA.getInstance;
        public frmNewLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string errMsg = "";
            int rc;

            if (tbLogin.Text == "")
            {
                MessageBox.Show("Login Name is required");
                return;
            }

            if (tbPword.Text == "")
            {
                MessageBox.Show("Password is required");
                return;
            }

            if (tbCPword.Text == "")
            {
                MessageBox.Show("Confirm Password is required");
                return;
            }

            if (tbFName.Text == "")
            {
                MessageBox.Show("First Name is required");
                return;
            }

            if (tbLName.Text == "")
            {
                MessageBox.Show("Last Name is required");
                return;
            }

            if (tbEmail.Text == "")
            {
                MessageBox.Show("Email is required");
                return;
            }

            rc = db.newLoginAcct(tbLogin.Text, tbPword.Text, tbCPword.Text, checkAdmin.Checked, 
                tbFName.Text, tbLName.Text, tbEmail.Text, tbPhone.Text, ref errMsg);

            if (rc == -1)
            {
                MessageBox.Show(errMsg);
                return;
            }

            MessageBox.Show("New user added!");

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.loginUpdateToList();

            // End New Code

            this.Close();
        }
    }
}

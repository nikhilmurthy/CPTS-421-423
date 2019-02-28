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
    public partial class frmChangePW : Form
    {
        DBA db = DBA.getInstance;
        public frmChangePW()
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
            if (tbOldPword.Text == "")
            {
                MessageBox.Show("Old Password is null");
                return;
            }

            if (tbNewPword.Text == "")
            {
                MessageBox.Show("New Password is null");
                return;
            }

            if (tbCNPword.Text == "")
            {
                MessageBox.Show("Confirm New Password is null");
                return;
            }

            rc = db.changePW(Global.loginName, tbOldPword.Text, tbNewPword.Text, tbCNPword.Text, ref errMsg);

            if (rc == -1)
            {
                MessageBox.Show(errMsg);
                MessageBox.Show("Change password failed!");
                return;
            }

            MessageBox.Show("Password changed!");
            this.Close();
        }
    }
}

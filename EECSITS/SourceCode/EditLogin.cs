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
    public partial class frmEditLogin : Form
    {
        List<string>[] la_login;
        DBA db = DBA.getInstance;
        string encPwd;
        string pwd = ""; // no change to existing password

        public frmEditLogin()
        {
            InitializeComponent();
            la_login = db.selAllLogin2();
            cbLogName.DataSource = la_login[0];
        }

        public frmEditLogin(int uID) : this()
        {
            int j = 0;
            /*
            InitializeComponent();
            la_login = db.selAllLogin2();
            cbLogName.DataSource = la_login[0];
             */
            for (int i = 0; i < la_login[0].Count; i++)
            {
                if (uID == Convert.ToInt32(la_login[7][i]))
                {
                    j = i;
                    break;
                }
            }

            cbLogName.SelectedIndex = j;
            cbLogName.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string errmsg = "";
            int rc;

            if ((Global.loginName == la_login[0][cbLogName.SelectedIndex]) && (checkDelete.Checked)) // User can't delete himself
            {
                MessageBox.Show("Can't delete yourself!");
                return;
            }

            if (tbFName.Text == "")
            {
                MessageBox.Show("First name is null");
                return;
            }

            if (tbLName.Text == "")
            {
                MessageBox.Show("Last name is null");
                return;
            }

            if (encPwd != tbPword.Text)  // user changed the password
            {
                if ((tbPword.Text == "") || (tbPword.Text != tbRPword.Text))
                {
                    MessageBox.Show("Password is null or Passwords dont match");
                    return;
                }
                pwd = tbPword.Text;
            };
         
            if (tbEmail.Text == "")
            {
                MessageBox.Show("Email is null");
                return;
            }

            rc = db.editLogin(la_login[0][cbLogName.SelectedIndex], tbFName.Text, tbLName.Text,
                pwd, tbEmail.Text, tbPhone.Text, checkAdmin.Checked, checkDelete.Checked, ref errmsg);

            if (rc == -1)
            {
                MessageBox.Show(errmsg);
                return;
            }

            if (checkDelete.Checked)
                MessageBox.Show("Login Account Deleted!");
            else
                MessageBox.Show("Login Account Changed!");

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.loginUpdateToList();

            // End New Code

            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbFName.Text = la_login[1][cbLogName.SelectedIndex];
            tbLName.Text = la_login[2][cbLogName.SelectedIndex];
            tbPword.Text = la_login[3][cbLogName.SelectedIndex];
            tbRPword.Text = la_login[3][cbLogName.SelectedIndex]; // encrypted password gets displayed twice
            encPwd = la_login[3][cbLogName.SelectedIndex];
            tbEmail.Text = la_login[4][cbLogName.SelectedIndex];
            tbPhone.Text = la_login[5][cbLogName.SelectedIndex];
            checkAdmin.Checked = Convert.ToBoolean(la_login[6][cbLogName.SelectedIndex]);
        }
    }
}

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
    public partial class frmLogin : Form
    {
        public delegate void UsrLoginHandler(object source, PropertyChangedEventArgs e);
        public event UsrLoginHandler UsrLoggedIn;

        DBA db = DBA.getInstance;
        List<string>[] la_budget;
        public frmLogin()
        {
            InitializeComponent();
            la_budget = db.selAllBudget();
        }

        // New Code

        public frmLogin(int bID)
        {
            int j = 0; // Index
            InitializeComponent();
            la_budget = db.selAllBudget();
            for (int i = 0; i < la_budget[0].Count; i++)
            {
                if (bID == Convert.ToInt32(la_budget[0][i]))
                {
                    j = i;
                    break;
                }
            }
        }

        // End New Code
 
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string errMsg = "";
            int rc;

            if (tbLoginName.Text == "")
            {
                MessageBox.Show("Username is null");
                return;
            }

            rc = db.login(tbLoginName.Text, tbPassword.Text, ref errMsg);

            if (rc == -1)
            {
                MessageBox.Show("Login failed!");
                return;
            }

            Global.loginName = tbLoginName.Text;

            UsrLoginHandler handler = UsrLoggedIn;

            if (handler != null)
            {
                if (rc == 1)
                    handler(this, new PropertyChangedEventArgs("AdminLogin"));
                else
                    handler(this, new PropertyChangedEventArgs("UserLogin"));
            }

            this.Close();
        }
    }
}

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
    public partial class frmEditBudget : Form
    {
        public delegate void BudgetChangedHandler(object source, PropertyChangedEventArgs e);
        public event BudgetChangedHandler DataChanged;

        DBA db = DBA.getInstance;
        List<string>[] la_budget;
        public frmEditBudget()
        {
            InitializeComponent();
            la_budget = db.selAllBudget();
            cbNumber.DataSource = la_budget[0];
        }

        // New Code

        public frmEditBudget(int bID)
        {
            int j = 0; // Index
            InitializeComponent();
            la_budget = db.selAllBudget();
            cbNumber.DataSource = la_budget[0];
            for (int i = 0; i < la_budget[0].Count; i++)
            {
                if (bID == Convert.ToInt32(la_budget[0][i]))
                {
                    j = i;
                    break;
                }
            }

            cbNumber.SelectedIndex = j;
            cbNumber.Enabled = false;
        }

        // End New Code

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbDesc.Text = la_budget[1][cbNumber.SelectedIndex];
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "";
            int bno = Convert.ToInt32(la_budget[0][cbNumber.SelectedIndex]);

            rc = db.editBudget(bno, checkDel.Checked, tbDesc.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkDel.Checked)
                MessageBox.Show("Budget #" + bno.ToString() + " Deleted");
            else
                MessageBox.Show("Budget #" + bno.ToString() + " Field Changed");

            BudgetChangedHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("budgetEdited"));
            }

            // New Code

            frmUI fUI;
            fUI = (frmUI) this.MdiParent;
            fUI.budgetUpdateToList();

            // End New Code

            this.Close();
        }
    }
}

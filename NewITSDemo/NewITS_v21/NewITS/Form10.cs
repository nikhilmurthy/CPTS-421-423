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
            comboBox1.DataSource = la_budget[0];
        }

        // New Code

        public frmEditBudget(int bID)
        {
            int j = 0; // Index
            InitializeComponent();
            la_budget = db.selAllBudget();
            comboBox1.DataSource = la_budget[0];
            for (int i = 0; i < la_budget[0].Count; i++)
            {
                if (bID == Convert.ToInt32(la_budget[0][i]))
                {
                    j = i;
                    break;
                }
            }

            comboBox1.SelectedIndex = j;
            comboBox1.Enabled = false;
        }

        // End New Code

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = la_budget[1][comboBox1.SelectedIndex];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "";
            int bno = Convert.ToInt32(la_budget[0][comboBox1.SelectedIndex]);

            rc = db.editBudget(bno, checkBox1.Checked, textBox1.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkBox1.Checked)
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

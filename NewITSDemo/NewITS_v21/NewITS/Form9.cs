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
    public partial class frmNewBudget : Form
    {
        public delegate void BudgetChangedHandler(object source, PropertyChangedEventArgs e);
        public event BudgetChangedHandler DataChanged;

        DBA db = DBA.getInstance;

        public frmNewBudget()
        {
            InitializeComponent();
        }

        private void frmNewBudget_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string err;
            int bno;
            int rc = -1;
            string errMsg = "";

            try
            {
                bno = Convert.ToInt32(textBox1.Text);
            }

            catch 
            {
                MessageBox.Show("Invalid budget number");
                return;
            }

            if (bno <= 0)
            {
                MessageBox.Show("Invalid budget number");
                return;
            }

            rc = db.newBudget(bno, textBox2.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            MessageBox.Show("New Budget #" + bno.ToString() + " Added");

            BudgetChangedHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("budgetAdded"));
            }

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.budgetUpdateToList();

            // End New Code

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

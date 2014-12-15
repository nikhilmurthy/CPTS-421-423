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
    public partial class frmListBudget : Form
    {
        DBA db = DBA.getInstance;
        List<string>[] la_budget;

        public frmListBudget()
        {
            int rc;
            la_budget = db.selAllBudget();
            InitializeComponent();

            rc = la_budget[0].Count;
            dataGridView1.RowCount = rc;
            int i = 0;
            while (i < rc)
            {
                dataGridView1.Rows[i].Cells[0].Value = la_budget[0][i];
                dataGridView1.Rows[i].Cells[1].Value = la_budget[1][i];
                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // New Code

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUI fUI;
            int ci = e.ColumnIndex;
            int ri = e.RowIndex;
            int bID = 0;

            if (ci != 0)
                return;

            bID = Convert.ToInt32(dataGridView1.Rows[ri].Cells[0].Value.ToString());

            fUI = (frmUI)this.MdiParent;

            fUI.budgetListToEdit(bID);

            this.Close();
        }

        // End New Code
    }
}

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
            InitializeComponent();
            load_datagrid();
        }

        public void load_datagrid()
        {
            int rc;
            la_budget = db.selAllBudget();
            rc = la_budget[0].Count;
            dataGridView1.RowCount = rc;
            int i = 0;
            while (i < rc)
            {
                dataGridView1.Rows[i].Cells[0].Value = la_budget[0][i];
                dataGridView1.Rows[i].Cells[1].Value = la_budget[1][i];
                i++;
            }

            dataGridView1.Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
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

            if ((ci != 0) || (ri < 0) || (Global.loginState != Global.usrAdmin))
                return;

            bID = Convert.ToInt32(dataGridView1.Rows[ri].Cells[0].Value.ToString());

            fUI = (frmUI)this.MdiParent;

            fUI.budgetListToEdit(bID);
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex >= 0) && (Global.loginState == Global.usrAdmin))
            {
                dataGridView1.Cursor = Cursors.Hand;
                dataGridView1.Rows[e.RowIndex].Cells[0].ToolTipText = "Click to edit this row";
            }

            else
                dataGridView1.Cursor = Cursors.Default;
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                dataGridView1.Cursor = Cursors.Default;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string rmsg;

            Button btn = sender as Button;

            if (btn.Name == "btnExportCSV")
                rmsg = Global.exportToCsvFile(dataGridView1, "BudgetList", "");
            else
                rmsg = Global.exportToPdfFile(dataGridView1, "BudgetList", "");

            if (rmsg != null)
                MessageBox.Show(rmsg + " file created sucessfully");
            else
                MessageBox.Show(" Unable to export to a file");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        // End New Code
    }
}

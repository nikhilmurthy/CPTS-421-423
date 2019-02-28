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
    public partial class frmLoginList : Form
    {
        DBA db = DBA.getInstance;
        List<string>[] la_login;

        public frmLoginList()
        {
            InitializeComponent();
            load_datagrid();
        }

        private void load_datagrid()
        {
            int rc;

            la_login = db.selAllLogin();
            rc = la_login[0].Count;
            if (rc == 0)
            {
                dataGridView1.Visible = false;
                return;
            }
            dataGridView1.Visible = true;
            dataGridView1.RowCount = rc;
            int i = 0;
            while (i < rc)
            {
                dataGridView1.Rows[i].Cells[0].Value = la_login[0][i];
                dataGridView1.Rows[i].Cells[1].Value = la_login[1][i];
                dataGridView1.Rows[i].Cells[2].Value = la_login[2][i];
                dataGridView1.Rows[i].Cells[3].Value = la_login[3][i];
                dataGridView1.Rows[i].Cells[4].Value = la_login[4][i];
                dataGridView1.Rows[i].Cells[5].Value = la_login[5][i];
                dataGridView1.Rows[i].Cells[6].Value = la_login[6][i];
                dataGridView1.Rows[i].Cells[7].Value = la_login[7][i];
                i++;
            }
            dataGridView1.Refresh();

            if (rc > 0)
                btnExportCSV.Visible = true;
            else
                btnExportCSV.Visible = false;
        }

        // Handle Click on Cancel Button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUI fUI;
            int ci = e.ColumnIndex;
            int ri = e.RowIndex;
            int uID = 0;

            if ((ci != 0) || (ri < 0))
                return;

            uID = Convert.ToInt32(dataGridView1.Rows[ri].Cells[0].Value.ToString());

            fUI = (frmUI)this.MdiParent;

            fUI.loginListToEdit(uID);

  //        this.Close();
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex >= 0))
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
                dataGridView1.Cursor = Cursors.Default;
        }

        // Handle Click on Refresh Button; reload the data from database

        private void btnRefresh_Click(object sender, EventArgs e)  
        {
            load_datagrid();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string rmsg;
            string filter;
            filter = "Filter:\tNone" + "\r\n\n";

            Button btn = sender as Button;

            if (btn.Name == "btnExportCSV")
                rmsg = Global.exportToCsvFile(dataGridView1, "LoginList", filter);
            else
                rmsg = Global.exportToPdfFile(dataGridView1, "LoginList", filter);

            if (rmsg != null)
                MessageBox.Show(rmsg + " file created sucessfully");
            else
                MessageBox.Show(" Unable to export to a file");

        }
    }
}

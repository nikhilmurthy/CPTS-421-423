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
    public partial class frmListDept : Form
    {
        DBA db = DBA.getInstance;
        List<string>[] la_dept;

        public frmListDept()
        {

            int rc;
            la_dept = db.selAllDept();

            InitializeComponent();

            rc = la_dept[0].Count;
            dataGridView1.RowCount = rc;
            int i = 0;
            while (i < rc)
            {
                dataGridView1.Rows[i].Cells[0].Value = la_dept[2][i];
                dataGridView1.Rows[i].Cells[1].Value = la_dept[0][i];
                dataGridView1.Rows[i].Cells[2].Value = la_dept[1][i];
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
            int dID = 0;

            if (ci != 0)
                return;

            dID = Convert.ToInt32(dataGridView1.Rows[ri].Cells[0].Value.ToString());

            fUI = (frmUI)this.MdiParent;

            fUI.deptListToEdit(dID);

            this.Close();
        }

        // End New Code
    }
}

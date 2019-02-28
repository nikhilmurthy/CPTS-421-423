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
    public partial class frmEditDepartment : Form
    {
        public delegate void DeptChangedHandler(object source, PropertyChangedEventArgs e);
        public event DeptChangedHandler DataChanged;

        DBA db = DBA.getInstance;
        List<string>[] la_dept;

        public frmEditDepartment()
        {
            InitializeComponent();
            la_dept = db.selAllDept();
            cbCAbbr.DataSource = la_dept[0];
        }

        public frmEditDepartment(int dID)
        {
            int j = 0; // Index
            InitializeComponent();
            la_dept = db.selAllDept();
            cbCAbbr.DataSource = la_dept[0];
            for (int i = 0; i < la_dept[0].Count; i++)
            {
                if (dID == Convert.ToInt32(la_dept[2][i]))
                {
                    j = i;
                    break;
                }
            }

            cbCAbbr.SelectedIndex = j;
            cbCAbbr.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "";

            string abbr = la_dept[0][cbCAbbr.SelectedIndex];

            rc = db.editDept(abbr, checkDel.Checked, tbNAbbr.Text, tbName.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkDel.Checked)
                MessageBox.Show("Department " + abbr + " Deleted");
            else
                MessageBox.Show("Department " + abbr + " Changed");

            DeptChangedHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("departmentEdited"));
            }

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.deptUpdateToList();

            // End New Code

            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbNAbbr.Text = la_dept[0][cbCAbbr.SelectedIndex];
            tbName.Text = la_dept[1][cbCAbbr.SelectedIndex];
        }
    }
}

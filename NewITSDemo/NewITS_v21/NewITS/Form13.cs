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
            comboBox1.DataSource = la_dept[0];
        }

        public frmEditDepartment(int dID)
        {
            int j = 0; // Index
            InitializeComponent();
            la_dept = db.selAllDept();
            comboBox1.DataSource = la_dept[0];
            for (int i = 0; i < la_dept[0].Count; i++)
            {
                if (dID == Convert.ToInt32(la_dept[2][i]))
                {
                    j = i;
                    break;
                }
            }

            comboBox1.SelectedIndex = j;
            comboBox1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "";

            string abbr = la_dept[0][comboBox1.SelectedIndex];

            rc = db.editDept(abbr, checkBox1.Checked, textBox1.Text, textBox2.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkBox1.Checked)
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
            textBox1.Text = la_dept[0][comboBox1.SelectedIndex];
            textBox2.Text = la_dept[1][comboBox1.SelectedIndex];
        }
    }
}

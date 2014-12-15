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
    public partial class frmNewDept : Form
    {
        public delegate void DeptChangedHandler(object source, PropertyChangedEventArgs e);
        public event DeptChangedHandler DataChanged;

//        DepartmentStore dpts = DepartmentStore.getInstance;
        DBA db = DBA.getInstance;

        public frmNewDept()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string err;
            int rc = -1;
            string errMsg = "";

//            dpts.NewDepartment(textBox1.Text, textBox2.Text);
//            err = db.insertDept(textBox1.Text, textBox2.Text);
            rc = db.newDept(textBox1.Text, textBox2.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            MessageBox.Show("New Department Added");

            DeptChangedHandler handler = DataChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("deptAdded"));
            }

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.deptUpdateToList();

            // End New Code

            this.Close();
        }
    }
}

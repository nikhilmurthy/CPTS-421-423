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
    public partial class frmNewCategory : Form
    {
        public delegate void CategoryChangedHandler(object source, PropertyChangedEventArgs e);
        public event CategoryChangedHandler DataChanged;

//        CategoryStore cs = CategoryStore.getInstance;

        DBA db = DBA.getInstance;
        public frmNewCategory()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string err;
            int rc = -1;
            string errMsg = "";

            //  cs.NewCategory(textBox1.Text);
//            err = db.insertCategory(textBox1.Text);
            rc = db.newCategory(textBox1.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            MessageBox.Show("New Category Added");

            CategoryChangedHandler handler = DataChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("categoryAdded"));
            }

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.catgUpdateToList();

            // End New Code

            this.Close();
        }
    }

 
}

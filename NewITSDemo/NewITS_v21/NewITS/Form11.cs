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
    public partial class frmEditCategory : Form
    {
        public delegate void CategoryChangedHandler(object source, PropertyChangedEventArgs e);
        public event CategoryChangedHandler DataChanged;

        DBA db = DBA.getInstance;
        List<string>[] la_category;
        public frmEditCategory()
        {
            InitializeComponent();
            la_category = db.selAllCategory();
            comboBox1.DataSource = la_category[0];
        }

        public frmEditCategory(int cID)
        {
            int j = 0; // Index
            InitializeComponent();
            la_category = db.selAllCategory();
            comboBox1.DataSource = la_category[0];
            for (int i = 0; i < la_category[0].Count; i++)
            {
                if (cID == Convert.ToInt32(la_category[1][i]))
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

            string name = la_category[0][comboBox1.SelectedIndex];

            rc = db.editCategory(name, checkBox1.Checked, textBox1.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkBox1.Checked)
                MessageBox.Show("Category" + name + " Deleted");
            else
                MessageBox.Show("Category Changed");

            CategoryChangedHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("categoryEdited"));
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

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
    public partial class frmEditVendor : Form
    {
        public delegate void VendorChangedHandler(object source, PropertyChangedEventArgs e);
        public event VendorChangedHandler DataChanged;

        DBA db = DBA.getInstance;
        List<string>[] la_vendor;

        public frmEditVendor()
        {
            InitializeComponent();
            la_vendor = db.selAllVendor();
            comboBox1.DataSource = la_vendor[0];
        }

        public frmEditVendor(int vID)
        {
            int j = 0; // Index
            InitializeComponent();
            la_vendor = db.selAllVendor();
            comboBox1.DataSource = la_vendor[0];
            for (int i = 0; i < la_vendor[0].Count; i++)
            {
                if (vID == Convert.ToInt32(la_vendor[5][i]))
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = la_vendor[0][comboBox1.SelectedIndex];
            textBox2.Text = la_vendor[1][comboBox1.SelectedIndex];
            textBox3.Text = la_vendor[2][comboBox1.SelectedIndex];
            textBox4.Text = la_vendor[3][comboBox1.SelectedIndex];
            textBox5.Text = la_vendor[4][comboBox1.SelectedIndex];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "";

            string name = la_vendor[0][comboBox1.SelectedIndex];

            rc = db.editVendor(name, checkBox1.Checked, textBox1.Text, textBox2.Text, 
                textBox3.Text, textBox4.Text, textBox5.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkBox1.Checked)
                MessageBox.Show("Vendor" + name + " Deleted");
            else
                MessageBox.Show("Vendor" + name + "Changed");

            VendorChangedHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("vendorEdited"));
            }

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.vendorUpdateToList();

            // End New Code

            this.Close();
        }
    }
}

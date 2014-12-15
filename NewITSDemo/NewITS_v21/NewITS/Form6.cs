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
    public partial class frmNewVendor : Form
    {
        public delegate void VendorChangedHandler(object source, PropertyChangedEventArgs e);
        public event VendorChangedHandler DataChanged;

        VendorStore vs = VendorStore.getInstance;
        DBA db = DBA.getInstance;

        public frmNewVendor()
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

    //        vs.NewVendor(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
//            err = db.insertVendor(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
            rc = db.newVendor(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            MessageBox.Show("New Vendor Added");

            VendorChangedHandler handler = DataChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("vendorAdded"));
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

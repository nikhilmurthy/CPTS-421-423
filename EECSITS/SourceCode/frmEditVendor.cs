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
            cbCName.DataSource = la_vendor[0];
        }

        public frmEditVendor(int vID)
        {
            int j = 0; // Index
            InitializeComponent();
            la_vendor = db.selAllVendor();
            cbCName.DataSource = la_vendor[0];
            for (int i = 0; i < la_vendor[0].Count; i++)
            {
                if (vID == Convert.ToInt32(la_vendor[5][i]))
                {
                    j = i;
                    break;
                }
            }

            cbCName.SelectedIndex = j;
            cbCName.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbNewName.Text = la_vendor[0][cbCName.SelectedIndex];
            tbAddress.Text = la_vendor[1][cbCName.SelectedIndex];
            tbPhone.Text = la_vendor[2][cbCName.SelectedIndex];
            tbFax.Text = la_vendor[3][cbCName.SelectedIndex];
            tbWebsite.Text = la_vendor[4][cbCName.SelectedIndex];
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "";

            string name = la_vendor[0][cbCName.SelectedIndex];

            rc = db.editVendor(name, checkDel.Checked, tbNewName.Text, tbAddress.Text, 
                tbPhone.Text, tbFax.Text, tbWebsite.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkDel.Checked)
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

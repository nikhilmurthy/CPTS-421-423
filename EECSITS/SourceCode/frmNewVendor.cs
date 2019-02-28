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

        DBA db = DBA.getInstance;

        public frmNewVendor()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "";

            rc = db.newVendor(tbName.Text, tbAddress.Text, tbPhone.Text, tbFax.Text, tbWebsite.Text, ref errMsg);

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

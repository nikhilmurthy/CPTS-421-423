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
    public partial class frmEditInventory : Form
    {
        DBA db = DBA.getInstance;

        List<string>[] la_category;
        List<string>[] la_building;
        List<string>[] la_dept;
        List<string>[] la_vendor;
        List<string>[] la_owner;
        List<string>[] la_lineitem;
        List<string>[] la_orderNum;
        List<string>[] la_inv;

        public frmEditInventory()
        {
            la_dept = db.selAllDept();
            la_building = db.selAllBuilding();
            la_category = db.selAllCategory();
            la_vendor = db.selAllVendor();
            la_owner = db.selAllOwner();

            InitializeComponent();
            cbDept.DataSource = la_dept[0];
            cbBuild.DataSource = la_building[0];
            cbCategory.DataSource = la_category[0];
            cbVendor.DataSource = la_vendor[0];
            cbOwner.DataSource = la_owner[4];
        }

        public frmEditInventory(int barcode)
        {
            la_dept = db.selAllDept();
            la_building = db.selAllBuilding();
            la_category = db.selAllCategory();
            la_vendor = db.selAllVendor();
            la_owner = db.selAllOwner();

            InitializeComponent();
            cbDept.DataSource = la_dept[0];
            cbBuild.DataSource = la_building[0];
            cbCategory.DataSource = la_category[0];
            cbVendor.DataSource = la_vendor[0];
            cbOwner.DataSource = la_owner[4];
            tbID.Text = Convert.ToString(barcode);
            tbID.Enabled = false;
            loadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "", purchaseDate = "";
            int bc;
            int catID, buildID, ownerID, vendorID, deptID;
            double purchasePrice;

            catID = Convert.ToInt32(la_category[1][cbCategory.SelectedIndex]);
            buildID = Convert.ToInt32(la_building[2][cbBuild.SelectedIndex]);
            ownerID = Convert.ToInt32(la_owner[5][cbOwner.SelectedIndex]);
            vendorID = Convert.ToInt32(la_vendor[5][cbVendor.SelectedIndex]);
            deptID = Convert.ToInt32(la_dept[2][cbDept.SelectedIndex]);

            try
            {
                bc = Convert.ToInt32(tbID.Text);
            }

            catch
            {
                MessageBox.Show("Invalid barcode number");
                return;
            }

            try
            {
                purchasePrice = Convert.ToDouble(tbPrice.Text);
            }

            catch
            {
                MessageBox.Show("Invalid purchase price");
                return;
            }

            // Set purchase date in correct form

            purchaseDate = tbPurDate.Text;
            purchaseDate = DateTime.Parse(purchaseDate).ToString("yyyy-MM-dd");
            tbPurDate.Text = purchaseDate;

            rc = db.editInventory(bc, checkDel.Checked, tbDesc.Text, catID, buildID, tbRoomNo.Text, ownerID, vendorID, deptID, tbPurDate.Text,
                purchasePrice, tbManf.Text, tbVouchNo.Text, tbSerialNo.Text, tbModelNo.Text, tbComments.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkDel.Checked)
                MessageBox.Show("Inventory " + bc + " Deleted");
            else
                MessageBox.Show("Inventory " + bc + " Changed");

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.invUpdateToList(bc);

            // End New Code

            this.Close();
        }

        private void tbID_Leave(object sender, EventArgs e)
        {
            int barcode;
            string deliveryDate = "";

            try
            {
                barcode = Convert.ToInt32(tbID.Text);
            }

            catch
            {
                MessageBox.Show("Barcode invalid");
                return;
            }


            la_inv = db.selInvOne(barcode);

            if (la_inv[0].Count() == 0)
            {
                //  Inventory not found

                MessageBox.Show("Barcode doesn't exist or marked as deleted");
                return;
            }

            tbOrderNo.Text = la_inv[0][0];
            tbDelDate.Text = la_inv[1][0]; // Delivery date
            tbPurDate.Text = la_inv[2][0]; // Purchase date

            // Set delivery date in correct form

            deliveryDate = tbDelDate.Text;
            deliveryDate = DateTime.Parse(deliveryDate).ToString("yyyy-MM-dd");
            tbDelDate.Text = deliveryDate;

            tbPrice.Text = la_inv[3][0];
            tbDesc.Text = la_inv[4][0];
            tbManf.Text = la_inv[5][0];
            tbSerialNo.Text = la_inv[7][0];
            tbModelNo.Text = la_inv[8][0];
            tbRoomNo.Text = la_inv[9][0];
            tbVouchNo.Text = la_inv[6][0];
            tbComments.Text = la_inv[10][0];

            handleOrderEntry();
        }

        private void loadData()
        {
            int barcode;
            string deliveryDate = "";

            try
            {
                barcode = Convert.ToInt32(tbID.Text);
            }

            catch
            {
                MessageBox.Show("Barcode invalid");
                return;
            }


            la_inv = db.selInvOne(barcode);

            if (la_inv[0].Count() == 0)
            {
                //  Inventory not found

                MessageBox.Show("Barcode doesn't exist or marked as deleted");
                return;
            }

            tbOrderNo.Text = la_inv[0][0];
            tbDelDate.Text = la_inv[1][0]; // Delivery date
            tbPurDate.Text = la_inv[2][0]; // Purchase date

            // Set delivery date in correct form

            deliveryDate = tbDelDate.Text;
            deliveryDate = DateTime.Parse(deliveryDate).ToString("yyyy-MM-dd");
            tbDelDate.Text = deliveryDate;

            tbPrice.Text = la_inv[3][0];
            tbDesc.Text = la_inv[4][0];
            tbManf.Text = la_inv[5][0];
            tbSerialNo.Text = la_inv[7][0];
            tbModelNo.Text = la_inv[8][0];
            tbRoomNo.Text = la_inv[9][0];
            tbVouchNo.Text = la_inv[6][0];
            tbComments.Text = la_inv[10][0];

            handleOrderEntry();
        }

        private void handleOrderEntry()
        {
            int ono;

            try
            {
                ono = Convert.ToInt32(tbOrderNo.Text);
            }

            catch
            {
                MessageBox.Show("Order invalid");
                return;
            }

            la_orderNum = db.selOrdersOne(ono);

            if (la_orderNum[0].Count() == 0)
            {
                //  Order not found

                MessageBox.Show("Order doesn't exist");
                return;
            }

            tbPurDate.Text = la_orderNum[0][0];

            if (la_orderNum[1][0] != "")
                cbVendor.SelectedIndex = la_vendor[5].IndexOf(la_orderNum[1][0]);

            if (la_orderNum[2][0] != "")
                cbOwner.SelectedIndex = la_owner[5].IndexOf(la_orderNum[2][0]);

            la_lineitem = db.selLineItemOne(ono);

            if (la_lineitem[0].Count() == 0)
            {
                //  Line items not found

                MessageBox.Show("No line items for this order");
                return;
            }

            dgLineItem.RowCount = 10;
            int i = 0;

            for (int j = 0; j < 7; j++)
            {
                i = 0;
                foreach (string item in la_lineitem[j])
                {
                    dgLineItem.Rows[i].Cells[j].Value = item;
                    i++;
                }
            };

            dgLineItem.RowCount = i;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbEmail.Text = la_owner[0][cbOwner.SelectedIndex];
        }
    }
}

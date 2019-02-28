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
    public partial class frmNewInventory : Form
    {
        DBA db = DBA.getInstance;

        List<string>[] la_category;
        List<string>[] la_building;
        List<string>[] la_dept;
        List<string>[] la_vendor;
        List<string>[] la_owner;

        List<string> la_order;
        List<string>[] la_lineitem;
        List<string>[] la_orderNum;
    
        int lineNoSave = 0;
        bool validOrder = false;


        public frmNewInventory()
        {
            la_dept = db.selAllDept();
            la_building = db.selAllBuilding();
            la_category = db.selAllCategory();
            la_vendor = db.selAllVendor();
            la_owner = db.selAllOwner();
            la_order = db.selOrdersZero();

            InitializeComponent();

  //        tbDate.Text = DateTime.Now.ToString("yyyy-M-d");
            tbDate.Text = DateTime.Now.ToString("M/d/yyyy");
            cbDept.DataSource = la_dept[0];
            cbBuild.DataSource = la_building[0];
            cbCategory.DataSource = la_category[0];
            cbVendor.DataSource = la_vendor[0];
            cbOwner.DataSource = la_owner[4];

        }


        public void frmNewInventoryDropdown()
        {
            la_order = db.selOrdersZero();

            la_dept = db.selAllDept();
            cbDept.DataSource = la_dept[0];

            la_building = db.selAllBuilding();
            cbBuild.DataSource = la_building[0];

            la_category = db.selAllCategory();
            cbCategory.DataSource = la_category[0];

            la_vendor = db.selAllVendor();
            cbVendor.DataSource = la_vendor[0];

            la_owner = db.selAllOwner();
            cbOwner.DataSource = la_owner[4];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
         */
      

        private void dgLineItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ci = e.ColumnIndex;
            int ri = e.RowIndex;

            if ((ci == 0) && (dgLineItem.Rows[ri].Cells[0].ReadOnly == false))
            {

                DataGridViewCheckBoxCell cb;

                if (lineNoSave > 0)
                {
                    cb = dgLineItem.Rows[lineNoSave - 1].Cells[0] as DataGridViewCheckBoxCell;

                    if (lineNoSave == (ri + 1))
                    {
                        lineNoSave = 0;
                        tbDesc.Text = null;
                        tbPrice.Text = null;
                        return;
                    }

                    cb.Value = false;
                }

                lineNoSave = Convert.ToInt32(dgLineItem.Rows[ri].Cells[1].Value.ToString());


                tbDesc.Text = dgLineItem.Rows[ri].Cells[3].Value.ToString(); // description
                tbPrice.Text = dgLineItem.Rows[ri].Cells[6].Value.ToString(); // price
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int invID = -1;
            int vendorID;
            int deptID;
            int catgID;
            int bldgID;
            int ownerID;
            int orderID;
            int lineNo;

            double purchasePrice = 0.0;

            string deliveryDate = "", purchaseDate = "";

            string errmsg = "";

            try
            {
                invID = Convert.ToInt32(tbID.Text);
            }

            catch
            {
                MessageBox.Show("Invalid inventory id");
                return;
            }

            if (invID <= 0)
            {
                MessageBox.Show("Invalid inventory id");
                return;
            }

            if (validOrder)
            {
                orderID = Convert.ToInt32(tbOrderNo.Text);
            }
            else
            {
                MessageBox.Show("Invalid order number");
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

            if (purchasePrice <= 0)
            {
                MessageBox.Show("Invalid purchase price");
                return;
            }

            catgID = Convert.ToInt32(la_category[1][cbCategory.SelectedIndex]);
            deptID = Convert.ToInt32(la_dept[2][cbDept.SelectedIndex]);
            vendorID = Convert.ToInt32(la_vendor[5][cbVendor.SelectedIndex]);
            bldgID = Convert.ToInt32(la_building[2][cbBuild.SelectedIndex]);
            ownerID = Convert.ToInt32(la_owner[5][cbOwner.SelectedIndex]);
/*
            try
            {
                orderID = Convert.ToInt32(tbOrderNo.Text);
            }

            catch
            {
                MessageBox.Show("Invalid order number");
                return;
            }
 

            if (orderID <= 0)
            {
                MessageBox.Show("Order number not valid");
                return;
            }
 */

            lineNo = lineNoSave;

  //        deliveryDate = tbDate.Text;
            deliveryDate = DateTime.Now.ToString("yyyy-MM-dd");

            purchaseDate = Convert.ToString(tbPurDate.Text);

            purchaseDate = DateTime.Parse(purchaseDate).ToString("yyyy-MM-dd");

            invID = db.newInventory(invID, tbDesc.Text, catgID, bldgID, tbRoomNo.Text, ownerID, orderID, lineNo, vendorID,
                           deptID, deliveryDate, purchaseDate, purchasePrice, tbManf.Text,
                           tbVouchNo.Text, tbSerialNo.Text, tbModelNo.Text, tbComments.Text, ref errmsg);

            if (invID <= 0)
            {
                MessageBox.Show(errmsg);
                return;
            }

            else
            {
                MessageBox.Show("INV #" + invID.ToString() + " Created");

                // New Code

                frmUI fUI;

                fUI = (frmUI)this.MdiParent;

                fUI.invUpdateToList(invID);

                // End New Code

                this.Close();
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            handleOrderEntry();
        }

        // New Code

        public void loadData()
        {
            handleOrderEntry();
        }
        private void clearOrderEntry()

        {
            dgLineItem.RowCount = 0;
            tbPurDate.Text = "";
            lineNoSave = 0;
            tbDesc.Text = "";
            tbPrice.Text = "";
 
        }
        // End New Code

        private void handleOrderEntry()
        {
            int ono;

            validOrder = false;
            try
            {
                ono = Convert.ToInt32(tbOrderNo.Text);
            }

            catch
            {
                MessageBox.Show("Order invalid");
                clearOrderEntry();
                return;
            }

            la_orderNum = db.selOrdersOne(ono);

            if (la_orderNum[0].Count() == 0)
            {
                //  Order not found

                MessageBox.Show("Order doesn't exist");
                clearOrderEntry();
                return;
            }


            tbPurDate.Text = la_orderNum[0][0];

            if (la_orderNum[1][0] != "")
                cbVendor.SelectedIndex = la_vendor[5].IndexOf(la_orderNum[1][0]);

            if (la_orderNum[2][0] != "")
            {
                cbOwner.SelectedIndex = la_owner[5].IndexOf(la_orderNum[2][0]);

                cbBuild.SelectedIndex = la_building[2].IndexOf(la_owner[6][cbOwner.SelectedIndex]);
                tbRoomNo.Text = la_owner[7][cbOwner.SelectedIndex];

            }

            la_lineitem = db.selLineItemOne(ono);

            if (la_lineitem[0].Count() == 0)
            {
                //  Line items not found

                MessageBox.Show("No line items for this order");
                return;
            }

            dgLineItem.RowCount = 10;
            int i = 0;

            for (int j = 0; j < 8; j++)
            {
                i = 0;
                foreach (string item in la_lineitem[j])
                {
                    dgLineItem.Rows[i].Cells[j + 1].Value = item;
                    i++;
                }
            };
            int k = i;
            dgLineItem.RowCount = i;
            i = 0;
            while (i < k)
            {
                bool b;
                DataGridViewCheckBoxCell cb = (DataGridViewCheckBoxCell)dgLineItem.Rows[i].Cells[9];
                b = Convert.ToBoolean(Convert.ToInt32(la_lineitem[8][i]));
                cb.Value = b;

                if (b)
                    dgLineItem.Rows[i].Cells[0].ReadOnly = true;

                i++;
            }
            validOrder = true;
        }

        private void tbOrderNo_Leave(object sender, EventArgs e)
        {
            handleOrderEntry();
        }

        private void cbOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbEmail.Text = la_owner[0][cbOwner.SelectedIndex];
        }
    }
}
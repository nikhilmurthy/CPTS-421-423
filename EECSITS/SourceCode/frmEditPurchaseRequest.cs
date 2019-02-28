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
    public partial class frmEditPurchaseRequest : Form
    {
        DBA db = DBA.getInstance;
        List<string>[] la_budget;
        List<string>[] la_vendor;
        List<string>[] la_dept;
        List<string>[] la_purchaser;
        List<string>[] la_auth;
        List<string>[] la_owner;
        List<string>[] la_faculty;
        List<string>[] la_orders;
        List<string>[] la_lineitem;

        public frmEditPurchaseRequest()
        {
            InitializeComponent();

            cbPayMethod.SelectedIndex = 0;
            cbShipping.SelectedIndex = 0;

            la_vendor = db.selAllVendor();
            cbCompName.DataSource = la_vendor[0];
            la_dept = db.selAllDept();
            la_owner = db.selAllOwner();
            la_purchaser = db.selAllPurchaser();
            la_faculty = db.selAllFaculty();
            la_auth = db.selAllAuth();
            cbDept.DataSource = la_dept[0];
            cbRequest.DataSource = la_owner[4];
            cbPurchaser.DataSource = la_purchaser[4];
            cbFaculty.DataSource = la_faculty[4];
            cbExpAuth.DataSource = la_auth[4];
            la_budget = db.selAllBudget();
            cbBudget.DataSource = la_budget[0];
        }

        public frmEditPurchaseRequest(int orderNo)
        {
            InitializeComponent();

            cbPayMethod.SelectedIndex = 0;
            cbShipping.SelectedIndex = 0;

            la_vendor = db.selAllVendor();
            cbCompName.DataSource = la_vendor[0];
            la_dept = db.selAllDept();
            la_owner = db.selAllOwner();
            la_purchaser = db.selAllPurchaser();
            la_faculty = db.selAllFaculty();
            la_auth = db.selAllAuth();
            cbDept.DataSource = la_dept[0];
            cbRequest.DataSource = la_owner[4];
            cbPurchaser.DataSource = la_purchaser[4];
            cbFaculty.DataSource = la_faculty[4];
            cbExpAuth.DataSource = la_auth[4];
            la_budget = db.selAllBudget();
            cbBudget.DataSource = la_budget[0];
            tbReqNum.Text = Convert.ToString(orderNo);
            tbReqNum.Enabled = false;
            loadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int rc = -1;
            int ownerID, purchaserID, vendorID, deptID, facultyID, authID;
            int budgetNo, reqNo;
            string errMsg = "", comment;
            int shipMode = cbShipping.SelectedIndex;
            int paymentType = cbPayMethod.SelectedIndex;

            try
            {
                reqNo = Convert.ToInt32(tbReqNum.Text);
            }

            catch
            {
                MessageBox.Show("Invalid requisition number");
                return;
            }

            ownerID = Convert.ToInt32(la_owner[5][cbPurchaser.SelectedIndex]);
            purchaserID = Convert.ToInt32(la_purchaser[5][cbRequest.SelectedIndex]);
            vendorID = Convert.ToInt32(la_vendor[5][cbCompName.SelectedIndex]);
            deptID = Convert.ToInt32(la_dept[2][cbDept.SelectedIndex]);
            authID = Convert.ToInt32(la_auth[5][cbExpAuth.SelectedIndex]);
            facultyID = Convert.ToInt32(la_faculty[5][cbFaculty.SelectedIndex]);
            budgetNo = Convert.ToInt32(la_budget[0][cbBudget.SelectedIndex]);

            comment = tbComment.Text;

            rc = db.editPurchaseRequest(reqNo, checkDel.Checked, ownerID, purchaserID, facultyID,
                   authID, vendorID, deptID, budgetNo, comment, shipMode, paymentType, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkDel.Checked)
                MessageBox.Show("Order " + reqNo + " Deleted");
            else
                MessageBox.Show("Order " + reqNo + " Changed");

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.orderUpdateToList(reqNo);

            // End New Code

            this.Close();
        }

        private void cbRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbReqPhone.Text = la_owner[3][cbRequest.SelectedIndex];
            tbEmail.Text = la_owner[0][cbRequest.SelectedIndex];
        }

        private void tbReqNum_Leave(object sender, EventArgs e)
        {
            int reqNum;

            try
            {
                reqNum = Convert.ToInt32(tbReqNum.Text);
            }

            catch
            {
                MessageBox.Show("Requisition number invalid");
                return;
            }


            la_orders = db.selOrdersOne(reqNum);

            if (la_orders[0].Count() == 0)
            {
                //  Inventory not found

                MessageBox.Show("Requisition number doesn't exist or marked as deleted");
                return;
            }

            tbDate.Text = la_orders[0][0];
            tbTotal.Text = la_orders[9][0];
            tbComment.Text = la_orders[10][0];

            handleOrderEntry(reqNum);
        }

        private void loadData()
        {
            int reqNum;

            try
            {
                reqNum = Convert.ToInt32(tbReqNum.Text);
            }

            catch
            {
                MessageBox.Show("Requisition number invalid");
                return;
            }


            la_orders = db.selOrdersOne(reqNum);

            if (la_orders[0].Count() == 0)
            {
                //  Inventory not found

                MessageBox.Show("Requisition number doesn't exist or marked as deleted");
                return;
            }

            tbDate.Text = la_orders[0][0];
            tbTotal.Text = la_orders[9][0];
            tbComment.Text = la_orders[10][0];

            handleOrderEntry(reqNum);
        }

        private void handleOrderEntry(int oNo)
        {
            la_lineitem = db.selLineItemOne(oNo);

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

        private void cbCompName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index; // Selected item index
            string vname;

            vname = cbCompName.SelectedValue.ToString();

            index = cbCompName.SelectedIndex;

            tbAddress.Text = la_vendor[1][index];
            tbPhone.Text = la_vendor[2][index];
            tbFax.Text = la_vendor[3][index];
            tbWebSite.Text = la_vendor[4][index];
        }
    }
}

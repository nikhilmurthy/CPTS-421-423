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
    public partial class frmNewPurchaseRequest : Form
    {
        DBA db = DBA.getInstance;
        List<string>[] la_budget;
        List<string>[] la_vendor;
        List<string>[] la_dept;
        List<string>[] la_purchaser;
        List<string>[] la_auth;
        List<string>[] la_owner;
        List<string>[] la_faculty;
        public frmNewPurchaseRequest()
        {
            InitializeComponent();

            cbPayMethod.SelectedIndex = 0;
            cbShip.SelectedIndex = 0;

            tbDate.Text = DateTime.Now.ToString("M/d/yyyy");

            // initialize the datagrid for lineitem

            dgLineItem.RowCount = 10;
            for (int i = 0; i < 10; i++)
            {
                dgLineItem.Rows[i].Cells[0].Value = (i + 1).ToString();
            }

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

        public void frmNewPurchaseReloadDropdown()
        {
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int i = 0; // Row index
            int numEntries = 0; // Number of entries in inventory
            int cnt; // Counts how many cells in a given row are empty

            int orderID;
            int vendorID;
            int deptID;
            int purchaserID;
            int ownerID;
            int authID;
            int facultyID;
            int shipMode;
            int budgetNo;
            int paymentType;
            double total;
            int rc = -1;
            string errMsg = "";

            List<string> catalog = new List<string>();
            List<string> desc = new List<string>();
            List<string> qty = new List<string>();
            List<string> unit = new List<string>();
            List<string> up = new List<string>();
            List<string> amt = new List<string>();

            try
            {
                orderID = Convert.ToInt32(tbDeptReq.Text);
            }

            catch
            {
                MessageBox.Show("Invalid requisition number");
                return;
            }

            if (orderID <= 0)
            {
                MessageBox.Show("Invalid requisition number");
                return;
            }


            // Check each row in the data grid view

            while (i < 10)
            {
                cnt = 0;

                // Check if all cells after index in the row are empty

                for (int j = 1; j < 6; j++)
                {
                    if (dgLineItem.Rows[i].Cells[j].Value == null)
                    {
                        cnt++;
                    };
                };

                if (cnt == 0)
                {
                    numEntries++;
                    i++;
                }

                else
                    break;

            };

            MessageBox.Show("Number Entries = " + numEntries.ToString());

            if (numEntries == 0)
                return;

            // create an order

            vendorID = Convert.ToInt32(la_vendor[5][cbCompName.SelectedIndex]);
            deptID = Convert.ToInt32(la_dept[2][cbDept.SelectedIndex]);
            facultyID = Convert.ToInt32(la_faculty[5][cbFaculty.SelectedIndex]);
            authID = Convert.ToInt32(la_auth[5][cbExpAuth.SelectedIndex]);
            purchaserID = Convert.ToInt32(la_purchaser[5][cbPurchaser.SelectedIndex]);
            ownerID = Convert.ToInt32(la_owner[5][cbRequest.SelectedIndex]);
            shipMode = cbShip.SelectedIndex;
            budgetNo = Convert.ToInt32(la_budget[0][cbBudget.SelectedIndex]);
            paymentType = cbPayMethod.SelectedIndex;

            i = 0;

            while (i < numEntries)
            {
                // Compute lineitem total using the product of quantity and unit price
                int u; // Row unit
                int q; // Row qty
                double currentQty, currentUnitPrice;

                try
                {
                    u = Convert.ToInt32(dgLineItem.Rows[i].Cells[4].Value);
                }

                catch
                {
                    MessageBox.Show("Invalid unit");
                    return;
                }

                try
                {
                    q = Convert.ToInt32(dgLineItem.Rows[i].Cells[3].Value);
                }

                catch
                {
                    MessageBox.Show("Invalid quantity");
                    return;
                }

                try
                {
                    currentUnitPrice = Convert.ToDouble(dgLineItem.Rows[i].Cells[5].Value);
                }

                catch
                {
                    MessageBox.Show("Invalid unit price");
                    return;
                }

                currentQty = (double)q;

                dgLineItem.Rows[i].Cells[5].Value = (string.Format("{0:0.00}", currentUnitPrice));

                double liTotal = currentQty * currentUnitPrice;

                dgLineItem.Rows[i].Cells[6].Value = (string.Format("{0:0.00}", liTotal));

                catalog.Add(dgLineItem.Rows[i].Cells[1].Value.ToString());
                desc.Add(dgLineItem.Rows[i].Cells[2].Value.ToString());
                qty.Add(dgLineItem.Rows[i].Cells[3].Value.ToString());
                unit.Add(dgLineItem.Rows[i].Cells[4].Value.ToString());
                up.Add(dgLineItem.Rows[i].Cells[5].Value.ToString());
                amt.Add(dgLineItem.Rows[i].Cells[6].Value.ToString());

                i++;
            }

            // Compute total of all lineitem totals

            double currentTotal = 0;

            foreach (string s in amt)
            {
                currentTotal += Convert.ToDouble(s);
            }

            tbTotal.Text = (string.Format("{0:0.00}", currentTotal));

            total = Convert.ToDouble(tbTotal.Text);

            rc = db.newPurchaseRequest(orderID, tbComment.Text, DateTime.Now.ToString("yyyy-MM-dd"), vendorID, shipMode, budgetNo,
                purchaserID, ownerID, deptID, paymentType, facultyID, authID, total, numEntries,
                                            catalog, desc, qty, unit, up, amt, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            else
            {
                MessageBox.Show("Order #" + rc.ToString() + " Created");

                // New Code

                frmUI fUI;

                fUI = (frmUI)this.MdiParent;

                fUI.orderUpdateToList(rc);

                // End New Code

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void cbRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbRPhone.Text = la_owner[3][cbRequest.SelectedIndex];
            tbEmail.Text = la_owner[0][cbRequest.SelectedIndex];
        }
    }
}
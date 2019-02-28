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
    public partial class frmListOrder : Form
    {
        DBA db = DBA.getInstance;
        List<string>[] la_requester;
        List<string>[] la_purchaser;
        List<string>[] la_auth;
        List<string>[] la_faculty;
        List<string>[] la_dept;
        List<string>[] la_vendor;
        List<string>[] la_budget;
        List<string>[] la_order;
        List<string>[] la_lineitem;

        public frmListOrder()
        {
            InitializeComponent();
            la_requester = db.selAllOwner();
            la_purchaser = db.selAllPurchaser();
            la_auth = db.selAllAuth();
            la_faculty = db.selAllFaculty();
            la_vendor = db.selAllVendor();
            la_dept = db.selAllDept();
            la_budget = db.selAllBudget();
            la_requester[4].Insert(0, "All Requesters");
            la_purchaser[4].Insert(0, "All Purchasers");
            la_auth[4].Insert(0, "All Authorities");
            la_faculty[4].Insert(0, "All Faculties");
            la_vendor[0].Insert(0, "All Vendors");
            la_dept[0].Insert(0, "All Departments");
            la_budget[2].Insert(0, "All Budgets");
            cbRequest.DataSource = la_requester[4];
            cbPurchase.DataSource = la_purchaser[4];
            cbAuth.DataSource = la_auth[4];
            cbFaculty.DataSource = la_faculty[4];
            cbDept.DataSource = la_dept[0];
            cbVendor.DataSource = la_vendor[0];
            cbBudget.DataSource = la_budget[2];

            btnExportCSV.Visible = false;
            btnExportPDF.Visible = false;

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }

            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, true);
            }

            dateTimePicker1.Value = DateTime.Today.AddMonths(-3);
            dateTimePicker2.Value = DateTime.Today;
            checkBox1.Checked = true;

            cbPendDeliv.Checked = true;
            btnSearch_Click(null, null);
        }

        public frmListOrder(int orderNo) : this()
        {

            tbOrderID.Text = Convert.ToString(orderNo);
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            cbPendDeliv.Checked = false;
            btnSearch_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int rc;
            int orderID;
            int minReqID = 0; // Requester ID Min
            int minAID = 0; // Authority ID Min
            int minPID = 0; // Purchaser ID Min
            int minBID = 0; // Budget ID Min
            int minVID = 0; // Vendor ID Min
            int minDID = 0; // Department ID Min
            int minFID = 0; // Faculty ID Min
            int minOrdID = 0; // Order ID Min

            int maxReqID = int.MaxValue;
            int maxAID = int.MaxValue;  // Authority ID Max
            int maxPID = int.MaxValue;  // Purchaser ID Max
            int maxBID = int.MaxValue;  // Budget ID Max
            int maxVID = int.MaxValue;  // Vendor ID Max
            int maxDID = int.MaxValue;  // Department ID Max
            int maxFID = int.MaxValue; // Faculty ID Max
            int maxOrdID = int.MaxValue; // Order ID Max

  //          DateTime dateFrom = dateTimePicker1.Value;
  //          DateTime dateTo = dateTimePicker2.Value;

            DateTime dateFrom = DateTime.Today.AddYears(-100);
            DateTime dateTo = DateTime.Today;

            int qtyDiff = 0;

            if (tbOrderID.Text != "")
            {
                try
                {
                    orderID = Convert.ToInt32(tbOrderID.Text);
                }

                catch
                {
                    MessageBox.Show("Invalid inventory number");
                    return;
                }

                minOrdID = orderID;
                maxOrdID = orderID;
            }

            if (checkBox2.Checked)
            {

                if (cbRequest.SelectedIndex != 0)
                {
                    minReqID = Convert.ToInt32(la_requester[5][cbRequest.SelectedIndex - 1]);
                    maxReqID = Convert.ToInt32(la_requester[5][cbRequest.SelectedIndex - 1]);
                }

                if (cbDept.SelectedIndex != 0)
                {
                    minDID = Convert.ToInt32(la_dept[2][cbDept.SelectedIndex - 1]);
                    maxDID = Convert.ToInt32(la_dept[2][cbDept.SelectedIndex - 1]);
                }

                if (cbVendor.SelectedIndex != 0)
                {
                    minVID = Convert.ToInt32(la_vendor[5][cbVendor.SelectedIndex - 1]);
                    maxVID = Convert.ToInt32(la_vendor[5][cbVendor.SelectedIndex - 1]);
                }

                if (cbPurchase.SelectedIndex != 0)
                {
                    minPID = Convert.ToInt32(la_purchaser[5][cbPurchase.SelectedIndex - 1]);
                    maxPID = Convert.ToInt32(la_purchaser[5][cbPurchase.SelectedIndex - 1]);
                }

                if (cbAuth.SelectedIndex != 0)
                {
                    minAID = Convert.ToInt32(la_auth[5][cbAuth.SelectedIndex - 1]);
                    maxAID = Convert.ToInt32(la_auth[5][cbAuth.SelectedIndex - 1]);
                }

                if (cbFaculty.SelectedIndex != 0)
                {
                    minFID = Convert.ToInt32(la_faculty[5][cbFaculty.SelectedIndex - 1]);
                    maxFID = Convert.ToInt32(la_faculty[5][cbFaculty.SelectedIndex - 1]);
                }

                if (cbBudget.SelectedIndex != 0)
                {
                    minBID = Convert.ToInt32(la_budget[0][cbBudget.SelectedIndex - 1]);
                    maxBID = Convert.ToInt32(la_budget[0][cbBudget.SelectedIndex - 1]);
                }
            }

            if (checkBox1.Checked)
            {
                dateFrom = dateTimePicker1.Value;
                dateTo = dateTimePicker2.Value;
            }

            if (cbPendDeliv.Checked)
                qtyDiff = 1;

            la_order = db.selOrderReport(minReqID, maxReqID, minPID, maxPID, minAID, maxAID, minFID, maxFID, minDID, maxDID,
             minBID, maxBID, minVID, maxVID, minOrdID, maxOrdID, dateFrom, dateTo, qtyDiff);

            rc = la_order[0].Count;

            if (rc == 0)
            {
                MessageBox.Show("No results found");
                dataGridView1.RowCount = 0;
                dgLineItem.RowCount = 0;
                tbOID.Text = "";

                btnExportCSV.Visible = false;
                btnExportPDF.Visible = false;
                return;
            }

            dataGridView1.RowCount = rc;


            int i = 0;

            while (i < rc) // Fill in data grid with results
            {
                dataGridView1.Rows[i].Cells[0].Value = la_order[0][i];
                dataGridView1.Rows[i].Cells[1].Value = la_order[1][i];
                dataGridView1.Rows[i].Cells[2].Value = la_order[2][i];
                dataGridView1.Rows[i].Cells[3].Value = la_order[3][i];
                dataGridView1.Rows[i].Cells[4].Value = la_order[4][i];
                dataGridView1.Rows[i].Cells[5].Value = la_order[5][i];
                dataGridView1.Rows[i].Cells[6].Value = la_order[6][i];
                dataGridView1.Rows[i].Cells[7].Value = la_order[7][i];
                dataGridView1.Rows[i].Cells[8].Value = la_order[8][i];
                dataGridView1.Rows[i].Cells[9].Value = la_order[9][i];
                dataGridView1.Rows[i].Cells[10].Value = la_order[10][i];
                dataGridView1.Rows[i].Cells[11].Value = la_order[11][i];
                dataGridView1.Rows[i].Cells[12].Value = la_order[12][i];
                dataGridView1.Rows[i].Cells[13].Value = la_order[13][i];
                dataGridView1.Rows[i].Cells[14].Value = la_order[14][i];
                dataGridView1.Rows[i].Cells[15].Value = la_order[15][i];
                i++;
            }

            dgLineItem.RowCount = 0;
 
            // display line item for first row
            if (rc > 0)
            {
                int ordID = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value.ToString());
                tbOID.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                handleOrderEntry(ordID);

                btnExportCSV.Visible = true;
                btnExportPDF.Visible = true;
            }
        }
        private void handleOrderEntry(int ono)
        {
            
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


        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex >= 0))
            {
                dataGridView1.Cursor = Cursors.Hand;
                dataGridView1.Rows[e.RowIndex].Cells[0].ToolTipText = "Click to display line items for this order";
            }

            else
                dataGridView1.Cursor = Cursors.Default;
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                dataGridView1.Cursor = Cursors.Default;
            }
        }

        // New Code

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ci = e.ColumnIndex;
            int ri = e.RowIndex;
            int ordID = 0;
            if ((ci != 0) || (ri < 0))
                return;

            ordID = Convert.ToInt32(dataGridView1.Rows[ri].Cells[0].Value.ToString());
            tbOID.Text = dataGridView1.Rows[ri].Cells[0].Value.ToString();
            handleOrderEntry(ordID);
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            if (Global.loginState != Global.usrAdmin)
                return;

            ToolTip tt = new ToolTip();

            tbOID.Cursor = Cursors.Hand;
            tt.SetToolTip(tbOID, "Click to edit this order");
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            tbOID.Cursor = Cursors.Default;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Global.loginState != Global.usrAdmin)
                return;

            frmUI fUI;
            int orderID;

            try
            {
                orderID = Convert.ToInt32(tbOID.Text);
            }

            catch
            {
                MessageBox.Show("Order ID Null");
                return;
            }

            fUI = (frmUI)this.MdiParent;
            fUI.orderListToEdit(Convert.ToInt32(orderID));

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 0;
            tbOID.Text = "";
            dgLineItem.RowCount = 0;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

            string rmsg1, rmsg2;
            string filter;

            filter = "Filter: ";
            filter += "\t" + "Order ID: " + tbOrderID.Text + "\r\n";
            filter += "\t" + "Requester: " + cbRequest.Text + "\r\n";
            filter += "\t" + "Department: " + cbDept.Text + "\r\n";
            filter += "\t" + "Vendor: " + cbVendor.Text + "\r\n";
            filter += "\t" + "Purchaser: " + cbPurchase.Text + "\r\n";
            filter += "\t" + "Authorities: " + cbAuth.Text + "\r\n";
            filter += "\t" + "Faculty: " + cbFaculty.Text + "\r\n";
            filter += "\t" + "Budget: " + cbBudget.Text + "\r\n";
            filter += "\r\n";

            filter = "Filter: \t";
            if (tbOrderID.Text != "")
                filter += "Order ID = \"" + tbOID.Text + "\" & ";
            if (checkBox2.Checked)
            {
                if (cbRequest.SelectedIndex > 0)
                    filter += "Requestor =  \"" + cbRequest.Text + "\" & ";
                if (cbDept.SelectedIndex > 0)
                    filter += "Department =  \"" + cbDept.Text + "\" & ";
                if (cbVendor.SelectedIndex > 0)
                    filter += "Vendor = \"" + cbVendor.Text + "\" & ";
                if (cbPurchase.SelectedIndex > 0)
                    filter += "Purchaser =  \"" + cbPurchase.Text + "\" & ";
                if (cbAuth.SelectedIndex > 0)
                    filter += "Authorities =  \"" + cbAuth.Text + "\" & ";
                if (cbFaculty.SelectedIndex > 0)
                    filter += "Faculty = \"" + cbFaculty.Text + "\" & ";
                if (cbBudget.SelectedIndex > 0)
                    filter += "Budget = \"" + cbBudget.Text + "\" & ";
            }
            if (cbPendDeliv.Checked)
                filter += "PendingDelivery = \"" + "ON" + "\" & ";
            if (checkBox1.Checked)
            {
                filter += "Order Date >= \"" + dateTimePicker1.Text + "\" & ";
                filter += "Order Date <= \"" + dateTimePicker2.Text + "\" & ";
            }

            if (filter == "Filter: \t")
                filter += "None";
            else
                filter = filter.Remove(filter.Length - 2, 2);
            filter += "\r\n\n";

            Button btn = sender as Button;

            if (btn.Name == "btnExportCSV")
                rmsg1 = Global.exportToCsvFile(dataGridView1, "OrderList", filter);
            else
                rmsg1 = Global.exportToPdfFile(dataGridView1, "OrderList", filter);

            if (rmsg1 != null)
            {
                filter = "Filter: \t";
                if (tbOID.Text != "")
                    filter += "Order ID = \"" + tbOID.Text + "\"";
                else
                    filter += "None";
                filter += "\r\n\r\n";

                if (btn.Name == "btnExportCSV")
                    rmsg2 = Global.exportToCsvFile(dgLineItem, "LineItemList_Order_" + tbOID.Text, filter);
                else
                    rmsg2 = Global.exportToPdfFile(dgLineItem, "LineItemList_Order_" + tbOID.Text, filter);

                if (rmsg2 != null)
                    MessageBox.Show( rmsg1 + " & " + rmsg2 + " files created sucessfully");
                else
                    MessageBox.Show(" Unable to export line item grid to a file");
            }
            else
                MessageBox.Show(" Unable to export to a file");

        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox clb = sender as CheckedListBox;

            int i = clb.SelectedIndex;

            if (i < 0)
                return;

            if (clb.GetItemCheckState(i) == CheckState.Checked)
            {
                dataGridView1.Columns[i + 1].Visible = false;
                dataGridView1.Width = dataGridView1.Width - dataGridView1.Columns[i + 1].Width;
            }

            else
            {
                dataGridView1.Columns[i + 1].Visible = true;
                dataGridView1.Width = dataGridView1.Width + dataGridView1.Columns[i + 1].Width;
            }
        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox clb = sender as CheckedListBox;

            int i = clb.SelectedIndex;

            if (i < 0)
                return;

            if (clb.GetItemCheckState(i) == CheckState.Checked)
            {
                dgLineItem.Columns[i + 2].Visible = false;
                dgLineItem.Width = dgLineItem.Width - dgLineItem.Columns[i + 2].Width;
            }

            else
            {
                dgLineItem.Columns[i + 2].Visible = true;
                dgLineItem.Width = dgLineItem.Width + dgLineItem.Columns[i + 2].Width;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label12.Visible = false;
            }
            else
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label12.Visible = true;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
            {
                groupBox1.Visible = false;
            }
            else
            {
                groupBox1.Visible = true;
            }

        }

        // End New Code
    }
}

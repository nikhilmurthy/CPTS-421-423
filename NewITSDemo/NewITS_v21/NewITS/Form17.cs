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

            comboBox2.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;

            la_vendor = db.selAllVendor();
            comboBox3.DataSource = la_vendor[0];
            la_dept = db.selAllDept();
            la_owner = db.selAllOwner();
            la_purchaser = db.selAllPurchaser();
            la_faculty = db.selAllFaculty();
            la_auth = db.selAllAuth();
            comboBox1.DataSource = la_dept[0];
            comboBox4.DataSource = la_owner[4];
            comboBox7.DataSource = la_purchaser[4];
            comboBox8.DataSource = la_faculty[4];
            comboBox9.DataSource = la_auth[4];
            la_budget = db.selAllBudget();
            comboBox6.DataSource = la_budget[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rc = -1;
            int ownerID, purchaserID, vendorID, deptID, facultyID, authID;
            int budgetNo, reqNo;
            string errMsg = "", comment;
            int shipMode = comboBox5.SelectedIndex;
            int paymentType = comboBox2.SelectedIndex;

            try
            {
                reqNo = Convert.ToInt32(textBox11.Text);
            }

            catch
            {
                MessageBox.Show("Invalid requisition number");
                return;
            }

            ownerID = Convert.ToInt32(la_owner[5][comboBox7.SelectedIndex]);
            purchaserID = Convert.ToInt32(la_purchaser[5][comboBox4.SelectedIndex]);
            vendorID = Convert.ToInt32(la_vendor[5][comboBox3.SelectedIndex]);
            deptID = Convert.ToInt32(la_dept[2][comboBox1.SelectedIndex]);
            authID = Convert.ToInt32(la_auth[5][comboBox9.SelectedIndex]);
            facultyID = Convert.ToInt32(la_faculty[5][comboBox8.SelectedIndex]);
            budgetNo = Convert.ToInt32(la_budget[0][comboBox6.SelectedIndex]);

            comment = textBox8.Text;

            rc = db.editPurchaseRequest(reqNo, checkBox1.Checked, ownerID, purchaserID, facultyID,
                   authID, vendorID, deptID, budgetNo, comment, shipMode, paymentType, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkBox1.Checked)
                MessageBox.Show("Order " + reqNo + " Deleted");
            else
                MessageBox.Show("Order " + reqNo + " Changed");

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.orderUpdateToList();

            // End New Code

            this.Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = la_owner[3][comboBox4.SelectedIndex];
            textBox3.Text = la_owner[0][comboBox4.SelectedIndex];
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            int reqNum;
            string deliveryDate = "";

            try
            {
                reqNum = Convert.ToInt32(textBox11.Text);
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

            textBox1.Text = la_orders[0][0];
            textBox4.Text = la_orders[9][0];
            textBox8.Text = la_orders[10][0];

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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index; // Selected item index
            string vname;

            vname = comboBox3.SelectedValue.ToString();

            index = comboBox3.SelectedIndex;

            textBox38.Text = la_vendor[1][index];
            textBox5.Text = la_vendor[2][index];
            textBox6.Text = la_vendor[3][index];
            textBox7.Text = la_vendor[4][index];
        }
    }
}

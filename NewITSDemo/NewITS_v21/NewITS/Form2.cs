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
        DataStore ds = DataStore.getInstance;
        DepartmentStore dpts = DepartmentStore.getInstance;
        VendorStore vs = VendorStore.getInstance;
        UserStore us = UserStore.getInstance;
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

            comboBox2.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;

            textBox1.Text = DateTime.Now.ToString("M/d/yyyy");

            // initialize the datagrid for lineitem

            dgLineItem.RowCount = 10;
            for (int i = 0; i < 10; i++)
            {
                dgLineItem.Rows[i].Cells[0].Value = (i + 1).ToString();
            }

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

        public void frmNewPurchaseReloadDropdown()
        {
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

        private void button1_Click(object sender, EventArgs e)
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
                orderID = Convert.ToInt32(textBox11.Text);
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

            vendorID = Convert.ToInt32(la_vendor[5][comboBox3.SelectedIndex]);
            deptID = Convert.ToInt32(la_dept[2][comboBox1.SelectedIndex]);
            facultyID = Convert.ToInt32(la_faculty[5][comboBox8.SelectedIndex]);
            authID = Convert.ToInt32(la_auth[5][comboBox9.SelectedIndex]);
            purchaserID = Convert.ToInt32(la_purchaser[5][comboBox7.SelectedIndex]);
            ownerID = Convert.ToInt32(la_owner[5][comboBox4.SelectedIndex]);
            shipMode = comboBox5.SelectedIndex;
            budgetNo = Convert.ToInt32(la_budget[0][comboBox6.SelectedIndex]);
            paymentType = comboBox2.SelectedIndex;

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

            textBox4.Text = (string.Format("{0:0.00}", currentTotal));

            //            textBox4.Text = (string.Format("{0:0.00}", currentUnitPrice));

            total = Convert.ToDouble(textBox4.Text);

            rc = db.newPurchaseRequest(orderID, textBox8.Text, DateTime.Now.ToString("yyyy-MM-dd"), vendorID, shipMode, budgetNo,
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

                fUI.orderUpdateToList();

                // End New Code

                this.Close();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmNewPurchaseRequest_Load(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = la_owner[3][comboBox4.SelectedIndex];
            textBox3.Text = la_owner[0][comboBox4.SelectedIndex];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgLineItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
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
            comboBox1.DataSource = la_dept[0];
            comboBox3.DataSource = la_building[0];
            comboBox4.DataSource = la_category[0];
            comboBox5.DataSource = la_vendor[0];
            comboBox6.DataSource = la_owner[4];
        }

        public frmEditInventory(int barcode)
        {
            la_dept = db.selAllDept();
            la_building = db.selAllBuilding();
            la_category = db.selAllCategory();
            la_vendor = db.selAllVendor();
            la_owner = db.selAllOwner();

            InitializeComponent();
            comboBox1.DataSource = la_dept[0];
            comboBox3.DataSource = la_building[0];
            comboBox4.DataSource = la_category[0];
            comboBox5.DataSource = la_vendor[0];
            comboBox6.DataSource = la_owner[4];
            textBox2.Text = Convert.ToString(barcode);
            textBox2.Enabled = false;
            loadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "", purchaseDate = "";
            int bc;
            int catID, buildID, ownerID, vendorID, deptID;
            double purchasePrice;

            catID = Convert.ToInt32(la_category[1][comboBox4.SelectedIndex]);
            buildID = Convert.ToInt32(la_building[2][comboBox3.SelectedIndex]);
            ownerID = Convert.ToInt32(la_owner[5][comboBox6.SelectedIndex]);
            vendorID = Convert.ToInt32(la_vendor[5][comboBox5.SelectedIndex]);
            deptID = Convert.ToInt32(la_dept[2][comboBox1.SelectedIndex]);

            try
            {
                bc = Convert.ToInt32(textBox2.Text);
            }

            catch
            {
                MessageBox.Show("Invalid barcode number");
                return;
            }

            try
            {
                purchasePrice = Convert.ToDouble(textBox4.Text);
            }

            catch
            {
                MessageBox.Show("Invalid purchase price");
                return;
            }

            // Set purchase date in correct form

            purchaseDate = textBox3.Text;
            purchaseDate = DateTime.Parse(purchaseDate).ToString("yyyy-MM-dd");
            textBox3.Text = purchaseDate;

            rc = db.editInventory(bc, checkBox1.Checked, textBox5.Text, catID, buildID, textBox14.Text, ownerID, vendorID, deptID, textBox3.Text,
                purchasePrice, textBox11.Text, textBox15.Text, textBox12.Text, textBox13.Text, textBox10.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkBox1.Checked)
                MessageBox.Show("Inventory " + bc + " Deleted");
            else
                MessageBox.Show("Inventory " + bc + " Changed");

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.invUpdateToList();

            // End New Code

            this.Close();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            int barcode;
            string deliveryDate = "";

            try
            {
                barcode = Convert.ToInt32(textBox2.Text);
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

            textBox7.Text = la_inv[0][0];
            textBox1.Text = la_inv[1][0]; // Delivery date
            textBox3.Text = la_inv[2][0]; // Purchase date

            // Set delivery date in correct form

            deliveryDate = textBox1.Text;
            deliveryDate = DateTime.Parse(deliveryDate).ToString("yyyy-MM-dd");
            textBox1.Text = deliveryDate;

            textBox4.Text = la_inv[3][0];
            textBox5.Text = la_inv[4][0];
            textBox11.Text = la_inv[5][0];
            textBox12.Text = la_inv[7][0];
            textBox13.Text = la_inv[8][0];
            textBox14.Text = la_inv[9][0];
            textBox15.Text = la_inv[6][0];
            textBox10.Text = la_inv[10][0];

            handleOrderEntry();
        }

        private void loadData()
        {
            int barcode;
            string deliveryDate = "";

            try
            {
                barcode = Convert.ToInt32(textBox2.Text);
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

            textBox7.Text = la_inv[0][0];
            textBox1.Text = la_inv[1][0]; // Delivery date
            textBox3.Text = la_inv[2][0]; // Purchase date

            // Set delivery date in correct form

            deliveryDate = textBox1.Text;
            deliveryDate = DateTime.Parse(deliveryDate).ToString("yyyy-MM-dd");
            textBox1.Text = deliveryDate;

            textBox4.Text = la_inv[3][0];
            textBox5.Text = la_inv[4][0];
            textBox11.Text = la_inv[5][0];
            textBox12.Text = la_inv[7][0];
            textBox13.Text = la_inv[8][0];
            textBox14.Text = la_inv[9][0];
            textBox15.Text = la_inv[6][0];
            textBox10.Text = la_inv[10][0];

            handleOrderEntry();
        }

        private void handleOrderEntry()
        {
            int ono;

            try
            {
                ono = Convert.ToInt32(textBox7.Text);
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

            textBox3.Text = la_orderNum[0][0];

            if (la_orderNum[1][0] != "")
                comboBox5.SelectedIndex = la_vendor[5].IndexOf(la_orderNum[1][0]);

            if (la_orderNum[2][0] != "")
                comboBox6.SelectedIndex = la_owner[5].IndexOf(la_orderNum[2][0]);

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
            textBox9.Text = la_owner[0][comboBox6.SelectedIndex];
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

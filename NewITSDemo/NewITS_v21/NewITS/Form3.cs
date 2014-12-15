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

        DataStore ds = DataStore.getInstance;
        BuildingStore bs = BuildingStore.getInstance;
        DepartmentStore dpts = DepartmentStore.getInstance;

        List<string>[] la_category;
        List<string>[] la_building;
        List<string>[] la_dept;
        List<string>[] la_vendor;
        List<string>[] la_owner;

        List<string> la_order;
        List<string>[] la_lineitem;
        List<string>[] la_orderNum;
        int lineNoSave = 0;


        public frmNewInventory()
        {
            la_dept = db.selAllDept();
            la_building = db.selAllBuilding();
            la_category = db.selAllCategory();
            la_vendor = db.selAllVendor();
            la_owner = db.selAllOwner();
            la_order = db.selOrdersZero();


            InitializeComponent();
            textBox1.Text = DateTime.Now.ToString("yyyy-M-d");
            comboBox1.DataSource = la_dept[0];
            comboBox3.DataSource = la_building[0];
            comboBox4.DataSource = la_category[0];
            comboBox5.DataSource = la_vendor[0];
            comboBox6.DataSource = la_owner[4];
        }

        public frmNewInventory(int ordNum)
        {
            la_dept = db.selAllDept();
            la_building = db.selAllBuilding();
            la_category = db.selAllCategory();
            la_vendor = db.selAllVendor();
            la_owner = db.selAllOwner();
            la_order = db.selOrdersZero();


            InitializeComponent();
            textBox1.Text = DateTime.Now.ToString("yyyy-M-d");
            comboBox1.DataSource = la_dept[0];
            comboBox3.DataSource = la_building[0];
            comboBox4.DataSource = la_category[0];
            comboBox5.DataSource = la_vendor[0];
            comboBox6.DataSource = la_owner[4];

            textBox7.Text = Convert.ToString(ordNum);
            textBox7.Enabled = false;
            loadData();
        }

        public void frmNewInventoryDropdown()
        {
            la_order = db.selOrdersZero();

            la_dept = db.selAllDept();
            comboBox1.DataSource = la_dept[0];

            la_building = db.selAllBuilding();
            comboBox3.DataSource = la_building[0];

            la_category = db.selAllCategory();
            comboBox4.DataSource = la_category[0];

            la_vendor = db.selAllVendor();
            comboBox5.DataSource = la_vendor[0];

            la_owner = db.selAllOwner();
            comboBox6.DataSource = la_owner[4];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //       this.Hide();
            this.Close();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;
            int ono;

            ono = Convert.ToInt32(comboBox2.SelectedValue);

            index = comboBox2.SelectedIndex;

            la_lineitem = db.selLineItemOne(ono);
            la_orderNum = db.selOrdersOne(ono);

            if (la_orderNum[1][0] != "")
                comboBox5.SelectedIndex = la_vendor[5].IndexOf(la_orderNum[1][0]);
            if (la_orderNum[2][0] != "")
                comboBox6.SelectedIndex = la_owner[5].IndexOf(la_orderNum[2][0]);

            textBox6.Text = la_orderNum[1][0];

            dgLineItem.RowCount = 10;
            int i = 0;
            for (int j = 0; j < 7; j++)
            {
                i = 0;
                foreach (string item in la_lineitem[j])
                {
                    dgLineItem.Rows[i].Cells[j + 1].Value = item;
                    i++;
                }
            };

            dgLineItem.RowCount = i;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //         MessageBox.Show(comboBox1.SelectedValue.ToString());
        }

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
 //                       cb.Value = true; // Doesn't work
                        lineNoSave = 0;
                        textBox5.Text = null;
                        textBox4.Text = null;
                        return;
                    }

                    cb.Value = false;
                }

                lineNoSave = Convert.ToInt32(dgLineItem.Rows[ri].Cells[1].Value.ToString());


                textBox5.Text = dgLineItem.Rows[ri].Cells[3].Value.ToString(); // description
                textBox4.Text = dgLineItem.Rows[ri].Cells[6].Value.ToString(); // price
            }

        }

        private void button2_Click(object sender, EventArgs e)
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
                invID = Convert.ToInt32(textBox2.Text);
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

            try
            {
                purchasePrice = Convert.ToDouble(textBox4.Text);
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

            catgID = Convert.ToInt32(la_category[1][comboBox4.SelectedIndex]);
            deptID = Convert.ToInt32(la_dept[2][comboBox1.SelectedIndex]);
            vendorID = Convert.ToInt32(la_vendor[5][comboBox5.SelectedIndex]);
            bldgID = Convert.ToInt32(la_building[2][comboBox3.SelectedIndex]);
            ownerID = Convert.ToInt32(la_owner[5][comboBox6.SelectedIndex]);

            try
            {
                orderID = Convert.ToInt32(textBox7.Text);
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

            lineNo = lineNoSave;

            deliveryDate = textBox1.Text;

            purchaseDate = Convert.ToString(textBox3.Text);

            purchaseDate = DateTime.Parse(purchaseDate).ToString("yyyy-MM-dd");

            invID = db.newInventory(invID, textBox5.Text, catgID, bldgID, textBox14.Text, ownerID, orderID, lineNo, vendorID,
                           deptID, deliveryDate, purchaseDate, purchasePrice, textBox11.Text,
                           textBox15.Text, textBox12.Text, textBox13.Text, textBox10.Text, ref errmsg);

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

                fUI.invUpdateToList();

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

        // End New Code

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

        private void textBox7_Leave_1(object sender, EventArgs e)
        {
            handleOrderEntry();
        }

        private void comboBox6_DropDown(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox9.Text = la_owner[0][comboBox6.SelectedIndex];
        }
    }
}
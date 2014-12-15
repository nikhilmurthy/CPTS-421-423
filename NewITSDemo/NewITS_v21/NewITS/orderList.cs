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
            comboBox1.DataSource = la_requester[4];
            comboBox4.DataSource = la_purchaser[4];
            comboBox5.DataSource = la_auth[4];
            comboBox6.DataSource = la_faculty[4];
            comboBox2.DataSource = la_dept[0];
            comboBox3.DataSource = la_vendor[0];
            comboBox7.DataSource = la_budget[2];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
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
            int maxReqID = 99000; // Requester ID Max
            int maxAID = 99000; // Authority ID Max
            int maxPID = 99000; // Purchaser ID Max
            int maxBID = 99000; // Budget ID Max
            int maxVID = 99000; // Vendor ID Max
            int maxDID = 99000; // Department ID Max
            int maxFID = 99000; // Faculty ID Max
            int maxOrdID = 99000; // Order ID Max

            if (textBox2.Text != "")
            {
                try
                {
                    orderID = Convert.ToInt32(textBox2.Text);
                }

                catch
                {
                    MessageBox.Show("Invalid inventory number");
                    return;
                }

                minOrdID = orderID;
                maxOrdID = orderID;
            }

            if (comboBox1.SelectedIndex != 0)
            {
                minReqID = Convert.ToInt32(la_requester[5][comboBox1.SelectedIndex - 1]);
                maxReqID = Convert.ToInt32(la_requester[5][comboBox1.SelectedIndex - 1]);
            }

            if (comboBox2.SelectedIndex != 0)
            {
                minDID = Convert.ToInt32(la_dept[2][comboBox2.SelectedIndex - 1]);
                maxDID = Convert.ToInt32(la_dept[2][comboBox2.SelectedIndex - 1]);
            }

            if (comboBox3.SelectedIndex != 0)
            {
                minVID = Convert.ToInt32(la_vendor[5][comboBox3.SelectedIndex - 1]);
                maxVID = Convert.ToInt32(la_vendor[5][comboBox3.SelectedIndex - 1]);
            }

            if (comboBox4.SelectedIndex != 0)
            {
                minPID = Convert.ToInt32(la_purchaser[5][comboBox4.SelectedIndex - 1]);
                maxPID = Convert.ToInt32(la_purchaser[5][comboBox4.SelectedIndex - 1]);
            }

            if (comboBox5.SelectedIndex != 0)
            {
                minAID = Convert.ToInt32(la_auth[5][comboBox5.SelectedIndex - 1]);
                maxAID = Convert.ToInt32(la_auth[5][comboBox5.SelectedIndex - 1]);
            }

            if (comboBox6.SelectedIndex != 0)
            {
                minFID = Convert.ToInt32(la_faculty[5][comboBox6.SelectedIndex - 1]);
                maxFID = Convert.ToInt32(la_faculty[5][comboBox6.SelectedIndex - 1]);
            }

            if (comboBox7.SelectedIndex != 0)
            {
                minBID = Convert.ToInt32(la_budget[0][comboBox7.SelectedIndex - 1]);
                maxBID = Convert.ToInt32(la_budget[0][comboBox7.SelectedIndex - 1]);
            }

            la_order = db.selOrderReport(minReqID, maxReqID, minPID, maxPID, minAID, maxAID, minFID, maxFID, minDID, maxDID,
             minBID, maxBID, minVID, maxVID, minOrdID, maxOrdID);

            rc = la_order[0].Count;

            if (rc == 0)
            {
                MessageBox.Show("No results found");
                dataGridView1.RowCount = 0;
                dgLineItem.RowCount = 0;
                textBox1.Text = "";
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

            for (int j = 0; j < 16; j++) // Clear the la_order array of data
                la_order[j].Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ci = e.ColumnIndex;
            int ri = e.RowIndex;
            int ordID = 0;
            if (ci != 0)
                return;

            ordID = Convert.ToInt32(dataGridView1.Rows[ri].Cells[0].Value.ToString());

   //       MessageBox.Show("cliekd " + ordID.ToString());
            textBox1.Text = dataGridView1.Rows[ri].Cells[0].Value.ToString();
            handleOrderEntry(ordID);

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
            if (e.ColumnIndex == 0)
            {
                dataGridView1.Cursor = Cursors.Hand;
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
            if (ci != 0)
                return;

            ordID = Convert.ToInt32(dataGridView1.Rows[ri].Cells[0].Value.ToString());

            //       MessageBox.Show("cliekd " + ordID.ToString());
            textBox1.Text = dataGridView1.Rows[ri].Cells[0].Value.ToString();
            handleOrderEntry(ordID);
        }

        private void dgLineItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUI fUI;

            fUI = (frmUI)this.MdiParent;
            fUI.orderListToInvNew(Convert.ToInt32(textBox1.Text));

            this.Close();
        }

        // End New Code
    }
}

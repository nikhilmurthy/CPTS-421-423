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
    public partial class frmListInventory : Form
    {
        DBA db = DBA.getInstance;
        List<string>[] la_owner;
        List<string>[] la_category;
        List<string>[] la_building;
        List<string>[] la_inventory;
        public frmListInventory()
        {
            InitializeComponent();

            la_owner = db.selAllOwner();
            la_building = db.selAllBuilding();
            la_category = db.selAllCategory();
            la_owner[4].Insert(0, "All Owners");
            la_building[0].Insert(0, "All Buildings");
            la_category[0].Insert(0, "All Categories");
            cbOwner.DataSource = la_owner[4];
            cbBuild.DataSource = la_building[0];
            cbCategory.DataSource = la_category[0];

            btnExportCSV.Visible = false;
            btnExportPDF.Visible = false;

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }

            dateTimePicker1.Value = DateTime.Today.AddMonths(-3);
            dateTimePicker2.Value = DateTime.Today;
            checkBox1.Checked = true;
            btnSearch_Click(null, null);
        }

        public frmListInventory(int invID) : this()
        {
            tbInvenID.Text = Convert.ToString(invID);
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            btnSearch_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int minOwnID = 0;
            int minBID = 0;
            int minIID = 0;
            int minCID = 0;
            int minOrdID = 0;
            int maxOwnID = int.MaxValue;
            int maxBID = int.MaxValue;
            int maxIID = int.MaxValue;
            int maxCID = int.MaxValue;
            int maxOrdID = int.MaxValue;
            int rc;
            int orderID;
            int invID;
 //         DateTime dateFrom = DateTime.MinValue;
 //         DateTime dateTo = DateTime.MaxValue;

            DateTime dateFrom = DateTime.Today.AddYears(-100);
            DateTime dateTo = DateTime.Today;

            if (tbInvenID.Text != "")
            {
                try
                {
                    invID = Convert.ToInt32(tbInvenID.Text);
                }

                catch
                {
                    MessageBox.Show("Invalid inventory number");
                    return;
                }

                minIID = invID;
                maxIID = invID;
            }


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
                if (cbOwner.SelectedIndex != 0)
                {
                    minOwnID = Convert.ToInt32(la_owner[5][cbOwner.SelectedIndex - 1]);
                    maxOwnID = Convert.ToInt32(la_owner[5][cbOwner.SelectedIndex - 1]);
                }

                if (cbBuild.SelectedIndex != 0)
                {
                    minBID = Convert.ToInt32(la_building[2][cbBuild.SelectedIndex - 1]);
                    maxBID = Convert.ToInt32(la_building[2][cbBuild.SelectedIndex - 1]);
                }

                if (cbCategory.SelectedIndex != 0)
                {
                    minCID = Convert.ToInt32(la_category[1][cbCategory.SelectedIndex - 1]);
                    maxCID = Convert.ToInt32(la_category[1][cbCategory.SelectedIndex - 1]);
                }
            }
            if (checkBox1.Checked)
            {
                dateFrom = dateTimePicker1.Value;
                dateTo = dateTimePicker2.Value;
            }

            la_inventory = db.selInvReport(minOwnID, maxOwnID, minBID, maxBID, minCID, maxCID, minOrdID, maxOrdID, minIID, maxIID, dateFrom, dateTo);

            rc = la_inventory[0].Count;

            dataGridView1.RowCount = rc;

            if (rc == 0)
            {

                MessageBox.Show("No results found");
                btnExportCSV.Visible = false;
                btnExportPDF.Visible = false;
                return;
            };

            btnExportCSV.Visible = true;
            btnExportPDF.Visible = true;

            int i = 0;

            while (i < rc) // Fill in data grid with results
            {
                dataGridView1.Rows[i].Cells[0].Value = la_inventory[0][i];
                dataGridView1.Rows[i].Cells[1].Value = la_inventory[1][i];
                dataGridView1.Rows[i].Cells[2].Value = la_inventory[2][i];
                dataGridView1.Rows[i].Cells[3].Value = la_inventory[3][i];
                dataGridView1.Rows[i].Cells[4].Value = la_inventory[4][i];
                dataGridView1.Rows[i].Cells[5].Value = la_inventory[5][i];
                dataGridView1.Rows[i].Cells[6].Value = la_inventory[6][i];
                dataGridView1.Rows[i].Cells[7].Value = la_inventory[7][i];
                dataGridView1.Rows[i].Cells[8].Value = la_inventory[8][i];

                i++;
            }

            for (int j = 0; j < 6; j++) // Clear the la_inventory array of data
                la_inventory[j].Clear();
        }

        // New Code

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUI fUI;
            int ci = e.ColumnIndex;
            int ri = e.RowIndex;
            int barcode = 0;

            if ((ci != 0) || (ri < 0) || (Global.loginState != Global.usrAdmin))
                return;

            barcode = Convert.ToInt32(dataGridView1.Rows[ri].Cells[0].Value.ToString());

            fUI = (frmUI)this.MdiParent;

            fUI.invListToEdit(barcode);

            this.Close();
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0) && (e.RowIndex >= 0) && (Global.loginState == Global.usrAdmin))
            {
                dataGridView1.Cursor = Cursors.Hand;
                dataGridView1.Rows[e.RowIndex].Cells[0].ToolTipText = "Click to edit this row";
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

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 0;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string filter;
            string rmsg;

            filter = "Filter: \t";
            if (tbInvenID.Text != "")
                filter += "Inventory ID = \"" + tbInvenID.Text + "\" & ";
            if (tbOrderID.Text != "")
                filter += "Order ID =  \"" + tbOrderID.Text + "\" & ";
            if (checkBox2.Checked)
            {
                if (cbOwner.SelectedIndex > 0)
                    filter += "Owner =  \"" + cbOwner.Text + "\" & ";
                if (cbBuild.SelectedIndex > 0)
                    filter += "Building =  \"" + cbBuild.Text + "\" & ";
                if (cbCategory.SelectedIndex > 0)
                    filter += "Category = \"" + cbCategory.Text + "\" & ";
            }
            if (checkBox1.Checked)
            {
                filter += "Delivery Date >= \"" + dateTimePicker1.Text + "\" & ";
                filter += "Delivery Date <= \"" + dateTimePicker2.Text + "\" & ";
            }
            if (filter == "Filter: \t")
                filter += "None";
            else
                filter = filter.Remove(filter.Length - 2, 2);
            filter += "\r\n\n";

            Button btn = sender as Button;

            if (btn.Name == "btnExportCSV")
                rmsg = Global.exportToCsvFile(dataGridView1, "InventoryList", filter);

            else
                rmsg = Global.exportToPdfFile(dataGridView1, "InventoryList", filter);

            if (rmsg != null)
                MessageBox.Show(rmsg + " file created sucessfully");
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label8.Visible = false;
            }
            else
            {
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
                label8.Visible = true;

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

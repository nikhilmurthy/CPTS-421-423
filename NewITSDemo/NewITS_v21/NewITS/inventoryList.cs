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
            int rc;
            InitializeComponent();
  //          label1.Cursor = Cursors.Hand;
            la_owner = db.selAllOwner();
            la_building = db.selAllBuilding();
            la_category = db.selAllCategory();
            la_owner[4].Insert(0, "All Owners");
            la_building[0].Insert(0, "All Buildings");
            la_category[0].Insert(0, "All Categories");
            comboBox1.DataSource = la_owner[4];
            comboBox2.DataSource = la_building[0];
            comboBox3.DataSource = la_category[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int minOwnID = 0;
            int minBID = 0;
            int minIID = 0;
            int minCID = 0;
            int minOrdID = 0;
            int maxOwnID = 99000;
            int maxBID = 99000;
            int maxIID = 99000;
            int maxCID = 99000;
            int maxOrdID = 99000;
            int rc;
            int orderID;
            int invID;

            if (textBox1.Text != "")
            {
                try
                {
                    invID = Convert.ToInt32(textBox1.Text);
                }

                catch
                {
                    MessageBox.Show("Invalid inventory number");
                    return;
                }

                minIID = invID;
                maxIID = invID;
            }


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
                minOwnID = Convert.ToInt32(la_owner[5][comboBox1.SelectedIndex - 1]);
                maxOwnID = Convert.ToInt32(la_owner[5][comboBox1.SelectedIndex - 1]);
            }

            if (comboBox2.SelectedIndex != 0)
            {
                minBID = Convert.ToInt32(la_building[2][comboBox2.SelectedIndex - 1]);
                maxBID = Convert.ToInt32(la_building[2][comboBox2.SelectedIndex - 1]);
            }

            if (comboBox3.SelectedIndex != 0)
            {
                minCID = Convert.ToInt32(la_category[1][comboBox3.SelectedIndex - 1]);
                maxCID = Convert.ToInt32(la_category[1][comboBox3.SelectedIndex - 1]);
            }

            la_inventory = db.selInvReport(minOwnID, maxOwnID, minBID, maxBID, minCID, maxCID, minOrdID, maxOrdID, minIID, maxIID);

            rc = la_inventory[0].Count;

            dataGridView1.RowCount = rc;

            if (rc == 0)
            {

                MessageBox.Show("No results found");
                return;
            };

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

//                dataGridView1.Cursor = Cursors.Hand;
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

            if (ci != 0)
                return;

            barcode = Convert.ToInt32(dataGridView1.Rows[ri].Cells[0].Value.ToString());

            fUI = (frmUI)this.MdiParent;

            fUI.invListToEdit(barcode);

            this.Close();
        } 

        // End New Code
    }
}

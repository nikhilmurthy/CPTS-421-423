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
    public partial class frmEditBuilding : Form
    {
        public delegate void BuildingChangedHandler(object source, PropertyChangedEventArgs e);
        public event BuildingChangedHandler DataChanged;

        DBA db = DBA.getInstance;
        List<string>[] la_building;
        public frmEditBuilding()
        {
            InitializeComponent();
            la_building = db.selAllBuilding();
            comboBox1.DataSource = la_building[0];
        }

        public frmEditBuilding(int bID)
        {
            int j = 0; // Index
            InitializeComponent();
            la_building = db.selAllBuilding();
            comboBox1.DataSource = la_building[0];
            for (int i = 0; i < la_building[0].Count; i++)
            {
                if (bID == Convert.ToInt32(la_building[2][i]))
                {
                    j = i;
                    break;
                }
            }

            comboBox1.SelectedIndex = j;
            comboBox1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "";

            string abbr = la_building[0][comboBox1.SelectedIndex];

            rc = db.editBuilding(abbr, checkBox1.Checked, textBox1.Text, textBox2.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkBox1.Checked)
                MessageBox.Show("Building " + abbr + " Deleted");
            else
                MessageBox.Show("Building " + abbr + " Changed");

            BuildingChangedHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("buildingEdited"));
            }

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.bldgUpdateToList();

            // End New Code

            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = la_building[0][comboBox1.SelectedIndex];
            textBox2.Text = la_building[1][comboBox1.SelectedIndex];
        }
    }
}

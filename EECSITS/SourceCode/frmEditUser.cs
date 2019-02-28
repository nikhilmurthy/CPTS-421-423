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
    public partial class frmEditUser : Form
    {
        public delegate void UserChangedHandler(object source, PropertyChangedEventArgs e);
        public event UserChangedHandler DataChanged;

        DBA db = DBA.getInstance;
        List<string>[] la_user;
        List<string>[] la_building;

        public frmEditUser()
        {
            la_building = db.selAllBuilding();
            InitializeComponent();
            la_user = db.selAllUser();
            cbBuild.DataSource = la_building[0];
            cbID.DataSource = la_user[5];
        }

        public frmEditUser(int uID)
        {
            int j = 0; // Index
            la_building = db.selAllBuilding();
            InitializeComponent();
            la_user = db.selAllUser();
            cbBuild.DataSource = la_building[0];
            cbID.DataSource = la_user[5];

            for (int i = 0; i < la_user[0].Count; i++)
            {
                if (uID == Convert.ToInt32(la_user[5][i]))
                {
                    j = i;
                    break;
                }
            }

            cbID.SelectedIndex = j;
            cbID.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "";
            int id, bid;

            id = Convert.ToInt32(la_user[5][cbID.SelectedIndex]);
            bid = Convert.ToInt32(la_building[2][cbBuild.SelectedIndex]); // building id

            rc = db.editUser(id, checkDel.Checked, tbFirstName.Text, tbLastName.Text, tbPhone.Text, tbEmail.Text, bid, tbRoomNo.Text,
                checkPur.Checked, checkOwn.Checked, checkEA.Checked, checkFaculty.Checked, checkStudent.Checked, checkStaff.Checked,
                ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkDel.Checked)
                MessageBox.Show("User #" + id + " Deleted");
            else
                MessageBox.Show("User #" + id + " Updated");

            UserChangedHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("userEdited"));
            }

            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFirstName.Text = la_user[1][cbID.SelectedIndex]; // First Name
            tbLastName.Text = la_user[2][cbID.SelectedIndex]; // Last Name
            tbPhone.Text = la_user[3][cbID.SelectedIndex]; // Phone
            tbEmail.Text = la_user[0][cbID.SelectedIndex]; // Email
         //   tbEmail.Text = la_user[0][cbID.SelectedIndex]; // Email
          //  buildID = Convert.ToInt32(la_building[2][cbBuild.SelectedIndex]);
            if (la_building[2][0] != "")
                cbBuild.SelectedIndex = la_building[2].IndexOf(la_user[12][0]);

            tbRoomNo.Text = la_user[13][cbID.SelectedIndex]; // Email
            checkOwn.Checked = Convert.ToBoolean(la_user[6][cbID.SelectedIndex]); // Owner
            checkPur.Checked = Convert.ToBoolean(la_user[7][cbID.SelectedIndex]); // Purchaser
            checkEA.Checked = Convert.ToBoolean(la_user[8][cbID.SelectedIndex]); // Exp Authority
            checkFaculty.Checked = Convert.ToBoolean(la_user[9][cbID.SelectedIndex]); // Faculty
            checkStaff.Checked = Convert.ToBoolean(la_user[10][cbID.SelectedIndex]); // Staff
            checkStudent.Checked = Convert.ToBoolean(la_user[11][cbID.SelectedIndex]); // Student

        }
    }
}

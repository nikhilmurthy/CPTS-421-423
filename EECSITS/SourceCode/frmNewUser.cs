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
    public partial class frmNewUser : Form
    {
        public delegate void UserChangedHandler(object source, PropertyChangedEventArgs e);
        public event UserChangedHandler DataChanged;

        DBA db = DBA.getInstance;
        List<string>[] la_building;

        public frmNewUser()
        {
            la_building = db.selAllBuilding();
            InitializeComponent();
            cbBuild.DataSource = la_building[0];
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int id;
            int bldg_ID;

            int rc = -1;
            string errMsg = "";

            try
            {
                id = Convert.ToInt32(tbUID.Text);
            }

            catch
            {
                MessageBox.Show("Invalid id number");
                return;
            }

            if (id <= 0)
            {
                MessageBox.Show("Invalid id number");
                return;
            }
            bldg_ID = Convert.ToInt32(la_building[2][cbBuild.SelectedIndex]);
            rc = db.newUser(id, tbFName.Text, tbLName.Text, tbPhone.Text, tbEmail.Text, bldg_ID, tbRoomNo.Text, checkPur.Checked
                , checkOwn.Checked, checkEA.Checked, checkFac.Checked, checkStudent.Checked, checkStaff.Checked, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            MessageBox.Show("New User Added");

            UserChangedHandler handler = DataChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("userAdded"));
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

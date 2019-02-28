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
    public partial class frmNewBuilding : Form
    {
        public delegate void BuildChangedHandler(object source, PropertyChangedEventArgs e);
        public event BuildChangedHandler DataChanged;

        DBA db = DBA.getInstance;

        public frmNewBuilding()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int rc = -1;
            string errMsg = "";

            rc = db.newBuilding(tbABBR.Text, tbName.Text, ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            MessageBox.Show("New Building Added");

            BuildChangedHandler handler = DataChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("buildAdded"));
            }

            // New Code

            frmUI fUI;

            fUI = (frmUI)this.MdiParent;

            fUI.bldgUpdateToList();

            // End New Code

            this.Close();
        }
    }
}

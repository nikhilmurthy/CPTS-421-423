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

    public partial class frmUI : Form
    {
        // declare static constants

        const int usrNotLogged = 0;
        const int usrNormal = 1;
        const int usrAdmin = 2;

        // Declare child forms

        frmNewPurchaseRequest frmNewPR;
        frmNewInventory frmNewInv;
        frmNewDept frmNewD;
        frmNewBuilding frmNewB;
        frmNewVendor frmNewV;
        frmNewUser frmNewUsr;
        frmNewCategory frmNewC;
        frmNewBudget frmNewBdgt;

        frmEditBuilding frmEditB;
        frmEditBudget frmEditBdgt;
        frmEditCategory frmEditC;
        frmEditDepartment frmEditD;
        frmEditInventory frmEditI;
        frmEditVendor frmEditV;
        frmEditUser frmEditU;
        frmEditPurchaseRequest frmEditPR;

        frmListDept frmListD;
        frmListBldg frmListB;
        frmListVendor frmListV;
        frmListBudget frmListBdgt;
        frmListPurchaser frmListP;
        frmListOwner frmListO;
        frmListAuth frmListA;
        frmListCategory frmListC;
        frmListInventory frmListI;
        frmListOrder frmListOrder;

        frmLogin frmL;
        frmNewLogin frmNL;
        frmChangePW frmCP;
        frmLoginList frmLL;
        frmEditLogin frmEL;

        Form cf; // current form

        // Declare globals

        DBA db;
        
        public frmUI()
        {
            db = DBA.getInstance;    // data access layer  
            InitializeComponent();

            Global.loginState = Global.usrNotLogged;
            handleLoginState(Global.loginState);
            loginToolStripMenuItem_Click(this, null);

   //       Set the form to 80% of the full screen size

           this.ClientSize = new Size(Screen.PrimaryScreen.WorkingArea.Width * 9 / 10, Screen.PrimaryScreen.WorkingArea.Height * 8 / 10);
    //        this.ClientSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height );
   //         this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

        }

        private void purchaseRequestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((frmNewPR == null) || (frmNewPR.Text == ""))
            {
                frmNewPR = new frmNewPurchaseRequest();
                frmNewPR.MdiParent = this;
                frmNewPR.MaximizeBox = true;
                frmNewPR.MinimizeBox = false;
                frmNewPR.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmNewPR;
            frmNewPR.Show();
        }

        private void inventoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((frmNewInv == null) || (frmNewInv.Text == ""))
            {
                frmNewInv = new frmNewInventory();
                frmNewInv.MdiParent = this;
                frmNewInv.MaximizeBox = true;
                frmNewInv.MinimizeBox = false;
                frmNewInv.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmNewInv;
            frmNewInv.Show();
       
        }

  
        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if ((frmNewD == null) || (frmNewD.Text == ""))
            {
                frmNewD = new frmNewDept();
                frmNewD.MdiParent = this;
                frmNewD.MaximizeBox = true;
                frmNewD.MinimizeBox = false;
                frmNewD.WindowState = FormWindowState.Maximized;
                frmNewD.DataChanged += new frmNewDept.DeptChangedHandler(this.onDeptSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmNewD;
            frmNewD.Show();
        }

        private void buildingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((frmNewB == null) || (frmNewB.Text == ""))
            {
                frmNewB = new frmNewBuilding();
                frmNewB.MdiParent = this;
                frmNewB.MaximizeBox = true;
                frmNewB.MinimizeBox = false;
                frmNewB.WindowState = FormWindowState.Maximized;
                frmNewB.DataChanged += new frmNewBuilding.BuildChangedHandler(this.onBuildSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmNewB;
            frmNewB.Show();
        }

        private void vendorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((frmNewV == null) || (frmNewV.Text == ""))
            {
                frmNewV = new frmNewVendor();
                frmNewV.MdiParent = this;
                frmNewV.MaximizeBox = true;
                frmNewV.MinimizeBox = false;
                frmNewV.WindowState = FormWindowState.Maximized;
                frmNewV.DataChanged += new frmNewVendor.VendorChangedHandler(this.onVendorSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmNewV;
            frmNewV.Show();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((frmNewUsr == null) || (frmNewUsr.Text == ""))
            {
                frmNewUsr = new frmNewUser();
                frmNewUsr.MdiParent = this;
                frmNewUsr.MaximizeBox = true;
                frmNewUsr.MinimizeBox = false;
                frmNewUsr.WindowState = FormWindowState.Maximized;
                frmNewUsr.DataChanged += new frmNewUser.UserChangedHandler(this.onUserSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmNewUsr;
            frmNewUsr.Show();
        }

        private void onDeptSubmit(object source, PropertyChangedEventArgs e)
        {
            if ((frmNewPR != null) && (frmNewPR.Text != ""))
            {
                frmNewPR.frmNewPurchaseReloadDropdown();
            }
            if ((frmNewInv != null) && (frmNewInv.Text != ""))
            {
                frmNewInv.frmNewInventoryDropdown();
            }
        }

        private void onVendorSubmit(object source, PropertyChangedEventArgs e)
        {
            if ((frmNewPR != null) && (frmNewPR.Text != ""))
            {
                frmNewPR.frmNewPurchaseReloadDropdown();
            }

            if ((frmNewInv != null) && (frmNewInv.Text != ""))
            {
                frmNewInv.frmNewInventoryDropdown();
            }
        }

        private void onBuildSubmit(object source, PropertyChangedEventArgs e)
        {
            if ((frmNewInv != null) && (frmNewInv.Text != ""))
            {
                frmNewInv.frmNewInventoryDropdown();
            }
        }

        private void onUserSubmit(object source, PropertyChangedEventArgs e)
        {
            if ((frmNewPR != null) && (frmNewPR.Text != ""))
            {
                frmNewPR.frmNewPurchaseReloadDropdown();
            }
        }

        private void onCategorySubmit(object source, PropertyChangedEventArgs e)
        {
            if ((frmNewInv != null) && (frmNewInv.Text != ""))
            {
                frmNewInv.frmNewInventoryDropdown();
            }
        }

        private void onBudgetSubmit(object source, PropertyChangedEventArgs e)
        {
            if ((frmNewPR != null) && (frmNewPR.Text != ""))
            {
                frmNewPR.frmNewPurchaseReloadDropdown();
            }
        }

        private void onLoginSubmit(object source, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AdminLogin")
                Global.loginState = Global.usrAdmin;
            else
                Global.loginState = Global.usrNormal;

            handleLoginState(Global.loginState);
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((frmNewC == null) || (frmNewC.Text == ""))
            {
                frmNewC = new frmNewCategory();
                frmNewC.MdiParent = this;
                frmNewC.MaximizeBox = true;
                frmNewC.MinimizeBox = false;
                frmNewC.WindowState = FormWindowState.Maximized;
                frmNewC.DataChanged += new frmNewCategory.CategoryChangedHandler(this.onCategorySubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmNewC;
            frmNewC.Show();
        }

        private void budgetToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ((frmNewBdgt == null) || (frmNewBdgt.Text == ""))
            {
                frmNewBdgt = new frmNewBudget();
                frmNewBdgt.MdiParent = this;
                frmNewBdgt.MaximizeBox = true;
                frmNewBdgt.MinimizeBox = false;
                frmNewBdgt.WindowState = FormWindowState.Maximized;
                frmNewBdgt.DataChanged += new frmNewBudget.BudgetChangedHandler(this.onBudgetSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmNewBdgt;
            frmNewBdgt.Show();

        }

        private void deptListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmListD == null) || (frmListD.Text == ""))
            {
                frmListD = new frmListDept();
                frmListD.MdiParent = this;
                frmListD.MaximizeBox = true;
                frmListD.MinimizeBox = false;
                frmListD.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmListD;
            frmListD.Show();

        }

        private void buildingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmListB == null) || (frmListB.Text == ""))
            {
                frmListB = new frmListBldg();
                frmListB.MdiParent = this;
                frmListB.MaximizeBox = true;
                frmListB.MinimizeBox = false;
                frmListB.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmListB;
            frmListB.Show();
        }

        private void vendorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmListV == null) || (frmListV.Text == ""))
            {
                frmListV = new frmListVendor();
                frmListV.MdiParent = this;
                frmListV.MaximizeBox = true;
                frmListV.MinimizeBox = false;
                frmListV.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmListV;
            frmListV.Show();
        }

        private void budgetListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmListBdgt == null) || (frmListBdgt.Text == ""))
            {
                frmListBdgt = new frmListBudget();
                frmListBdgt.MdiParent = this;
                frmListBdgt.MaximizeBox = true;
                frmListBdgt.MinimizeBox = false;
                frmListBdgt.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmListBdgt;
            frmListBdgt.Show();
        }

        private void purchaserListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmListP == null) || (frmListP.Text == ""))
            {
                frmListP = new frmListPurchaser();
                frmListP.MdiParent = this;
                frmListP.MaximizeBox = true;
                frmListP.MinimizeBox = false;
                frmListP.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmListP;
            frmListP.Show();
        }

        private void ownerListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmListO == null) || (frmListO.Text == ""))
            {
                frmListO = new frmListOwner();
                frmListO.MdiParent = this;
                frmListO.MaximizeBox = true;
                frmListO.MinimizeBox = false;
                frmListO.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmListO;
            frmListO.Show();
        }

        private void authorityListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmListA == null) || (frmListA.Text == ""))
            {
                frmListA = new frmListAuth();
                frmListA.MdiParent = this;
                frmListA.MaximizeBox = true;
                frmListA.MinimizeBox = false;
                frmListA.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmListA;
            frmListA.Show();
        }

        private void categoryListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmListC == null) || (frmListC.Text == ""))
            {
                frmListC = new frmListCategory();
                frmListC.MdiParent = this;
                frmListC.MaximizeBox = true;
                frmListC.MinimizeBox = false;
                frmListC.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmListC;
            frmListC.Show();
        }

        private void budgetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((frmEditBdgt == null) || (frmEditBdgt.Text == ""))
            {
                frmEditBdgt = new frmEditBudget();
                frmEditBdgt.MdiParent = this;
                frmEditBdgt.MaximizeBox = true;
                frmEditBdgt.MinimizeBox = false;
                frmEditBdgt.WindowState = FormWindowState.Maximized;
                frmEditBdgt.DataChanged += new frmEditBudget.BudgetChangedHandler(this.onBudgetSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmEditBdgt;
            frmEditBdgt.Show();
        }

        private void categoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((frmEditC == null) || (frmEditC.Text == ""))
            {
                frmEditC = new frmEditCategory();
                frmEditC.MdiParent = this;
                frmEditC.MaximizeBox = true;
                frmEditC.MinimizeBox = false;
                frmEditC.WindowState = FormWindowState.Maximized;
                frmEditC.DataChanged += new frmEditCategory.CategoryChangedHandler(this.onCategorySubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmEditC;
            frmEditC.Show();
        }

        private void buildingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((frmEditB == null) || (frmEditB.Text == ""))
            {
                frmEditB = new frmEditBuilding();
                frmEditB.MdiParent = this;
                frmEditB.MaximizeBox = true;
                frmEditB.MinimizeBox = false;
                frmEditB.WindowState = FormWindowState.Maximized;
                frmEditB.DataChanged += new frmEditBuilding.BuildingChangedHandler(this.onBuildSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmEditB;
            frmEditB.Show();
        }

        private void departmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((frmEditD == null) || (frmEditD.Text == ""))
            {
                frmEditD = new frmEditDepartment();
                frmEditD.MdiParent = this;
                frmEditD.MaximizeBox = true;
                frmEditD.MinimizeBox = false;
                frmEditD.WindowState = FormWindowState.Maximized;
                frmEditD.DataChanged += new frmEditDepartment.DeptChangedHandler(this.onDeptSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmEditD;
            frmEditD.Show();
        }

        private void vendorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((frmEditV == null) || (frmEditV.Text == ""))
            {
                frmEditV = new frmEditVendor();
                frmEditV.MdiParent = this;
                frmEditV.MaximizeBox = true;
                frmEditV.MinimizeBox = false;
                frmEditV.WindowState = FormWindowState.Maximized;
                frmEditV.DataChanged += new frmEditVendor.VendorChangedHandler(this.onVendorSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmEditV;
            frmEditV.Show();
        }

        private void userToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((frmEditU == null) || (frmEditU.Text == ""))
            {
                frmEditU = new frmEditUser();
                frmEditU.MdiParent = this;
                frmEditU.MaximizeBox = true;
                frmEditU.MinimizeBox = false;
                frmEditU.WindowState = FormWindowState.Maximized;
                frmEditU.DataChanged += new frmEditUser.UserChangedHandler(this.onUserSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmEditU;
            frmEditU.Show();
        }

        private void inventoryToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            if ((frmEditI == null) || (frmEditI.Text == ""))
            {
                frmEditI = new frmEditInventory();
                frmEditI.MdiParent = this;
                frmEditI.MaximizeBox = true;
                frmEditI.MinimizeBox = false;
                frmEditI.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmEditI;
            frmEditI.Show();
        }

        private void purchaseRequestToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            if ((frmEditPR == null) || (frmEditPR.Text == ""))
            {
                frmEditPR = new frmEditPurchaseRequest();
                frmEditPR.MdiParent = this;
                frmEditPR.MaximizeBox = true;
                frmEditPR.MinimizeBox = false;
                frmEditPR.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmEditPR;
            frmEditPR.Show();
        }

        private void inventoryListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmListI == null) || (frmListI.Text == ""))
            {
                frmListI = new frmListInventory();
                frmListI.MdiParent = this;
                frmListI.MaximizeBox = true;
                frmListI.MinimizeBox = false;
                frmListI.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmListI;
            frmListI.Show();
        }

        private void purchaseRequestToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if ((frmListOrder == null) || (frmListOrder.Text == ""))
            {
                frmListOrder = new frmListOrder();
                frmListOrder.MdiParent = this;
                frmListOrder.MaximizeBox = true;
                frmListOrder.MinimizeBox = false;
                frmListOrder.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmListOrder;
            frmListOrder.Show();
        }

        // New Code

        private void loaderror()
        {
            MessageBox.Show("Form already loaded. Refer to WINDOW to access.");
        }

        // Functions to go from List form to Edit form

        public void budgetListToEdit(int bID)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmEditBdgt)
                {
                    loaderror(); // form already loaded
                    return;
                }
            }

            frmEditBdgt = new frmEditBudget(bID);
            frmEditBdgt.MdiParent = this;
            frmEditBdgt.MaximizeBox = true;
            frmEditBdgt.MinimizeBox = false;
            frmEditBdgt.WindowState = FormWindowState.Maximized;
            frmEditBdgt.DataChanged += new frmEditBudget.BudgetChangedHandler(this.onBudgetSubmit);
            cf = frmEditBdgt;
            frmEditBdgt.Show();
        }

        public void bldgListToEdit(int bID)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmEditB)
                {
                    loaderror(); // form already loaded
                    return;
                }
            }

            frmEditB = new frmEditBuilding(bID);
            frmEditB.MdiParent = this;
            frmEditB.MaximizeBox = true;
            frmEditB.MinimizeBox = false;
            frmEditB.WindowState = FormWindowState.Maximized;
            frmEditB.DataChanged += new frmEditBuilding.BuildingChangedHandler(this.onBuildSubmit);
            cf = frmEditB;
            frmEditB.Show();
        }

        public void catgListToEdit(int cID)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmEditC)
                {
                    loaderror(); // form already loaded
                    return;
                }
            }

            frmEditC = new frmEditCategory(cID);
            frmEditC.MdiParent = this;
            frmEditC.MaximizeBox = true;
            frmEditC.MinimizeBox = false;
            frmEditC.WindowState = FormWindowState.Maximized;
            frmEditC.DataChanged += new frmEditCategory.CategoryChangedHandler(this.onCategorySubmit);
            cf = frmEditC;
            frmEditC.Show();
        }

        public void deptListToEdit(int dID)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmEditD)
                {
                    loaderror(); // form already loaded
                    return;
                }
            }

            frmEditD = new frmEditDepartment(dID);
            frmEditD.MdiParent = this;
            frmEditD.MaximizeBox = true;
            frmEditD.MinimizeBox = false;
            frmEditD.WindowState = FormWindowState.Maximized;
            frmEditD.DataChanged += new frmEditDepartment.DeptChangedHandler(this.onDeptSubmit);
            cf = frmEditD;
            frmEditD.Show();
        }

        public void vendorListToEdit(int vID)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmEditV)
                {
                    loaderror(); // form already loaded
                    return;
                }
            }

            frmEditV = new frmEditVendor(vID);
            frmEditV.MdiParent = this;
            frmEditV.MaximizeBox = true;
            frmEditV.MinimizeBox = false;
            frmEditV.WindowState = FormWindowState.Maximized;
            frmEditV.DataChanged += new frmEditVendor.VendorChangedHandler(this.onVendorSubmit);
            cf = frmEditV;
            frmEditV.Show();
        }

        public void userListToEdit(int uID)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmEditU)
                {
                    loaderror(); // form already loaded
                    return;
                }
            }

            frmEditU = new frmEditUser(uID);
            frmEditU.MdiParent = this;
            frmEditU.MaximizeBox = true;
            frmEditU.MinimizeBox = false;
            frmEditU.WindowState = FormWindowState.Maximized;
            frmEditU.DataChanged += new frmEditUser.UserChangedHandler(this.onUserSubmit);
            cf = frmEditU;
            frmEditU.Show();
        }

        public void invListToEdit(int barcode)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmEditI)
                {
                    loaderror(); // form already loaded
                    return;
                }
            }

            frmEditI = new frmEditInventory(barcode);
            frmEditI.MdiParent = this;
            frmEditI.MaximizeBox = true;
            frmEditI.MinimizeBox = false;
            frmEditI.WindowState = FormWindowState.Maximized;
            cf = frmEditI;
            frmEditI.Show();
        }

        public void orderListToEdit(int orderNo)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmEditPR)
                {
                    loaderror(); // form already loaded
                    return;
                }
            }

            frmEditPR = new frmEditPurchaseRequest(orderNo);
            frmEditPR.MdiParent = this;
            frmEditPR.MaximizeBox = true;
            frmEditPR.MinimizeBox = false;
            frmEditPR.WindowState = FormWindowState.Maximized;
            cf = frmEditPR;
            frmEditPR.Show();
        }

        public void loginListToEdit(int idNum)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmEL)
                {
                    loaderror(); // form already loaded
                    return;
                }
                //    f.Close();   // the other option is to close the open form and allow user to edit the new form
            }

            frmEL = new frmEditLogin(idNum);
            frmEL.MdiParent = this;
            frmEL.MaximizeBox = true;
            frmEL.MinimizeBox = false;
            frmEL.WindowState = FormWindowState.Maximized;
            cf = frmEL;
            frmEL.Show();
        }

        // Functions to go from New/Edit form to List form

        public void budgetUpdateToList()
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmListBdgt)
                    f.Close();
            }

            frmListBdgt = new frmListBudget();
            frmListBdgt.MdiParent = this;
            frmListBdgt.MaximizeBox = true;
            frmListBdgt.MinimizeBox = false;
            frmListBdgt.WindowState = FormWindowState.Maximized;
            cf = frmListBdgt;
            frmListBdgt.Show();
        }

        public void bldgUpdateToList()
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmListB)
                    f.Close();
            }

            frmListB = new frmListBldg();
            frmListB.MdiParent = this;
            frmListB.MaximizeBox = true;
            frmListB.MinimizeBox = false;
            frmListB.WindowState = FormWindowState.Maximized;
            cf = frmListB;
            frmListB.Show();
        }

        public void catgUpdateToList()
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmListC)
                    f.Close();
            }

            frmListC = new frmListCategory();
            frmListC.MdiParent = this;
            frmListC.MaximizeBox = true;
            frmListC.MinimizeBox = false;
            frmListC.WindowState = FormWindowState.Maximized;
            cf = frmListC;
            frmListC.Show();
        }

        public void deptUpdateToList()
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmListD)
                    f.Close();
            }

            frmListD = new frmListDept();
            frmListD.MdiParent = this;
            frmListD.MaximizeBox = true;
            frmListD.MinimizeBox = false;
            frmListD.WindowState = FormWindowState.Maximized;
            cf = frmListD;
            frmListD.Show();
        }

        public void vendorUpdateToList()
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmListV)
                    f.Close();
            }

            frmListV = new frmListVendor();
            frmListV.MdiParent = this;
            frmListV.MaximizeBox = true;
            frmListV.MinimizeBox = false;
            frmListV.WindowState = FormWindowState.Maximized;
            cf = frmListV;
            frmListV.Show();
        }

        public void invUpdateToList(int invID)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmListI)
                    f.Close();
            }

            frmListI = new frmListInventory(invID);
            frmListI.MdiParent = this;
            frmListI.MaximizeBox = true;
            frmListI.MinimizeBox = false;
            frmListI.WindowState = FormWindowState.Maximized;
            cf = frmListI;
            frmListI.Show();
        }

        public void orderUpdateToList(int ordNo)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmListOrder)
                    f.Close();
            }

            frmListOrder = new frmListOrder(ordNo);
            frmListOrder.MdiParent = this;
            frmListOrder.MaximizeBox = true;
            frmListOrder.MinimizeBox = false;
            frmListOrder.WindowState = FormWindowState.Maximized;
            cf = frmListOrder;
            frmListOrder.Show();
        }

        public void loginUpdateToList()
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f == frmLL)
                    f.Close();
            }

            frmLL = new frmLoginList();
            frmLL.MdiParent = this;
            frmLL.MaximizeBox = true;
            frmLL.MinimizeBox = false;
            frmLL.WindowState = FormWindowState.Maximized;
            cf = frmLL;
            frmLL.Show();
        }


        // End New Code

        private void handleLoginState (int loginState)
        {
            if (Global.loginState == Global.usrNotLogged)
            {
                adminToolStripMenuItem.Visible = false;
                newToolStripMenuItem.Visible = false;
                editToolStripMenuItem.Visible = false;
                reportToolStripMenuItem.Visible = false;
                windowToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem.Visible = false;
                loginToolStripMenuItem.Visible = false;
                cpwToolStripMenuItem.Visible = false;
            }
            else if (Global.loginState == Global.usrNormal)
            {
                adminToolStripMenuItem.Visible = false;
                newToolStripMenuItem.Visible = false;
                editToolStripMenuItem.Visible = false;
                reportToolStripMenuItem.Visible = true;
                windowToolStripMenuItem.Visible = true;
                logoutToolStripMenuItem.Visible = true;
                loginToolStripMenuItem.Visible = false;
                cpwToolStripMenuItem.Visible = true;
            }
            else if (Global.loginState == Global.usrAdmin)
            {
                adminToolStripMenuItem.Visible = true;
                newToolStripMenuItem.Visible = true;
                editToolStripMenuItem.Visible = true;
                reportToolStripMenuItem.Visible = true;
                windowToolStripMenuItem.Visible = true;
                logoutToolStripMenuItem.Visible = true;
                loginToolStripMenuItem.Visible = false;
                cpwToolStripMenuItem.Visible = true;
            };


        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmL == null) || (frmL.Text == ""))
            {
                frmL = new frmLogin();
                frmL.MdiParent = this;
                frmL.MaximizeBox = true;
                frmL.MinimizeBox = false;
                frmL.WindowState = FormWindowState.Maximized;
                frmL.UsrLoggedIn += new frmLogin.UsrLoginHandler(this.onLoginSubmit);
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmL;
            frmL.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
                f.Close();

            Global.loginState = Global.usrNotLogged;

            Global.loginName = null;

            handleLoginState(Global.loginState);

            loginToolStripMenuItem_Click(this, null);
        }

        private void aboutEECSInventroyTrackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version: 1.0.0");
        }

        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmNL == null) || (frmNL.Text == ""))
            {
                frmNL = new frmNewLogin();
                frmNL.MdiParent = this;
                frmNL.MaximizeBox = true;
                frmNL.MinimizeBox = false;
                frmNL.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmNL;
            frmNL.Show();
        }

        private void cpwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmCP == null) || (frmCP.Text == ""))
            {
                frmCP = new frmChangePW();
                frmCP.MdiParent = this;
                frmCP.MaximizeBox = true;
                frmCP.MinimizeBox = false;
                frmCP.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmCP;
            frmCP.Show();
        }

        private void listUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmLL == null) || (frmLL.Text == ""))
            {
                frmLL = new frmLoginList();
                frmLL.MdiParent = this;
                frmLL.MaximizeBox = true;
                frmLL.MinimizeBox = false;
                frmLL.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmLL;
            frmLL.Show();
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((frmEL == null) || (frmEL.Text == ""))
            {
                frmEL = new frmEditLogin();
                frmEL.MdiParent = this;
                frmEL.MaximizeBox = true;
                frmEL.MinimizeBox = false;
                frmEL.WindowState = FormWindowState.Maximized;
            }

            else
            {
                loaderror();
                return;
            }

            cf = frmEL;
            frmEL.Show();
        }
    }
}

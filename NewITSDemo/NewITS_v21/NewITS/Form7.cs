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

 //       UserStore us = UserStore.getInstance;

        DBA db = DBA.getInstance;
        public frmNewUser()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string err;
            int id;
            int rc = -1;
            string errMsg = "";

            try
            {
                id = Convert.ToInt32(textBox5.Text);
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

//            err = db.insertUser(textBox1.Text, textBox4.Text, textBox2.Text, textBox3.Text, checkBox2.Checked
//                , checkBox4.Checked, checkBox3.Checked, checkBox1.Checked, checkBox5.Checked, checkBox6.Checked);
            rc = db.newUser(id, textBox1.Text, textBox4.Text, textBox2.Text, textBox3.Text, checkBox2.Checked
                , checkBox4.Checked, checkBox3.Checked, checkBox1.Checked, checkBox5.Checked, checkBox6.Checked, ref errMsg);
//            us.NewUser(textBox1.Text, textBox4.Text, textBox2.Text, textBox3.Text, checkBox2.Checked
//                , checkBox4.Checked, checkBox3.Checked, checkBox1.Checked, checkBox5.Checked, checkBox6.Checked);

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

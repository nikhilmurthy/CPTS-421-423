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

        public frmEditUser()
        {
            InitializeComponent();
            la_user = db.selAllUser();
            comboBox1.DataSource = la_user[5];
        }

        public frmEditUser(int uID)
        {
            int j = 0; // Index
            InitializeComponent();
            la_user = db.selAllUser();
            comboBox1.DataSource = la_user[5];
            for (int i = 0; i < la_user[0].Count; i++)
            {
                if (uID == Convert.ToInt32(la_user[5][i]))
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
            int id;

            id = Convert.ToInt32(la_user[5][comboBox1.SelectedIndex]);

            rc = db.editUser(id, checkBox7.Checked, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text,
                checkBox1.Checked, checkBox4.Checked, checkBox2.Checked, checkBox3.Checked, checkBox6.Checked, checkBox5.Checked,
                ref errMsg);

            if (rc < 0)
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (checkBox7.Checked)
                MessageBox.Show("User " + id + " Deleted");
            else
                MessageBox.Show("User " + id + " Fields Changed");

            UserChangedHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("userEdited"));
            }

            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = la_user[1][comboBox1.SelectedIndex]; // First Name
            textBox2.Text = la_user[2][comboBox1.SelectedIndex]; // Last Name
            textBox3.Text = la_user[3][comboBox1.SelectedIndex]; // Phone
            textBox4.Text = la_user[0][comboBox1.SelectedIndex]; // Email
            checkBox4.Checked = Convert.ToBoolean(la_user[6][comboBox1.SelectedIndex]); // Owner
            checkBox1.Checked = Convert.ToBoolean(la_user[7][comboBox1.SelectedIndex]); // Purchaser
            checkBox2.Checked = Convert.ToBoolean(la_user[8][comboBox1.SelectedIndex]); // Exp Authority
            checkBox3.Checked = Convert.ToBoolean(la_user[9][comboBox1.SelectedIndex]); // Faculty
            checkBox5.Checked = Convert.ToBoolean(la_user[10][comboBox1.SelectedIndex]); // Staff
            checkBox6.Checked = Convert.ToBoolean(la_user[11][comboBox1.SelectedIndex]); // Student

        }
    }
}

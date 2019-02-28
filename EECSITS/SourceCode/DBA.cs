using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;

namespace NewITS
{
    class DBA
    {
        private MySqlConnection connection;

        private static DBA dba = null;

        public static DBA getInstance
        {
            get
            {
                if (dba == null)
                    dba = new DBA();

                return dba;
            }
        }

       //Constructor
        private DBA()
        {
            Initialize();
        }

        // Initiaize: Connect to the back-end database

        private void Initialize()
        {
            string server = "localhost";
            string database = "imdb";
            string uid = "root";
            string pwd = "my_pwd"; // Please change me
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
		    database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pwd + ";";
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                // handle common errors
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Check network connection to the server");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password");
                        break;
                }
            }

            MessageBox.Show("Connection made to the database");
        }

        // insertBudget
        //  - Invokes a store proc to insert a new Budget record to the database
        //  - Returns null if no error; otherwise the error message

        public string insertBudget(int bNum, string desc)
        {
            string err = null;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spInsertBudget";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_budget_no", bNum);
                cmd.Parameters["?_budget_no"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_description", desc);
                cmd.Parameters["?_description"].Direction = ParameterDirection.Input;

                cmd.ExecuteNonQuery();
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                err = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            return err;
        }

        // insertDept 
        //  - Invokes a store proc to insert a new Dept record to the database
        //  - Returns null if no error; otherwise the error message

        public string insertDept(string abbr, string name)
        {
            string err = null;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spInsertDept";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_abbr", abbr);
                cmd.Parameters["?_abbr"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_name", name);
                cmd.Parameters["?_name"].Direction = ParameterDirection.Input;

                cmd.ExecuteNonQuery();
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                err = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            return err;
        }

        // insertBuilding
        //  - Invokes a store proc to insert a new Building record to the database
        //  - Returns null if no error; otherwise the error message

        public string insertBuilding(string abbr, string name)
        {
            string err = null;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spInsertBuilding";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_abbr", abbr);
                cmd.Parameters["?_abbr"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_name", name);
                cmd.Parameters["?_name"].Direction = ParameterDirection.Input;

                cmd.ExecuteNonQuery();
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                err = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            return err;
        }

        // insertCategory 
        //  - Invokes a store proc to insert a new Category record to the database       
        //  - Returns null if no error; otherwise the error message

        public string insertCategory(string name)
        {
            string err = null;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spInsertCategory";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_name", name);
                cmd.Parameters["?_name"].Direction = ParameterDirection.Input;

                cmd.ExecuteNonQuery();
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                err = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            return err;
        }

        // insertUser 
        //  - Invokes a store proc to insert a new Budget record to the database
        //  - Returns null if no error; otherwise the error message

        public string insertUser(string firstname, string lastname, string phone, string email
            , bool p, bool o, bool ea, bool f, bool student, bool staff)
        {
            string err = null;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spInsertUser";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_email", email);
                cmd.Parameters["?_email"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_first_name", firstname);
                cmd.Parameters["?_first_name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_last_name", lastname);
                cmd.Parameters["?_last_name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_phone", phone);
                cmd.Parameters["?_phone"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_is_owner", o);
                cmd.Parameters["?_is_owner"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_is_purchaser", p);
                cmd.Parameters["?_is_purchaser"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_is_exp_authority", ea);
                cmd.Parameters["?_is_exp_authority"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_is_faculty", f);
                cmd.Parameters["?_is_faculty"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_is_staff", staff);
                cmd.Parameters["?_is_staff"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_is_student", student);
                cmd.Parameters["?_is_student"].Direction = ParameterDirection.Input;

                cmd.ExecuteNonQuery();
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                err = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            return err;
        }

        // insertVendor 
        //  - Invokes a store proc to insert a new Vendor record to the database       
        //  - Returns null if no error; otherwise the error message

        public string insertVendor(string name, string address, string phone, string fax, string website)
        {
            string err = null;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spInsertVendor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_name", name);
                cmd.Parameters["?_name"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_address", address);
                cmd.Parameters["?_address"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_phone", phone);
                cmd.Parameters["?_phone"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_fax", fax);
                cmd.Parameters["?_fax"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_website", website);
                cmd.Parameters["?_website"].Direction = ParameterDirection.Input;

                cmd.ExecuteNonQuery();
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                err = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            return err;
        }

        // insertVendor 
        //  - Invokes a store proc to insert a new Order record to the database       
        //  - Returns null if no error; otherwise the error message

        public int insertOrderLineItem(string description, string date, int vendorID, int shipMode, int budgetNo,
                                   int purchaserID, int ownerID, int deptID, int paymentType, int facultyID, int authID,
                                   double total, int count, List<string> catalogL, List<string> descL, List<string> qtyL, List<string> unitL, List<string> upL, 
                                   List <string> amtL, ref string errMsg)
        {
 
            int orderID = -1;

            string catalogS = "", descS = "", qtyS = "", upS = "", amtS = "", unitS = "";

            errMsg = "";

            foreach (string s in catalogL)
            {
                catalogS += s + "|";
            };

            foreach (string s in descL)
            {
                descS += s + "|";
            };

            foreach (string s in qtyL)
            {
                qtyS += s + "|";
            };

            foreach (string s in unitL)
            {
                unitS += s + "|";
            };

            foreach (string s in upL)
            {
                upS += s + "|";
            };

            foreach (string s in amtL)
            {
                amtS += s + "|";
            };

  

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spInsertOrderLineItem";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_description", description);
                cmd.Parameters["?_description"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_date", date);
                cmd.Parameters["?_date"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_vendor_id", vendorID);
                cmd.Parameters["?_vendor_id"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_ship_mode", shipMode);
                cmd.Parameters["?_ship_mode"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_budget_no", budgetNo);
                cmd.Parameters["?_budget_No"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_purchaser_id", purchaserID);
                cmd.Parameters["?_purchaser_id"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_owner_id", ownerID);
                cmd.Parameters["?_owner_id"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_dept_id", deptID);
                cmd.Parameters["?_dept_id"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_payment_type", paymentType);
                cmd.Parameters["?_payment_type"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_sign_faculty_id", facultyID);
                cmd.Parameters["?_sign_faculty_id"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_sign_auth_id", authID);
                cmd.Parameters["?_sign_auth_id"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_total_amt", total);
                cmd.Parameters["?_total_amt"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_count", count);
                cmd.Parameters["?_count"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_ca_list", catalogS);
                cmd.Parameters["?_ca_list"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_desc_list", descS);
                cmd.Parameters["?_desc_list"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_qty_list", qtyS);
                cmd.Parameters["?_qty_list"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_unit_list", unitS);
                cmd.Parameters["?_unit_list"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_up_list", upS);
                cmd.Parameters["?_up_list"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("?_amt_list", amtS);
                cmd.Parameters["?_amt_list"].Direction = ParameterDirection.Input;

                cmd.Parameters.Add(new MySqlParameter("?_order_id", MySqlDbType.Int32));
                cmd.Parameters["?_order_id"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                orderID = (int)cmd.Parameters["?_order_id"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
            }


            return orderID;
        }

        public int newPurchaseRequest(int orderID, string comment, string date, int vendorID, int shipMode, int budgetNo,
                           int purchaserID, int ownerID, int deptID, int paymentType, int facultyID, int authID,
                           double total, int count, List<string> catalogL, List<string> descL, List<string> qtyL, List<string> unitL, List<string> upL,
                           List<string> amtL, ref string errMsg)
        {

            int rc = -1;

            string catalogS = "", descS = "", qtyS = "", upS = "", amtS = "", unitS = "";

            errMsg = "";

            foreach (string s in catalogL)
            {
                catalogS += s + "|";
            };

            foreach (string s in descL)
            {
                descS += s + "|";
            };

            foreach (string s in qtyL)
            {
                qtyS += s + "|";
            };

            foreach (string s in unitL)
            {
                unitS += s + "|";
            };

            foreach (string s in upL)
            {
                upS += s + "|";
            };

            foreach (string s in amtL)
            {
                amtS += s + "|";
            };



            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spNewPurchaseRequest";

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_order_id", orderID);

                cmd.Parameters.AddWithValue("?_date", date);

                cmd.Parameters.AddWithValue("?_vendor_id", vendorID);

                cmd.Parameters.AddWithValue("?_ship_mode", shipMode);

                cmd.Parameters.AddWithValue("?_budget_no", budgetNo);

                cmd.Parameters.AddWithValue("?_purchaser_id", purchaserID);

                cmd.Parameters.AddWithValue("?_owner_id", ownerID);

                cmd.Parameters.AddWithValue("?_dept_id", deptID);

                cmd.Parameters.AddWithValue("?_payment_type", paymentType);

                cmd.Parameters.AddWithValue("?_sign_faculty_id", facultyID);

                cmd.Parameters.AddWithValue("?_sign_auth_id", authID);

                cmd.Parameters.AddWithValue("?_total_amt", total);

                cmd.Parameters.AddWithValue("?_count", count);

                cmd.Parameters.AddWithValue("?_comment", comment);

                cmd.Parameters.AddWithValue("?_ca_list", catalogS);

                cmd.Parameters.AddWithValue("?_desc_list", descS);

                cmd.Parameters.AddWithValue("?_qty_list", qtyS);

                cmd.Parameters.AddWithValue("?_unit_list", unitS);

                cmd.Parameters.AddWithValue("?_up_list", upS);

                cmd.Parameters.AddWithValue("?_amt_list", amtS);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
            } 


            return rc;
        }

        // insertInventory

        public int newInventory( int invID, string description, int catgID, int bldgID, string roomNo, int ownerID, int orderID, int lineNo, int vendorID,
                                    int deptID, string deliveryDate, string purchaseDate, double purchasePrice, string manf, string transNo,
                                    string serialNo, string modelNo, string comment, ref string errMsg)

        {

            errMsg = "";

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spNewInventory";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("?_inv_id", invID);
                cmd.Parameters.AddWithValue("?_description", description);
                cmd.Parameters.AddWithValue("?_catg_id", catgID);
                cmd.Parameters.AddWithValue("?_bldg_id", bldgID);
                cmd.Parameters.AddWithValue("?_room_no", roomNo);        
                cmd.Parameters.AddWithValue("?_owner_id", ownerID);
                cmd.Parameters.AddWithValue("?_order_no", orderID);
                cmd.Parameters.AddWithValue("?_line_no", lineNo);
                cmd.Parameters.AddWithValue("?_vendor_id", vendorID);
                cmd.Parameters.AddWithValue("?_dept_id", deptID);
                cmd.Parameters.AddWithValue("?_delivery_date", deliveryDate);
                cmd.Parameters.AddWithValue("?_purchase_date", purchaseDate);
                cmd.Parameters.AddWithValue("?_purchase_price", purchasePrice);
                cmd.Parameters.AddWithValue("?_manf", manf);
                cmd.Parameters.AddWithValue("?_trans_no", transNo);
                cmd.Parameters.AddWithValue("?_serial_no", serialNo);
                cmd.Parameters.AddWithValue("?_model_no", modelNo);
                cmd.Parameters.AddWithValue("?_comment", comment); 

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;
                 

                cmd.ExecuteNonQuery();

                 invID = (int)cmd.Parameters["?_rc"].Value;
                 errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }

            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            } 
 

            return invID;
        }



        // selAbbrDept - returns the list of department abbrevations

        public List<string> selAbbrDept()
        {
            List<string> itemList = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAbbrDept";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["abbr"]);
                itemList.Add(item);
            }

            dr.Close();
            return itemList;
        }

        // selAbbrBuilding - returns the list of department abbrevations

        public List<string> selAbbrBuilding()
        {
            List<string> itemList = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAbbrBuilding";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["abbr"]);
                itemList.Add(item);
            }

            dr.Close();
            return itemList;
        }
        public List<string> selNameCategory()
        {
            List<string> itemList = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelNameCategory";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["name"]);
                itemList.Add(item);
            }

            dr.Close();
            return itemList;
        }
        public List<string> selNoBudget()
        {
            List<string> itemList = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelNoDescBudget";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["budget_no"]);
                itemList.Add(item);
            }

            dr.Close();
            return itemList;
        }

        public List<string>[] selAllBudget()
        {
            List<string>[] itemList = new List<string>[3];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllBudget";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item2;
                string item = Convert.ToString(dr["budget_no"]);
                itemList[0].Add(item);
                item2 = Convert.ToString(dr["description"]);
                itemList[1].Add(item2);
                itemList[2].Add(item + " (" + item2 + ")");

            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllVendor()
        {
            List<string>[] itemList = new List<string>[6];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllVendor";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["name"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["address"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["phone"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["fax"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["website"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["vendor_id"]);
                itemList[5].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllBuilding()
        {
            List<string>[] itemList = new List<string>[3];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllBuilding";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["abbr"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["name"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["bldg_id"]);
                itemList[2].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllDept()
        {
            List<string>[] itemList = new List<string>[3];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllDept";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["abbr"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["name"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["dept_id"]);
                itemList[2].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllCategory()
        {
            List<string>[] itemList = new List<string>[2];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllCategory";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["name"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["catg_id"]);
                itemList[1].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllAuth()
        {
            List<string>[] itemList = new List<string>[6];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllAuth";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["email"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["first_name"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["last_name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["phone"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["user_id"]);
                itemList[5].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllFaculty()
        {
            List<string>[] itemList = new List<string>[6];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllFaculty";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["email"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["first_name"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["last_name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["phone"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["user_id"]);
                itemList[5].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllPurchaser()
        {
            List<string>[] itemList = new List<string>[6];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllPurchaser";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["email"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["first_name"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["last_name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["phone"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["user_id"]);
                itemList[5].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllOwner()
        {
            List<string>[] itemList = new List<string>[9];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllOwner";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["email"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["first_name"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["last_name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["phone"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["user_id"]);
                itemList[5].Add(item);
                item = Convert.ToString(dr["bldg_id"]);
                itemList[6].Add(item);
                item = Convert.ToString(dr["room_no"]);
                itemList[7].Add(item);
                item = Convert.ToString(dr["abbr"]);
                itemList[8].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllUser()
        {
            List<string>[] itemList = new List<string>[14];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            itemList[9] = new List<string>();
            itemList[10] = new List<string>();
            itemList[11] = new List<string>();
            itemList[12] = new List<string>();
            itemList[13] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllUser";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["email"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["first_name"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["last_name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["phone"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["user_id"]);
                itemList[5].Add(item);
                item = Convert.ToString(dr["is_owner"]);
                itemList[6].Add(item);
                item = Convert.ToString(dr["is_purchaser"]);
                itemList[7].Add(item);
                item = Convert.ToString(dr["is_exp_authority"]);
                itemList[8].Add(item);
                item = Convert.ToString(dr["is_faculty"]);
                itemList[9].Add(item);
                item = Convert.ToString(dr["is_staff"]);
                itemList[10].Add(item);
                item = Convert.ToString(dr["is_student"]);
                itemList[11].Add(item);
                item = Convert.ToString(dr["bldg_id"]);
                itemList[12].Add(item);
                item = Convert.ToString(dr["room_no"]);
                itemList[13].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllLogin2()
        {
            List<string>[] itemList = new List<string>[8];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllLogin2";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["login_name"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["first_name"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["last_name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["passwrd"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["email"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["phone"]);
                itemList[5].Add(item);
                item = Convert.ToString(dr["is_admin"]);
                itemList[6].Add(item);
                item = Convert.ToString(dr["login_id"]);
                itemList[7].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string> selOrdersZero()
        {
            List<string> itemList = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelOrdersZero";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["order_id"]);
                itemList.Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selLineItemOne(int order_id)
        {
            List<string>[] itemList = new List<string>[9];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?_order_id", order_id);
            cmd.Parameters["?_order_id"].Direction = ParameterDirection.Input;
            cmd.CommandText = "spSelLineItemOne";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["line_no"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["catelog_no"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["description"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["qty"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["unit"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["unit_price"]);
                itemList[5].Add(item);
                item = Convert.ToString(dr["total_amt"]);
                itemList[6].Add(item);
                item = Convert.ToString(dr["qty_recv"]);
                itemList[7].Add(item);
                item = Convert.ToString(dr["is_delivered"]);
                itemList[8].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selOrdersOne(int order_id)
        {
            List<string>[] itemList = new List<string>[11];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            itemList[9] = new List<string>();
            itemList[10] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?_order_id", order_id);
            cmd.Parameters["?_order_id"].Direction = ParameterDirection.Input;
            cmd.CommandText = "spSelOrdersOne";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["date"]);
                item = DateTime.Parse(item).ToString("MM/dd/yyyy");
                itemList[0].Add(item);
                item = Convert.ToString(dr["vendor_id"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["owner_id"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["budget_no"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["purchaser_id"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["owner_id"]);
                itemList[5].Add(item);
                item = Convert.ToString(dr["dept_id"]);
                itemList[6].Add(item);
                item = Convert.ToString(dr["sign_faculty_id"]);
                itemList[7].Add(item);
                item = Convert.ToString(dr["sign_auth_id"]);
                itemList[8].Add(item);
                item = Convert.ToString(dr["total_amt"]);
                itemList[9].Add(item);
                item = Convert.ToString(dr["comment"]);
                itemList[10].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selInvOne(int inv_id)
        {
            List<string>[] itemList = new List<string>[11];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            itemList[9] = new List<string>();
            itemList[10] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?_inv_id", inv_id);
            cmd.Parameters["?_inv_id"].Direction = ParameterDirection.Input;
            cmd.CommandText = "spSelInvOne";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["order_id"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["delivery_date"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["purchase_date"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["purchase_price"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["description"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["manf"]);
                itemList[5].Add(item);
                item = Convert.ToString(dr["trans_no"]);
                itemList[6].Add(item);
                item = Convert.ToString(dr["serial_no"]);
                itemList[7].Add(item);
                item = Convert.ToString(dr["model_no"]);
                itemList[8].Add(item);
                item = Convert.ToString(dr["room_no"]);
                itemList[9].Add(item);
                item = Convert.ToString(dr["comment"]);
                itemList[10].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selInvReport(int min_ownerID , int max_ownerID, int min_bldgID, 
        int max_bldgID, int min_catgID, int max_catgID, int min_orderno, int max_orderno, int min_invID, int max_invID,
            DateTime sDate, DateTime eDate)
        {
            List<string>[] itemList = new List<string>[11];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            itemList[9] = new List<string>();
            itemList[10] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?_min_ownerID", min_ownerID);
            cmd.Parameters["?_min_ownerID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_max_ownerID", max_ownerID);
            cmd.Parameters["?_max_ownerID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_min_bldgID", min_bldgID);
            cmd.Parameters["?_min_bldgID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_max_bldgID", max_bldgID);
            cmd.Parameters["?_max_bldgID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_min_catgID", min_catgID);
            cmd.Parameters["?_min_catgID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_max_catgID", max_catgID);
            cmd.Parameters["?_max_catgID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_min_orderno", min_orderno);
            cmd.Parameters["?_min_orderno"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_max_orderno", max_orderno);
            cmd.Parameters["?_max_orderno"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_min_invID", min_invID);
            cmd.Parameters["?_min_invID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_max_invID", max_invID);
            cmd.Parameters["?_max_invID"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_sDate", sDate);
            cmd.Parameters["?_sDate"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("?_eDate", eDate);
            cmd.Parameters["?_eDate"].Direction = ParameterDirection.Input;
            cmd.CommandText = "spSelInvReport";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["inv_id"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["delivery_date"]);
                item = DateTime.Parse(item).ToString("MM/dd/yyyy");
                itemList[1].Add(item);
                item = Convert.ToString(dr["serial_no"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["order_id"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["name"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[5].Add(item);
                item = Convert.ToString(dr["abbr"]);
                itemList[6].Add(item);
                item = Convert.ToString(dr["room_no"]);
                itemList[7].Add(item);
                item = Convert.ToString(dr["description"]);
                itemList[8].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selOrderReport(int min_reqID, int max_reqID, int min_purchaserID, int max_purchaserID,
            int min_authID, int max_authID, int min_facultyID, int max_facultyID, int min_deptID, int max_deptID, 
            int min_budgetID, int max_budgetID, int min_vendorID, int max_vendorID, int min_orderno, int max_orderno,
            DateTime sDate, DateTime eDate, int qty_diff)
        {
            List<string>[] itemList = new List<string>[17];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            itemList[9] = new List<string>();
            itemList[10] = new List<string>();
            itemList[11] = new List<string>();
            itemList[12] = new List<string>();
            itemList[13] = new List<string>();
            itemList[14] = new List<string>();
            itemList[15] = new List<string>();
            itemList[16] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?_min_reqID", min_reqID);
            cmd.Parameters.AddWithValue("?_max_reqID", max_reqID);
            cmd.Parameters.AddWithValue("?_min_purchaserID", min_purchaserID);
            cmd.Parameters.AddWithValue("?_max_purchaserID", max_purchaserID);
            cmd.Parameters.AddWithValue("?_min_authID", min_authID);
            cmd.Parameters.AddWithValue("?_max_authID", max_authID);
            cmd.Parameters.AddWithValue("?_min_facultyID", min_facultyID);
            cmd.Parameters.AddWithValue("?_max_facultyID", max_facultyID);
            cmd.Parameters.AddWithValue("?_min_deptID", min_deptID);
            cmd.Parameters.AddWithValue("?_max_deptID", max_deptID);
            cmd.Parameters.AddWithValue("?_min_budgetID", min_budgetID);
            cmd.Parameters.AddWithValue("?_max_budgetID", max_budgetID);
            cmd.Parameters.AddWithValue("?_min_vendorID", min_vendorID);
            cmd.Parameters.AddWithValue("?_max_vendorID", max_vendorID);
            cmd.Parameters.AddWithValue("?_min_orderno", min_orderno);
            cmd.Parameters.AddWithValue("?_max_orderno", max_orderno);
            cmd.Parameters.AddWithValue("?_sDate", sDate);
            cmd.Parameters.AddWithValue("?_eDate", eDate);
            cmd.Parameters.AddWithValue("?_qty_diff", qty_diff);
            cmd.CommandText = "spSelOrderReport";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["order_id"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["date"]);
                item = DateTime.Parse(item).ToString("MM/dd/yyyy");
                itemList[1].Add(item);
                item = Convert.ToString(dr["name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["ship_mode"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["budget_no"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["pFName"]) + " " + Convert.ToString(dr["pLName"]);
                itemList[5].Add(item);
                item = Convert.ToString(dr["oFName"]) + " " + Convert.ToString(dr["oLName"]);
                itemList[6].Add(item);
                item = Convert.ToString(dr["abbr"]);
                itemList[7].Add(item);
                item = Convert.ToString(dr["payment_type"]);
                itemList[8].Add(item);
                item = Convert.ToString(dr["fFName"]) + " " + Convert.ToString(dr["fLName"]);
                itemList[9].Add(item);
                item = Convert.ToString(dr["aFName"]) + " " + Convert.ToString(dr["aLName"]);
                itemList[10].Add(item);
                item = Convert.ToString(dr["total_amt"]);
                itemList[11].Add(item);
                item = Convert.ToString(dr["total_qty"]);
                itemList[12].Add(item);
                item = Convert.ToString(dr["total_qty_recv"]);
                itemList[13].Add(item);
                item = Convert.ToString(dr["is_delivered"]);
                itemList[14].Add(item);
                item = Convert.ToString(dr["comment"]);
                itemList[15].Add(item);
            }

            dr.Close();

            return itemList;
        }
        public List<string>[] selInvReportUser(int owner_id)
        {
            List<string>[] itemList = new List<string>[11];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            itemList[9] = new List<string>();
            itemList[10] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?_owner_id", owner_id);
            cmd.Parameters["?_owner_id"].Direction = ParameterDirection.Input;
            cmd.CommandText = "spSelInvReportUser";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["inv_id"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["order_id"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["abbr"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["room_no"]);
                itemList[5].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selInvReportBuilding(int building_id)
        {
            List<string>[] itemList = new List<string>[11];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            itemList[9] = new List<string>();
            itemList[10] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?_building_id", building_id);
            cmd.Parameters["?_building_id"].Direction = ParameterDirection.Input;
            cmd.CommandText = "spSelInvReportBuilding";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["inv_id"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["order_id"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["abbr"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["room_no"]);
                itemList[5].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selInvReportCategory(int category_id)
        {
            List<string>[] itemList = new List<string>[11];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            itemList[9] = new List<string>();
            itemList[10] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?_category_id", category_id);
            cmd.Parameters["?_category_id"].Direction = ParameterDirection.Input;
            cmd.CommandText = "spSelInvReportCategory";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["inv_id"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["order_id"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["abbr"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["room_no"]);
                itemList[5].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selInvReportOrder(int order_id)
        {
            List<string>[] itemList = new List<string>[11];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            itemList[9] = new List<string>();
            itemList[10] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?_order_id", order_id);
            cmd.Parameters["?_order_id"].Direction = ParameterDirection.Input;
            cmd.CommandText = "spSelInvReportOrder";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["inv_id"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["order_id"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["abbr"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["room_no"]);
                itemList[5].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selInvReportInvID(int inv_id)
        {
            List<string>[] itemList = new List<string>[11];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            itemList[8] = new List<string>();
            itemList[9] = new List<string>();
            itemList[10] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("?_inv_id", inv_id);
            cmd.Parameters["?_inv_id"].Direction = ParameterDirection.Input;
            cmd.CommandText = "spSelInvReportInvID";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["inv_id"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["order_id"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["name"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["first_name"]) + " " + Convert.ToString(dr["last_name"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["abbr"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["room_no"]);
                itemList[5].Add(item);
            }

            dr.Close();

            return itemList;
        }

        public List<string>[] selAllLogin()
        {
            List<string>[] itemList = new List<string>[8];
            itemList[0] = new List<string>();
            itemList[1] = new List<string>();
            itemList[2] = new List<string>();
            itemList[3] = new List<string>();
            itemList[4] = new List<string>();
            itemList[5] = new List<string>();
            itemList[6] = new List<string>();
            itemList[7] = new List<string>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSelAllLogin";
            cmd.Connection = connection;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string item = Convert.ToString(dr["login_id"]);
                itemList[0].Add(item);
                item = Convert.ToString(dr["login_name"]);
                itemList[1].Add(item);
                item = Convert.ToString(dr["is_admin"]);
                itemList[2].Add(item);
                item = Convert.ToString(dr["first_name"]);
                itemList[3].Add(item);
                item = Convert.ToString(dr["last_name"]);
                itemList[4].Add(item);
                item = Convert.ToString(dr["phone"]);
                itemList[5].Add(item);
                item = Convert.ToString(dr["email"]);
                itemList[6].Add(item);
                item = Convert.ToString(dr["creation_date"]);
                itemList[7].Add(item);
            }

            dr.Close();

            return itemList;
        }

        // insertBudget
        //  - Invokes a store proc to insert a new Budget record to the database
        //  - Returns budget no if no errors; otherwise -1; errMsg will have more details

        public int newBudget(int bNum, string desc, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spNewBudget";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_budget_no", bNum);
                cmd.Parameters.AddWithValue("?_description", desc);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            if ((rc > 0) && (rc != bNum))
            {
                errMsg = "Unexpected Error: rc = " + rc.ToString() + " does not match bNum = " + bNum.ToString() + "Contact DBA/SysAdmin";
                return -1;
            }
             
            return rc;
        }

        public int newBuilding(string abbr, string name, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spNewBuilding";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_abbr", abbr);
                cmd.Parameters.AddWithValue("?_name", name);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            return rc;
        }

        public int newCategory(string name, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spNewCategory";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_name", name);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            return rc;
        }

        public int newDept(string abbr, string name, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spNewDept";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_abbr", abbr);
                cmd.Parameters.AddWithValue("?_name", name);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            return rc;
        }

        public int newUser(int userID, string firstname, string lastname, string phone, string email, int bldgID, string roomNo
            , bool p, bool o, bool ea, bool f, bool student, bool staff, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spNewUser";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_user_id", userID);
                cmd.Parameters.AddWithValue("?_email", email);
                cmd.Parameters.AddWithValue("?_first_name", firstname);
                cmd.Parameters.AddWithValue("?_last_name", lastname);
                cmd.Parameters.AddWithValue("?_phone", phone);
                cmd.Parameters.AddWithValue("?_bldg_id", bldgID);
                cmd.Parameters.AddWithValue("?_room_no", roomNo);
                cmd.Parameters.AddWithValue("?_is_owner", o);
                cmd.Parameters.AddWithValue("?_is_purchaser", p);
                cmd.Parameters.AddWithValue("?_is_exp_authority", ea);
                cmd.Parameters.AddWithValue("?_is_faculty", f);
                cmd.Parameters.AddWithValue("?_is_staff", staff);
                cmd.Parameters.AddWithValue("?_is_student", student);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            if ((rc > 0) && (rc != userID))
            {
                errMsg = "Unexpected Error: rc = " + rc.ToString() + " does not match user_id = " + userID.ToString() + "Contact DBA/SysAdmin";
                return -1;
            }

            return rc;
        }

        public int newVendor(string name, string address, string phone, string fax, string website, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spNewVendor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_name", name);
                cmd.Parameters.AddWithValue("?_address", address);
                cmd.Parameters.AddWithValue("?_phone", phone);
                cmd.Parameters.AddWithValue("?_fax", fax);
                cmd.Parameters.AddWithValue("?_website", website);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            return rc;
        }

        public int editBudget(int bNum, bool option, string desc, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spEditBudget";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_budget_no", bNum);
                cmd.Parameters.AddWithValue("?_isDelete", option);
                cmd.Parameters.AddWithValue("?_description", desc);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            return rc;
        }

        public int editCategory(string name, bool option, string newname, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spEditCategory";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_name", name);
                cmd.Parameters.AddWithValue("?_isDelete", option);
                cmd.Parameters.AddWithValue("?_newname", newname);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            return rc;
        }

        public int editBuilding(string abbr, bool option, string newabbr, string newname, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spEditBuilding";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_abbr", abbr);
                cmd.Parameters.AddWithValue("?_isDelete", option);
                cmd.Parameters.AddWithValue("?_newabbr", newabbr);
                cmd.Parameters.AddWithValue("?_newname", newname);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            return rc;
        }

        public int editDept(string abbr, bool option, string newabbr, string newname, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spEditDept";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_abbr", abbr);
                cmd.Parameters.AddWithValue("?_isDelete", option);
                cmd.Parameters.AddWithValue("?_newabbr", newabbr);
                cmd.Parameters.AddWithValue("?_newname", newname);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            return rc;
        }

        public int editVendor(string name, bool option, string newname, string newaddress, string newphone, string newfax, string newwebsite, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spEditVendor";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_name", name);
                cmd.Parameters.AddWithValue("?_isDelete", option);
                cmd.Parameters.AddWithValue("?_newname", newname);
                cmd.Parameters.AddWithValue("?_newaddress", newaddress);
                cmd.Parameters.AddWithValue("?_newphone", newphone);
                cmd.Parameters.AddWithValue("?_newfax", newfax);
                cmd.Parameters.AddWithValue("?_newwebsite", newwebsite);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            return rc;
        }

        public int editUser(int userID, bool option, string firstname, string lastname, string phone, string email
    , int bid, string room, bool p, bool o, bool ea, bool f, bool student, bool staff, ref string errMsg)
        {
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spEditUser";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_user_id", userID);
                cmd.Parameters.AddWithValue("?_isDelete", option);
                cmd.Parameters.AddWithValue("?_first_name", firstname);
                cmd.Parameters.AddWithValue("?_last_name", lastname);
                cmd.Parameters.AddWithValue("?_phone", phone);
                cmd.Parameters.AddWithValue("?_email", email);
                cmd.Parameters.AddWithValue("?_bldg_id", bid);
                cmd.Parameters.AddWithValue("?_room_no", room);
                cmd.Parameters.AddWithValue("?_is_owner", o);
                cmd.Parameters.AddWithValue("?_is_purchaser", p);
                cmd.Parameters.AddWithValue("?_is_exp_authority", ea);
                cmd.Parameters.AddWithValue("?_is_faculty", f);
                cmd.Parameters.AddWithValue("?_is_staff", staff);
                cmd.Parameters.AddWithValue("?_is_student", student);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                rc = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }
            catch (Exception ex)
            {
                errMsg = "Error - SafeMySql: Exception: " + ex.StackTrace;
                return -1;
            }

            if ((rc > 0) && (rc != 1))
            {
                errMsg = "Unexpected Error: rc = " + rc.ToString() + " more than one row updated user_id = " + userID.ToString() + "Contact DBA/SysAdmin";
                return -1;
            }

            return rc;
        }

        public int editInventory(int invID, bool option, string desc,  int catgID, int bldgID, string roomNo, int ownerID, int vendorID,
                                    int deptID, string purchaseDate, double purchasePrice, string manf, string transNo,
                                    string serialNo, string modelNo, string comment, ref string errMsg)
        {

            errMsg = "";

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spEditInventory";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("?_inv_id", invID);
                cmd.Parameters.AddWithValue("?_isDelete", option);
                cmd.Parameters.AddWithValue("?_description", desc);
                cmd.Parameters.AddWithValue("?_catg_id", catgID);
                cmd.Parameters.AddWithValue("?_bldg_id", bldgID);
                cmd.Parameters.AddWithValue("?_room_no", roomNo);
                cmd.Parameters.AddWithValue("?_owner_id", ownerID);
                cmd.Parameters.AddWithValue("?_vendor_id", vendorID);
                cmd.Parameters.AddWithValue("?_dept_id", deptID);
                cmd.Parameters.AddWithValue("?_purchase_date", purchaseDate);
                cmd.Parameters.AddWithValue("?_purchase_price", purchasePrice);
                cmd.Parameters.AddWithValue("?_manf", manf);
                cmd.Parameters.AddWithValue("?_trans_no", transNo);
                cmd.Parameters.AddWithValue("?_serial_no", serialNo);
                cmd.Parameters.AddWithValue("?_model_no", modelNo);
                cmd.Parameters.AddWithValue("?_comment", comment);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;


                cmd.ExecuteNonQuery();

                invID = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }

            return invID;
        }

        public int editPurchaseRequest(int orderID, bool option, int ownerID, int purchaserID, 
                                    int facID, int authID, int vendorID,
                                    int deptID, int budgetNo, string comment,
                                    int shipMode, int paymentType, ref string errMsg)
        {

            errMsg = "";

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spEditPurchaseRequest";
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("?_order_id", orderID);
                cmd.Parameters.AddWithValue("?_isDelete", option);
                cmd.Parameters.AddWithValue("?_owner_id", ownerID);
                cmd.Parameters.AddWithValue("?_purchaser_id", purchaserID);
                cmd.Parameters.AddWithValue("?_sign_faculty_id", facID);
                cmd.Parameters.AddWithValue("?_sign_auth_id", authID);
                cmd.Parameters.AddWithValue("?_vendor_id", vendorID);
                cmd.Parameters.AddWithValue("?_dept_id", deptID);
                cmd.Parameters.AddWithValue("?_budget_no", budgetNo);
                cmd.Parameters.AddWithValue("?_comment", comment);
                cmd.Parameters.AddWithValue("?_ship_mode", shipMode);
                cmd.Parameters.AddWithValue("?_payment_type", paymentType);

                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;

                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;


                cmd.ExecuteNonQuery();

                orderID = (int)cmd.Parameters["?_rc"].Value;
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                errMsg = "Error " + ex.Number + " has occurred: " + ex.Message;
                return -1;
            }

            return orderID;
        }

        public int login(string loginName, string passwrd, ref string errMsg)
        {
            string err = null;
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spLogin";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_login_name", loginName);
                cmd.Parameters["?_login_name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_passwrd", passwrd);
                cmd.Parameters["?_passwrd"].Direction = ParameterDirection.Input;
                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                if (cmd.Parameters["?_rc"].Value.GetType() == typeof(System.DBNull))
                {
                    rc = -1;
                    errMsg = "Login Failed!";
                    return rc;
                }

                rc = (int) (cmd.Parameters["?_rc"].Value);
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                err = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            return rc;
        }

        public int newLoginAcct(string loginName, string passwrd, string repeatPasswrd, 
            bool isAdmin, string firstName, string lastName, string email, string phone, ref string errMsg)
        {
            string err = null;
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spNewLoginAcct";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_login_name", loginName);
                cmd.Parameters["?_login_name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_passwrd", passwrd);
                cmd.Parameters["?_passwrd"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_repeat_passwrd", repeatPasswrd);
                cmd.Parameters["?_repeat_passwrd"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_is_admin", isAdmin);
                cmd.Parameters["?_is_admin"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_first_name", firstName);
                cmd.Parameters["?_first_name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_last_name", lastName);
                cmd.Parameters["?_last_name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_email", email);
                cmd.Parameters["?_email"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_phone", phone);
                cmd.Parameters["?_phone"].Direction = ParameterDirection.Input;
                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                if (cmd.Parameters["?_rc"].Value.GetType() == typeof(System.DBNull))
                {
                    rc = -1;
                    errMsg = "Login Failed!";
                    return rc;
                }

                rc = (int)(cmd.Parameters["?_rc"].Value);
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                err = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            return rc;
        }

        public int changePW(string loginName, string oldPasswrd, string newPasswrd, string repeatPasswrd, ref string errMsg)
        {
            string err = null;
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spChangePW";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_login_name", loginName);
                cmd.Parameters["?_login_name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_old_passwrd", oldPasswrd);
                cmd.Parameters["?_old_passwrd"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_new_passwrd", newPasswrd);
                cmd.Parameters["?_new_passwrd"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_repeat_passwrd", repeatPasswrd);
                cmd.Parameters["?_repeat_passwrd"].Direction = ParameterDirection.Input;
                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                if (cmd.Parameters["?_rc"].Value.GetType() == typeof(System.DBNull))
                {
                    rc = -1;
                    errMsg = "Login Failed!";
                    return rc;
                }

                rc = (int)(cmd.Parameters["?_rc"].Value);
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                err = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            return rc;
        }

        public int editLogin(string loginName, string firstName, string lastName, string passwrd,
             string email, string phone, bool isAdmin, bool isDelete, ref string errMsg)
        {
            string err = null;
            errMsg = "Unknown Error";
            int rc = -1;

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "spEditLogin";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("?_login_name", loginName);
                cmd.Parameters["?_login_name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_first_name", firstName);
                cmd.Parameters["?_first_name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_last_name", lastName);
                cmd.Parameters["?_last_name"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_passwrd", passwrd);
                cmd.Parameters["?_passwrd"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_email", email);
                cmd.Parameters["?_email"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_phone", phone);
                cmd.Parameters["?_phone"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_is_admin", isAdmin);
                cmd.Parameters["?_is_admin"].Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("?_is_delete", isDelete);
                cmd.Parameters["?_is_delete"].Direction = ParameterDirection.Input;
                cmd.Parameters.Add(new MySqlParameter("?_rc", MySqlDbType.Int32));
                cmd.Parameters["?_rc"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add(new MySqlParameter("?_errmsg", MySqlDbType.VarChar));
                cmd.Parameters["?_errmsg"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                if (cmd.Parameters["?_rc"].Value.GetType() == typeof(System.DBNull))
                {
                    rc = -1;
                    errMsg = "Edit Failed!";
                    return rc;
                }

                rc = (int)(cmd.Parameters["?_rc"].Value);
                errMsg = (string)cmd.Parameters["?_errmsg"].Value;
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                err = "Error " + ex.Number + " has occurred: " + ex.Message;
            }

            return rc;
        }
   }
}

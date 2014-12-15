using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewITS
{
    class User
    {
        private int id;
        private string firstName;
        private string lastName;
        private string phone;
        private string email;
        private bool isP;
        private bool isO;
        private bool isEA;
        private bool isF;
        private bool isStu;
        private bool isSta;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public bool IsP
        {
            get { return isP; }
            set { isP = value; }
        }

        public bool IsO
        {
            get { return isO; }
            set { isO = value; }
        }

        public bool IsEA
        {
            get { return isEA; }
            set { isEA = value; }
        }

        public bool IsF
        {
            get { return isF; }
            set { isF = value; }
        }

        public bool IsStu
        {
            get { return isStu; }
            set { isStu = value; }
        }

        public bool IsSta
        {
            get { return isSta; }
            set { isSta = value; }
        }
    }

    class UserStore
    {
        private static UserStore us = null;

        public static UserStore getInstance
        {
            get
            {
                if (us == null)
                {
                    us = new UserStore();
                }
                return us;
            }
        }

        private List<User> uList;
        private int nextUser;

        private UserStore()
        {
            uList = new List<User>();
            nextUser = 1;
        }

        public int NewUser(string f, string l, string p, string e, 
            bool purchase, bool owner, bool ea, bool faculty, bool student, bool staff)
        {
            User u;

            u = new User(); // Create new user

            // Assign user values

            u.ID = nextUser;
            u.FirstName = f;
            u.LastName = l;
            u.Phone = p;
            u.Email = e;
            u.IsP = purchase;
            u.IsO = owner;
            u.IsEA = ea;
            u.IsF = faculty;
            u.IsStu = student;
            u.IsSta = staff;

            // Add to user list

            uList.Add(u);

            nextUser++;

            return u.ID;
        }

        public List<string> OrderUser()
        {
            List<string> ol = new List<string>();
            foreach (User u in uList)
            {
                ol.Add(u.FirstName + " " + u.LastName);
            }

            return ol;
        }

        public List<string> OrderUserOwner()
        {
            List<string> ol = new List<string>();
            foreach (User u in uList)
            {
                if (u.IsO == true)
                    ol.Add(u.FirstName + " " + u.LastName);
            }

            return ol;
        }

        public List<string> OrderUserPurchaser()
        {
            List<string> ol = new List<string>();
            foreach (User u in uList)
            {
                if (u.IsP == true)
                    ol.Add(u.FirstName + " " + u.LastName);
            }

            return ol;
        }

        public List<string> OrderUserExpenditure()
        {
            List<string> ol = new List<string>();
            foreach (User u in uList)
            {
                if (u.IsEA == true)
                    ol.Add(u.FirstName + " " + u.LastName);
            }

            return ol;
        }

        public List<string> OrderUserFaculty()
        {
            List<string> ol = new List<string>();
            foreach (User u in uList)
            {
                if (u.IsF == true)
                    ol.Add(u.FirstName + " " + u.LastName);
            }

            return ol;
        }

        public User OrderName(string fn, string ln)
        {
            foreach (User u in uList)
            {
                if ((u.FirstName == fn) && (u.LastName == ln))
                    return u;
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewITS
{
    class Department
    {
        private int id;
        private string sname;
        private string lname;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string SName
        {
            get { return sname; }
            set { sname = value; }
        }

        public string LName
        {
            get { return lname; }
            set { lname = value; }
        }
    }

    class DepartmentStore
    {
        private static DepartmentStore dpts = null;

        public static DepartmentStore getInstance
        {
            get
            {
                if (dpts == null)
                {
                    dpts = new DepartmentStore();
                }
                return dpts;
            }
        }

        private List<Department> dList;
        private int nextDept;

        private DepartmentStore()
        {
            dList = new List<Department>();
            nextDept = 1;
        }

        public int NewDepartment(string shortname, string longname)
        {
            Department d;

            d = new Department(); // Create new building

            // Assign building values

            d.ID = nextDept;
            d.SName = shortname;
            d.LName = longname;

            // Add to building list

            dList.Add(d); 

            nextDept++;

            return d.ID;
        }

        public List<string> OrderDept()
        {
            List<string> ol = new List<string>();
            foreach (Department d in dList)
                ol.Add(d.SName);

            return ol;
        }
    }
}

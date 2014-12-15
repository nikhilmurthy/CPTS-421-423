using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewITS
{
    public class Building
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

    public class BuildingStore
    {
        private static BuildingStore bs = null;

        public static BuildingStore getInstance
        {
            get
            {
                if (bs == null)
                {
                    bs = new BuildingStore();
                }
                return bs;
            }
        }

        private List<Building> bList;
        private int nextBuild;

        private BuildingStore()
        {
            bList = new List<Building>();
            nextBuild = 1;
        }

        public int NewBuilding(string shortname, string longname)
        {
            Building b;

            b = new Building(); // Create new building

            // Assign building values

            b.ID = nextBuild;
            b.SName = shortname;
            b.LName = longname;

            // Add to building list

            bList.Add(b); 

            nextBuild++;

            return b.ID;
        }

        public List<string> OrderBuild()
        {
            List<string> ol = new List<string>();
            foreach (Building b in bList)
                ol.Add(b.SName);

            return ol;
        }
    }
}

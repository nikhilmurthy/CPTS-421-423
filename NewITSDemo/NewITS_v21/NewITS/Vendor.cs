using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewITS
{
    class Vendor
    {
        private int id;
        private string name;
        private string address;
        private string phone;
        private string fax;
        private string website;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        public string Website
        {
            get { return website; }
            set { website = value; }
        }
    }

    class VendorStore
    {
        private static VendorStore vs = null;

        public static VendorStore getInstance
        {
            get
            {
                if (vs == null)
                {
                    vs = new VendorStore();
                }
                return vs;
            }
        }

        private List<Vendor> vList;
        private int nextVendor;

        private VendorStore()
        {
            vList = new List<Vendor>();
            nextVendor = 1;
        }

        public int NewVendor(string n, string a, string p, string f, string w)
        {
            Vendor v;

            v = new Vendor(); // Create new building

            // Assign building values

            v.ID = nextVendor;
            v.Name = n;
            v.Address = a;
            v.Phone = p;
            v.Fax = f;
            v.Website = w;

            // Add to building list

            vList.Add(v);

            nextVendor++;

            return v.ID;
        }

        public List<string> OrderVendor()
        {
            List<string> ol = new List<string>();
            foreach (Vendor v in vList)
                ol.Add(v.Name);

            return ol;
        }

        public Vendor OrderName(string s)
        {
            foreach (Vendor v in vList)
            {
                if (v.Name == s)
                    return v;
            }

            return null;
        }
    }
}

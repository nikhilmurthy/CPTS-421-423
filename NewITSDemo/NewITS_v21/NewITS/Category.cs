using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewITS
{
    class Category
    {
        private int id;
        private string name;

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
    }

    class CategoryStore
    {
        private static CategoryStore cs = null;

        public static CategoryStore getInstance
        {
            get
            {
                if (cs == null)
                {
                    cs = new CategoryStore();
                }
                return cs;
            }
        }

        private List<Category> cList;
        private int nextCat;

        private CategoryStore()
        {
            cList = new List<Category>();
            nextCat = 1;
        }

        public int NewCategory(string name)
        {
            Category c;

            c = new Category(); // Create new building

            // Assign category values

            c.Name = name;

            // Add to category list

            cList.Add(c);

            nextCat++;

            return c.ID;
        }

        public List<string> OrderCat()
        {
            List<string> ol = new List<string>();
            foreach (Category c in cList)
                ol.Add(c.Name);

            return ol;
        }
    }
}

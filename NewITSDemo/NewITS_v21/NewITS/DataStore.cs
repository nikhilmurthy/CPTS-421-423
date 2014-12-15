using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewITS
{
    public class LineItem
    {
        private int lNum;
        private int catalogNum;
        private string desc;
        private int qty;
        private int unit;
        private double price;

        public int LNum
        {
            get { return lNum; }
            set { lNum = value; }
        }

        public int CatalogNum
        {
            get { return catalogNum; }
            set { catalogNum = value; }
        }
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public int Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
    }

    class Order
    {
        private int orderNo;
        private string date;
        private string desc;
        private List<LineItem> liList;

        public int OrderNo // new
        {
            get { return orderNo; }
            set { orderNo = value; }
        }


        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public List<LineItem> LiList
        {
            get { return liList; }
            set { liList = value; }
        }
    }
    public class DataStore
    {
        private static DataStore ds = null;
   //     private DataStore() { }

         public static DataStore getInstance
           {
              get 
              {
                 if (ds == null)
                 {
                    ds = new DataStore();
                 }
                 return ds;
              }
           }


            private List<Order> oList;
            private int nextOrder;


            private DataStore()
            {
                oList = new List<Order>();
                nextOrder = 1;
            }
 
            public int NewOrder(string desc, string date)
            {

                Order o;

                o = new Order(); // Create new order object

                // Assign order data members

                o.OrderNo = nextOrder;

                o.Desc = desc;
                o.Date = date;
                o.LiList = new List<LineItem>();

                // Add to order list

                oList.Add(o);

                nextOrder++;
                return o.OrderNo;
            }

            public int AddOrderLine(int ono, int lineNum, int catNum, string desc, int qty, int unit, double price)
            {
                LineItem li;
                Order po = null;

                foreach (Order o in oList)
                {
                    if (o.OrderNo == ono)
                    {
                        po = o;
                        break;
                    }

                }
                if (po == null)
                {
                    Console.WriteLine (" invalid order #");
                    return -1;
                };
                
                li = new LineItem(); // Create new line item

                // Set up line item attributes

                li.CatalogNum = catNum;
                li.Desc = desc;
                li.Qty = qty;
                li.Unit = unit;
                li.Price = price;

                po.LiList.Add(li);
                return 0;
               
            }

           public List<string> OrderList()
            {
               List<string> ol = new List<string>();
              foreach (Order o in oList)
                  ol.Add (o.OrderNo.ToString());
              return ol;

            }

            public string OrderDate(int ono)
            {
                string rs = "";

                foreach (Order o in oList)
                {
                    if (o.OrderNo == ono)
                    {
                        rs =  o.Date;
                        break;
                    }

                }
                return rs;
            }
            public List<LineItem> OrderLine(int ono)
            {
                List<LineItem> ll = new List<LineItem>();

                foreach (Order o in oList)
                {
                    if (o.OrderNo == ono)
                    {
                        ll = o.LiList;
                        break;
                    }

                }
                return ll;
            } 
    }
}

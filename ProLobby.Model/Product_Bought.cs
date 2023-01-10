using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Model
{
    public class Product_Bought
    {
        private int _ID;
        public int ID { get { return _ID; } set { if (_ID == 0) _ID = value; } }
        public string Name;
        public int Quantity;
        public int Social_Activist_ID;
        public int ProductID;
        public int TweetID;
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Model
{
    public class Product
    {
        private int _ID;
        public int ID { get { return _ID; } set { if (_ID == 0) _ID = value; } }
        public string Name { get; set; }
        public int Quantity { get; set; }
        private int _Social_Activist_ID;
        public int Social_Activist_ID { get { return _Social_Activist_ID; } set { if (_Social_Activist_ID == 0) _Social_Activist_ID = value; } }
        public decimal Price { get; set; }
        public string Business_Email { get; set; }
        public string CampaignHashtag { get; set; }
    }
}

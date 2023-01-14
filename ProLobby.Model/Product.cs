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
        public string Activist_Email { get; set; }
        public decimal Price { get; set; }
        public int TweetID { get; set; }
        public bool isSent { get; set; }
        public string Business_Email { get; set; }
        public string CampaignHashtag { get; set; 
        }
    }
}

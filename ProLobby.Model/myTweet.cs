using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Model
{
    public class myTweet
    {
        private int _ID;
        public int ID { get { return _ID; } set { if (_ID == 0) _ID = value; } }
        public string Text { get; set; }
        public string Product_Name { get; set; }
        public int Quantity { get; set; }
        public string Social_Activist_Email { get; set; }
        public string Campaign_Hashtag { get; set; }
        public int TimesTweeted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Model
{
    public class Campaign
    {
        private int _ID;
        public int ID { get { return _ID; } set { if (_ID == 0) _ID = value; } }
        public string Link { get; set; }
        public string Hashtag { get; set; }
        public string Email { get; set; }
    }
}

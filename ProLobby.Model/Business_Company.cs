using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Model
{
    public class Business_Company
    {
        private int _ID;
        public int ID { get { return _ID; } set { if (_ID == 0) _ID = value; } }
        public string Business_Company_Name { get; set; }
        public string Email { get; set; }
        public string Link_Business_Website { get; set; }
    }
}

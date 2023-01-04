using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Model
{
    public class Social_Activist
    {
        private int _ID;
        public int ID { get { return _ID; } set { if (_ID == 0) _ID = value; } }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Money_Earned { get; set; }
    }
}

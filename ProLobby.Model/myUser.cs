using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Model
{
    public class myUser
    {
        private int _ID;
        public int ID { get { return _ID; } set { if (_ID == 0) _ID = value; } }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}

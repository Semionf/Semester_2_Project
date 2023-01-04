using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Model
{
    public class Non_Profit
    {
        private int _ID;
        public int ID { get { return _ID; } set { if (_ID == 0) _ID = value; } }
        public string Non_Profit_Name { get; set; }
        public string Email { get; set; }
        public string Link_Non_Profit_Website { get; set; }
    }
}

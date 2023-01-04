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
        public string CampaignName { get; set; }
        public string Email { get; set; }
        public string Link_Organization_Website { get; set; }
        public string Link_Campaign_URL { get; set; }
        public string HashTag { get; set; }
        public int Non_Profit_ID { get; set; }
    }
}

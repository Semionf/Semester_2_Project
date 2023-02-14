using PromoteIt.DAL;
using PromoteIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoteIt.Entities
{
    public class Campaigns: BaseEntity
    {
        private static Data.Sql.Campaigns dataSql = new Data.Sql.Campaigns();

        public Campaigns(Logger log) : base(log)
        {
        }

        public Dictionary<int,object> LoadCampaigns(string Email)
        {
            return dataSql.LoadCampaigns(Email);
        }
        public Dictionary<int, object> LoadCampaigns()
        {
            return dataSql.LoadCampaigns();
        }
        public void addCampaign(Campaign campaign)
        {
            
            dataSql.addCampaign(campaign);
        }

    }
}

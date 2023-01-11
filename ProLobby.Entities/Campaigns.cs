using PromoteIt.DAL;
using PromoteIt.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Entities
{
    public class Campaigns
    {
        private static Data.Sql.Campaigns dataSql = new Data.Sql.Campaigns();
        public object LoadCampaigns(string Email)
        {
            return dataSql.LoadCampaigns(Email);
        }
        public object LoadCampaigns()
        {
            return dataSql.LoadCampaigns();
        }
        public void addCampaign(Campaign campaign)
        {
            
            dataSql.addCampaign(campaign);
        }

    }
}

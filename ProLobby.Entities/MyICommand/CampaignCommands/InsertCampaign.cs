using System;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using Utilities;
using PromoteIt.Model;
using PromoteIt.Entities.ICommand;

namespace PromoteIt.Entities.ICommand.CampaignCommands
{
    public class InsertCampaign : CommandManager, ICommand
    {
        public InsertCampaign(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param)
        {
            if (param[1] != null)
            {
                Model.Campaign campaign = new Model.Campaign();
                campaign = JsonConvert.DeserializeObject<Model.Campaign>(param[0].ToString());
                MainManager.Instance.campaigns.addCampaign(campaign);
                MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = $"Campaign {campaign.Hashtag} has been added!" });
                return JsonConvert.SerializeObject("Campaign inserted");
            }
            else
            {
                MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Error", Message = $"Campaign hasn't been added" });
                return JsonConvert.SerializeObject("Failed to insert Campaign");
            }
        }
    }
}

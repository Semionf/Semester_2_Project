using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoteIt.Entities.ICommand.CampaignCommands
{
    public class LoadAll : CommandManager, ICommand
    {
        public LoadAll(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param)
        {
            MainManager.Instance.InitCampaign();
            MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "Getting all campaigns!" });
            return JsonConvert.SerializeObject(MainManager.Instance.campaignsList);
        }
    }
}

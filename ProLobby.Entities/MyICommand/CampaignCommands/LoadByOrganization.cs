using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoteIt.Entities.ICommand.CampaignCommands
{
    public class LoadByOrganization : CommandManager, ICommand
    {
        public LoadByOrganization(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param)
        {
            MainManager.Instance.InitCampaign(param[1].ToString());
            MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = $"Getting campaigns of organization: {param[1]}" });
            throw new NotImplementedException();
        }
    }
}

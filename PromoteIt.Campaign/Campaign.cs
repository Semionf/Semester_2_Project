using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using PromoteIt.Model;
using static System.Collections.Specialized.BitVector32;
using PromoteIt.Entities.ICommand;
using PromoteIt.Entities;

namespace PromoteIt.Campaign
{
    public static class Campaign
    {
        [FunctionName("Campaign")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Campaign/{action}/{Email?}")] HttpRequest req, string action, string Email,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            ICommand command = CommandManager.Instance.CampaignCommand[action];
            try
            {
                string requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                switch (action)
                {
                    case "GET":
                        
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaignsList));

                    case "POST":

                        break;
                    case "LOAD":
                        MainManager.Instance.InitCampaign();
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "Getting all campaigns!"});
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaignsList));
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.AddToLog(new LogItem { exception = ex });
            }
            return null;
        }
    }
}

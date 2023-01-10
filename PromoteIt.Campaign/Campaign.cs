using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PromoteIt.Entities;
using PromoteIt.Model;
using static System.Collections.Specialized.BitVector32;

namespace PromoteIt.Campaign
{
    public static class Campaign
    {
        [FunctionName("Campaign")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Campaign/{action}")] HttpRequest req, string action,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            switch (action)
            {
                case "get":
                    MainManager.Instance.InitCampaign();
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.campaignsList));

                case "post":
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    PromoteIt.Model.Campaign campaign = new PromoteIt.Model.Campaign();
                    campaign = System.Text.Json.JsonSerializer.Deserialize<PromoteIt.Model.Campaign>(requestBody);
                    MainManager.Instance.campaigns.addCampaign(campaign);
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}

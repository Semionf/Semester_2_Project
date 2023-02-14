using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using Tweetinvi;
using PromoteIt.Entities;
using MyUtilities;

namespace PromoteIt.Server
{
    public static class Roles
    {
        [FunctionName("Roles")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Roles/{userID}")] HttpRequest req, string userID,
            ILogger log)
        {
            try
            {
                string url = $"https://dev-8wgfmbfrqoc31nqm.us.auth0.com/api/v2/users/{userID}/roles";
                string requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                var client = new RestClient(url);
                var request = new RestRequest("", Method.Get);
                Console.WriteLine(userID);
                request.AddHeader("authorization", $"Bearer  {Environment.GetEnvironmentVariable("BearerAuth0")}");
                var response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var json = JArray.Parse(response.Content);
                    MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "A user logged in!" });
                    return new OkObjectResult(json);
                }
                else
                {
                    MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "No such user in the DB!" });
                    return new NotFoundResult();
                }
            }
            catch (Exception ex)
            {
                MainManager.Instance.Logger.AddToLog(new LogItem { exception = ex });
                return null;
            }
        }
    }
}

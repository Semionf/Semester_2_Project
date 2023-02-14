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
using MyUtilities;
namespace PromoteIt.Server
{
    public static class User
    {
        [FunctionName("User")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "User/{action}/{Email?}")] HttpRequest req, string action, string Email,
            ILogger log)
        {
            try
            {
                string requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                switch (action)
                {
                    case "GET":
                        MainManager.Instance.InitUsers();
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "Users list loaded!" });
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.usersList));
                        break;
                    case "POST":
                        Model.myUser user = new Model.myUser();
                        user = System.Text.Json.JsonSerializer.Deserialize<Model.myUser>(requestGetBody);
                        MainManager.Instance.users.addUser(user);
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "Added new user to the system!" });
                        break;
                    case "CHECK":
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "Checking if user exists in the DB" });
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.users.checkUser(Email)));
                        break;
                    case "BALANCE":
                        MainManager.Instance.getBalance(Email);
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "Getting balance of a user!" });
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.UserBalance));
                        break;
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

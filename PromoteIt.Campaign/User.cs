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

namespace PromoteIt.Server
{
    public static class User
    {
        [FunctionName("User")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "User/{action}/{Email?}")] HttpRequest req, string action, string Email, 
            ILogger log)
        {
            switch (action)
            {
                case "GET":
                    string requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitUsers();
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance));

                case "POST":
                    string requestPostBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.User user = new Model.User();
                    user = System.Text.Json.JsonSerializer.Deserialize<Model.User>(requestPostBody);
                    MainManager.Instance.users.addUser(user);
                    break;
                case "CHECK":
                    string requestCheckBody = await new StreamReader(req.Body).ReadToEndAsync();
                    
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.users.checkUser(Email)));
                case "BALANCE":
                    string requestGetBody2 = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.getBalance(Email);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.UserBalance));

                default:
                    break;
            }
            return null;
        }
    }
    
}

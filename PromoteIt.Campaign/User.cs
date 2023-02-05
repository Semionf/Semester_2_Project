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
            string requestGetBody = "";
            switch (action)
            {
                case "GET":
                    try
                    {
                        requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    }
                    catch (Exception ex)
                    {

                        break;
                    }
                    MainManager.Instance.InitUsers();
                    try
                    {
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.usersList));
                    }
                    catch (Exception ex)
                    {

                    }
                    break;
                case "POST":
                    try
                    {
                        requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    }
                    catch(Exception ex)
                    {

                    }
                    Model.myUser user = new Model.myUser();
                    try
                    {
                        user = System.Text.Json.JsonSerializer.Deserialize<Model.myUser>(requestGetBody);
                    }catch(Exception ex)
                    {
                        break;
                    }
                    MainManager.Instance.users.addUser(user);
                    break;
                case "CHECK":
                    try
                    {
                        requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    }catch(Exception ex)
                    {

                        break;
                    }
                    try
                    {
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.users.checkUser(Email)));
                    }catch(Exception ex)
                    {

                    }
                    break;
                case "BALANCE":
                    try
                    {
                        requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    }catch(Exception ex)
                    {

                    }
                    MainManager.Instance.getBalance(Email);
                    try
                    {
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.UserBalance));
                    }catch(Exception ex)
                    {

                    }
                    break;
                default:
                    break;
            }
            return null;
        }
    }

}

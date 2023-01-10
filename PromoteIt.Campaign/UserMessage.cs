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
    public static class UserMessage
    {
        [FunctionName("UserMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "UserMessage/")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            Model.UserMessage message = new Model.UserMessage();
            message = System.Text.Json.JsonSerializer.Deserialize<Model.UserMessage>(requestBody);
            MainManager.Instance.messages.addMessage(message);
            return null;
        }
    }
}

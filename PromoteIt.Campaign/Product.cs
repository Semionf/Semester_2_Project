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

namespace PromoteIt.Server
{
    public static class Product
    {
        [FunctionName("Product")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Product/{action}")] HttpRequest req, string action,
            ILogger log)
        {
            switch (action)
            {
                case "get":
                    string requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitProducts();
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance));
                    
                case "post":
                    string requestPostBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Product product = new Model.Product();
                    product = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestPostBody);
                    MainManager.Instance.products.addProduct(product);
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}

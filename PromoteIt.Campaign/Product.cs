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
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Product/{action}/{Email?}")] HttpRequest req, string action, string Email,
            ILogger log)
        {
            switch (action)
            {
                case"BUSINESS":
                    string requestGetBody3 = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitProductsBought(Email);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                case "GET":
                    string requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitProducts(Email);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                case "BOUGHT":
                    string requestGetBody4 = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitMyProductsBought(Email);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                case "BUY":
                    string requestPostBody5 = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Product product = new Model.Product();
                    product = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestPostBody5);
                    MainManager.Instance.products.addProduct(product);
                    break;

                case "POST":
                    string requestPostBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Product product2 = new Model.Product();
                    product = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestPostBody);
                    MainManager.Instance.products.addProduct(product2);
                    break;
                case "GETALL":
                    string requestGetBody2 = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitProducts();
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                default:
                    break;
            }
            return null;
        }
    }
}

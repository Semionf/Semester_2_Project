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
using Tweetinvi;

namespace PromoteIt.Server
{
    public static class Product
    {
        [FunctionName("Product")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Product/{action}/{Email?}")] HttpRequest req, string action, string Email,
            ILogger log)
        {
            string requestGetBody = "";
            switch (action)
            {
                
                case"BUSINESS":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitProductsBought(Email);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                case "GET":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitProducts(Email);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                case "SUPPLIED":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitMyProductsSupplied(Email);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                case "NOTSUPPLIED":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitMyProductsNotSupplied(Email);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                case "BUY":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Product product = new Model.Product();
                    product = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestGetBody);
                    MainManager.Instance.products.buyProduct(product);
                    break;

                case "POST":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Product product2 = new Model.Product();
                    product = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestGetBody);
                    MainManager.Instance.products.addProduct(product);
                    break;
                case "SUPPLY":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.Product product3 = new Model.Product();
                    product = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestGetBody);
                    MainManager.Instance.products.supply(product);
                    break;
                case "GETALL":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitProducts();
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                default:
                    break;
            }
            return null;
        }
    }
}

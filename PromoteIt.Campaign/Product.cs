using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PromoteIt.Entities;
using MyUtilities;
using PromoteIt.Entities.ICommand;
namespace PromoteIt.Server
{
    public static class Product
    {
        [FunctionName("Product")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Product/{action}/{Email?}")] HttpRequest req, string action, string Email,
            ILogger log)
        {
            try
            {
                string requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                switch (action)
                {

                    case "BUSINESS":
                        MainManager.Instance.InitProductsBought(Email);
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = $"Getting products bought from a business: {Email}!" });
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                    case "GET":
                        MainManager.Instance.InitProducts(Email);
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = $"Getting products donated to organization: {Email}" });
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));

                    case "SUPPLIED":
                        MainManager.Instance.InitMyProductsSupplied(Email);
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = $"List of products supplied for user: {Email}" });
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));

                    case "NOTSUPPLIED":
                        MainManager.Instance.InitMyProductsNotSupplied(Email);                       
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = $"List of products not supplied for user: {Email}"});
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));

                    case "BUY":
                        Model.Product product = new Model.Product();
                        product = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestGetBody);
                        MainManager.Instance.products.buyProduct(product);
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "A user bought a product!" });
                        break;

                    case "POST":
                        Model.Product product2 = new Model.Product();
                        product = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestGetBody);
                        MainManager.Instance.products.addProduct(product);
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "A new product has been added!" });                        
                        break;

                    case "SUPPLY":
                        Model.Product product3 = new Model.Product();
                        product = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestGetBody);
                        MainManager.Instance.products.supply(product);                       
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "Business supplied products!" });
                        break;
                    
                    case "GETALL":
                        MainManager.Instance.InitProducts();
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "Getting all products!" });
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.productsList));
                    
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

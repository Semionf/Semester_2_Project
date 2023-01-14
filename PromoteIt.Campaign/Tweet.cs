using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tweetinvi;
using static System.Collections.Specialized.BitVector32;
using PromoteIt.Entities;
using Tweetinvi.Models;

namespace PromoteIt.Server
{
    public static class Tweet
    {
        [FunctionName("Tweet")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Tweet/{action}/{Email?}")] HttpRequest req, string action, string Email,
            ILogger log)
        {
            string API_KEY = "4qg1fwOdYVkfvueFxJoxP3Ng9";
            string API_KEY_SECRET = "BgBBIsbX8FvtSqrLFOo35ScbrDGaNosKZku0237AdxPQy5TQTB";
            string ACCESS_TOKEN = "1605842591156785154-mOEo4nRvNqN7LSL3LAwPxPMuiQ9ly4";
            string ACCESS_TOKEN_SECRET = "FfJlgsesXwNTfuzc4Ow5zeCsQ49BOspmY75rZGVqh8Lb6";
            string BEARER_TOKEN = "AAAAAAAAAAAAAAAAAAAAAGXjlAEAAAAA%2BY4cDWiuTEfC572pTW%2FJQfQkPeM%3DiwGWjDCkxPhXeWHwwdDdBIDnIb0OmqwAMo2j0P05h3SOjhFnCc";
            string CLIENT_ID = "Nm8tTEVKYmFTeU5HZVdSTngzS3k6MTpjaQ";
            string CLIENT_SECRET = "TRLM__QwOtIvv88DD-tSD9mOkeo8ZPYRrUkibpWPmoB7JoVUWT";
            log.LogInformation("C# HTTP trigger function processed a request.");
            var userClient = new TwitterClient(API_KEY, API_KEY_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            var appCredentials = new ConsumerOnlyCredentials(API_KEY, API_KEY_SECRET)
            {
                BearerToken = BEARER_TOKEN // bearer token is optional in some cases
            };
            var userCredentials = new TwitterCredentials(API_KEY, API_KEY_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            var userClient2 = new TwitterClient(appCredentials);
            var authenticatedUser = await userClient.Users.GetAuthenticatedUserAsync();
            Console.WriteLine("Hello " + authenticatedUser);

            // publish a tweet
            var tweet = await userClient.Tweets.PublishTweetAsync("Hello tweetinvi world!");
            Console.WriteLine("You published the tweet : " + tweet);


            string requestGetBody = "";
            switch (action)
            {

                case "GET":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitTweets(Email);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.tweetsList));

                case "POST":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    Model.myTweet myTweet = new Model.myTweet();
                    myTweet = System.Text.Json.JsonSerializer.Deserialize<Model.myTweet>(requestGetBody);
                    MainManager.Instance.tweets.addTweet(myTweet);
                    break;
                case "GETALL":
                    requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                    MainManager.Instance.InitTweets();
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.tweetsList));
                default:
                    break;
            }
            return null;
        }
    }
}

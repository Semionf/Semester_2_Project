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
using PromoteIt.Model;

using MyUtilities;
namespace PromoteIt.Server
{
    public static class Tweet
    {
        [FunctionName("Tweet")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Tweet/{action}/{Email?}/{Text?}")] HttpRequest req, string action, string Email, string Text,
            ILogger log)
        {
            try
            {
                string API_KEY = Environment.GetEnvironmentVariable("API_KEY"); 
                string API_KEY_SECRET = Environment.GetEnvironmentVariable("API_KEY_SECRET"); 
                string ACCESS_TOKEN = Environment.GetEnvironmentVariable("ACCESS_TOKEN"); 
                string ACCESS_TOKEN_SECRET = Environment.GetEnvironmentVariable("ACCESS_TOKEN_SECRET");
                string Twitter_Bearer = Environment.GetEnvironmentVariable("Twitter_Bearer");
                log.LogInformation("C# HTTP trigger function processed a request.");
                var userClient = new TwitterClient(API_KEY, API_KEY_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);

                var authenticatedUser = await userClient.Users.GetAuthenticatedUserAsync();
                Console.WriteLine("Hello " + authenticatedUser);

                string requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
                switch (action)
                {

                    case "GET":
                        MainManager.Instance.InitTweets(Email);
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = $"Getting tweets of the user: {Email}" });
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.tweetsList));
                        break;

                    case "POST":
                        Model.myTweet myTweet = new Model.myTweet();
                        myTweet = System.Text.Json.JsonSerializer.Deserialize<Model.myTweet>(requestGetBody);
                        if (myTweet.Quantity == 0)
                        {

                            var tweet = await userClient.Tweets.PublishTweetAsync(myTweet.Text + myTweet.TimesTweeted);
                            Console.WriteLine("You published the tweet : " + tweet);

                            MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "A user retweeted!" });
                        }
                        else if (myTweet.Quantity > 1)
                        {

                            var tweet = await userClient.Tweets.PublishTweetAsync($"User Email: {myTweet.Social_Activist_Email}  Bought :{myTweet.Quantity} {myTweet.Product_Name}s, To help {myTweet.Campaign_Hashtag} Success");
                            myTweet.Text = tweet.ToString();
                            Console.WriteLine("You published the tweet : " + tweet);
                            MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "New Tweet posted!" });
                        }
                        else
                        {

                            var tweet = await userClient.Tweets.PublishTweetAsync($"User Email: {myTweet.Social_Activist_Email}  Bought: {myTweet.Quantity} {myTweet.Product_Name}, To help {myTweet.Campaign_Hashtag} Success");
                            myTweet.Text = tweet.ToString();
                            Console.WriteLine("You published the tweet : " + tweet);
                            MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "New Tweet posted!" });
                        }
                        MainManager.Instance.tweets.addTweet(myTweet);
                        break;

                    case "GETALL":
                        MainManager.Instance.InitTweets();
                        MainManager.Instance.Logger.AddToLog(new LogItem { Type = "Event", Message = "Getting all tweets from all users!" });
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.tweetsList));
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


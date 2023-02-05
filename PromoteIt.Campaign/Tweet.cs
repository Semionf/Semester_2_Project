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
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Tweet/{action}/{Email?}/{Text?}")] HttpRequest req, string action, string Email, string Text,
            ILogger log)
        {
            string API_KEY = "R3wm5TQsjayCY7cQPjdNgWYBW";
            string API_KEY_SECRET = "nl5HwSr9nGxHKHF2s6kTgotHSn3MU81XkYAiKmOyaSttFsCkPr";
            string ACCESS_TOKEN = "1605842591156785154-4gG0DkrBGhfXAFp2FmxH7SRftyu9Rb";
            string ACCESS_TOKEN_SECRET = "M5UmyJQr5OpShCkXx3UYy6tg6pSWcb4GfX93YBvRwZIMj";

            log.LogInformation("C# HTTP trigger function processed a request.");
            var userClient = new TwitterClient(API_KEY, API_KEY_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);

            var authenticatedUser = await userClient.Users.GetAuthenticatedUserAsync();
            Console.WriteLine("Hello " + authenticatedUser);

            // publish a tweet




            string requestGetBody = await new StreamReader(req.Body).ReadToEndAsync();
            switch (action)
            {

                case "GET":
                    MainManager.Instance.InitTweets(Email);
                    try
                    {
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.tweetsList));
                    }
                    catch (Exception ex)
                    {

                       
                    }
                    break ;
                  
                case "POST":
                    Model.myTweet myTweet = new Model.myTweet();
                    try
                    {
                        myTweet = System.Text.Json.JsonSerializer.Deserialize<Model.myTweet>(requestGetBody);
                    }
                    catch (Exception ex)
                    {

                        break;
                    }
                    if (myTweet.Quantity == 0)
                    {
                        try
                        {
                            var tweet = await userClient.Tweets.PublishTweetAsync(myTweet.Text + myTweet.TimesTweeted);
                            Console.WriteLine("You published the tweet : " + tweet);
                        }
                        catch (Exception ex)
                        {

                            break;
                        }
                    }
                    else if (myTweet.Quantity > 1)
                    {
                        try
                        {
                            var tweet = await userClient.Tweets.PublishTweetAsync($"User Email: {myTweet.Social_Activist_Email}  Bought :{myTweet.Quantity} {myTweet.Product_Name}s, To help {myTweet.Campaign_Hashtag} Success");
                            myTweet.Text = tweet.ToString();
                            Console.WriteLine("You published the tweet : " + tweet);
                        }
                        catch (Exception ex) 
                        {
                            break; 
                        }
                    }
                    else
                    {
                        try
                        {
                            var tweet = await userClient.Tweets.PublishTweetAsync($"User Email: {myTweet.Social_Activist_Email}  Bought: {myTweet.Quantity} {myTweet.Product_Name}, To help {myTweet.Campaign_Hashtag} Success");
                            myTweet.Text = tweet.ToString();
                            Console.WriteLine("You published the tweet : " + tweet);
                        }
                        catch (Exception)
                        {

                            break;
                        }
                    }
                    MainManager.Instance.tweets.addTweet(myTweet);
                    break;

                case "GETALL":
                    MainManager.Instance.InitTweets();
                    try
                    {
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.tweetsList));
                    }
                    catch (Exception ex)
                    {

                        break;
                    }  
                default:
                    break;
            }
            return null;
        }
    }
}

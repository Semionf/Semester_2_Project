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
            string BEARER_TOKEN = "AAAAAAAAAAAAAAAAAAAAAJHvlAEAAAAApt7Ynj0IfozvLsMY82rTZO0nkR0%3DDDYhaKJJ63r1d9EdE8oLcKVuiBfPJbXxjAylZmstgJlTkuxF7y";
            string CLIENT_ID = "UG5wN3FqMlJ5Tjg1OUJ0bENvczQ6MTpjaQ";
            string CLIENT_SECRET = "IgxVa53q2Ppm_ybRFNp1AKjBroURJairEka_71yZmsy9kvogsc";
            log.LogInformation("C# HTTP trigger function processed a request.");
            var userClient = new TwitterClient(API_KEY, API_KEY_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            var appCredentials = new ConsumerOnlyCredentials(API_KEY, API_KEY_SECRET)
            {
                BearerToken = BEARER_TOKEN // bearer token is optional in some cases
            };
            var userCredentials = new TwitterCredentials(API_KEY, API_KEY_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            var userClient2 = new TwitterClient(userCredentials);
            var authenticatedUser = await userClient.Users.GetAuthenticatedUserAsync();
            Console.WriteLine("Hello " + authenticatedUser);

            // publish a tweet
            
           


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
                    if (myTweet.Quantity == 0)
                    {
                        var tweet = await userClient.Tweets.PublishTweetAsync(myTweet.Text + myTweet.TimesTweeted);
                        Console.WriteLine("You published the tweet : " + tweet);
                    }
                    else if (myTweet.Quantity > 1)
                    {
                        var tweet = await userClient.Tweets.PublishTweetAsync($"User Email: {myTweet.Social_Activist_Email}  Bought :{myTweet.Quantity} {myTweet.Product_Name}s, To help {myTweet.Campaign_Hashtag} Success");
                        myTweet.Text = tweet.ToString();
                        Console.WriteLine("You published the tweet : " + tweet);
                    }
                    else
                    {
                        var tweet = await userClient.Tweets.PublishTweetAsync($"User Email: {myTweet.Social_Activist_Email}  Bought: {myTweet.Quantity} {myTweet.Product_Name}, To help {myTweet.Campaign_Hashtag} Success");
                        myTweet.Text = tweet.ToString();
                        Console.WriteLine("You published the tweet : " + tweet);
                    }
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

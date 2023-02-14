
using PromoteIt.Entities;
using PromoteIt.Data.Sql;
using PromoteIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyUtilities;


namespace PromoteIt.Entities
{
    public class MainManager
    {
        public Logger Logger;
        public UserMessages messages;
        public int UserBalance = 0;
        public Campaigns campaigns;
        public Users users;
        public Tweets tweets;
        public Products products;
        public Dictionary<int, object> campaignsList;
        public Dictionary<int, object> productsList;
        public Dictionary<int, object> usersList;
        public Dictionary<int, object> tweetsList;
        public UserMessage message;
        private static MainManager _Instance = new MainManager();
        public static MainManager Instance { get { return _Instance; } }
        private MainManager() { Init(); }
        private string LoggerType = Environment.GetEnvironmentVariable("LogProvider");
        
        public void Init()
        {
            Logger = new Logger(LoggerType);
            messages = new UserMessages(Logger);
            campaigns = new Campaigns(Logger);
            users = new Users(Logger);
            tweets = new Tweets(Logger);
            products = new Products(Logger);
            campaignsList = new Dictionary<int, object>();
            productsList = new Dictionary<int, object>();
            usersList = new Dictionary<int, object>();
            tweetsList = new Dictionary<int, object>();
            message = new UserMessage();
        }

        public void InitCampaign(string Email)
        {
            campaignsList = campaigns.LoadCampaigns(Email);
        }
        public void InitCampaign()
        {
            campaignsList = campaigns.LoadCampaigns();
        }

        public void InitProducts(string Email)
        {
            productsList = products.LoadProducts(Email);
        }

        public void InitMyProductsSupplied(string Email)
        {
            productsList = products.LoadMyProductsSupplied(Email);
        }
        public void InitMyProductsNotSupplied(string Email)
        {
            productsList = products.LoadMyProductsNotSupplied(Email);
        }
        public void InitProductsBought(string Email)
        {
            productsList = products.LoadProductsBought(Email);
        }
        public void InitProducts()
        {
            productsList = products.LoadProducts();
        }
        public void InitUsers()
        {
            usersList = users.LoadUsers();
        }
        public void InitTweets()
        {
            tweetsList = tweets.LoadTweets();
        }
        public void InitTweets(string Email)
        {
            tweetsList = tweets.LoadTweets(Email);
        }
        public void getBalance(string Email)
        {
            UserBalance = users.LoadBalance(Email);
        }
    }
}

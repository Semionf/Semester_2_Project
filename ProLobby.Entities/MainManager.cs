
using ProLobby.Entities;
using PromoteIt.Data.Sql;
using PromoteIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Entities
{
    public class MainManager
    {
        public UserMessages messages = new UserMessages();
        public int UserBalance = 0;
        public Campaigns campaigns= new Campaigns();
        public Users users = new Users();
        public Tweets tweets= new Tweets();
        public Products products= new Products();
        private static MainManager _Instance = new MainManager();
        public static MainManager Instance { get { return _Instance; } }
        private MainManager() { }
        public Dictionary<int, object> campaignsList = new Dictionary<int, object>();
        public Dictionary<int, object> productsList = new Dictionary<int, object>();
        public Dictionary<int, object> usersList = new Dictionary<int, object>();
        public Dictionary<int, object> tweetsList = new Dictionary<int, object>();
        public UserMessage message = new UserMessage();

        public MyQueue MyQueue;
        public void Init()
        {
            MyQueue = new MyQueue();
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

﻿
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
        public Products products= new Products();
        private static MainManager _Instance = new MainManager();
        public static MainManager Instance { get { return _Instance; } }
        private MainManager() { }
        public Hashtable campaignsList = new Hashtable();
        public Hashtable productsList = new Hashtable();
        public Hashtable usersList = new Hashtable();
        public UserMessage message = new UserMessage();
        public void InitCampaign(string Email)
        {
            campaignsList = (Hashtable)campaigns.LoadCampaigns(Email);
        }
        public void InitCampaign()
        {
            campaignsList = (Hashtable)campaigns.LoadCampaigns();
        }
        public void InitProducts(string Email)
        {
            productsList = (Hashtable)products.LoadProducts(Email);
        }

        public void InitMyProductsBought(string Email)
        {
            productsList = (Hashtable)products.LoadMyProductsBought(Email);
        }

        public void InitProductsBought(string Email)
        {
            productsList = (Hashtable)products.LoadProductsBought(Email);
        }
        public void InitProducts()
        {
            productsList = (Hashtable)products.LoadProducts();
        }
        public void InitUsers()
        {
            usersList = (Hashtable)users.LoadUsers();
        }
        public void getBalance(string Email)
        {
            UserBalance = users.LoadBalance(Email);
        }
    }
}

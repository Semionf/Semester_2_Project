using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoteIt.DAL;
using PromoteIt.Model;

namespace PromoteIt.Data.Sql
{
   
    public class Social_Activists
    {
        public void AddUser(Social_Activist user)
        {
            string Query = string.Format("INSERT INTO Social_Activist (Email, Address, Phone, MoneyEarned) VALUES ('{0}', '{1}', '{2}', 0)", user.Email, user.Address, user.Phone);
            SqlQuery.RunNonQuery(Query);
        }
    }

    public class Non_Profities
    {
        public void AddUser(Non_Profit user)
        {
            string Query = string.Format("INSERT INTO Non_Profit (Name, Email, Link) VALUES ('{0}', '{1}', '{2}', 0)", user.Non_Profit_Name, user.Email, user.Link_Non_Profit_Website);
            SqlQuery.RunNonQuery(Query);
        }
    }

    public class Business_Companies
    {
        public void AddUser(Business_Company user)
        {
            string Query = string.Format("INSERT INTO Business_Company  (Name, Email, Link) VALUES ('{0}', '{1}', '{2}', 0)", user.Business_Company_Name, user.Email, user.Link_Business_Website);
            SqlQuery.RunNonQuery(Query);
        }
    }

    public class Tweets
    {
        //public void AddTweet(Tweet);
    }

    public class Products
    {
        public void BuyProduct(Product product)
        {
            
            string Query = string.Format("INSERT INTO Products_Bought  (Name, Quantity, Social_Activist_ID, ProductID) VALUES ('{0}', '{1}', '{2}', 0)", product.Name, product.Quantity, product.Social_Activist_ID, product.ID);
            
            SqlQuery.RunNonQuery(Query);
        }
        public void addProduct(Product product)
        {
            string Query = string.Format("INSERT INTO Product (Price, Business_Company_ID) VALUES ({0}, '{1}', '{2}')", product.Name, product.Price, product.Business_Company_ID);
            SqlQuery.RunNonQuery(Query);
        }
        public object LoadProducts()
        {
            {
                return SqlQuery.RunCommandResult("Select * from Products_Bought", insertToHashTableFromDB);
            }
        }

        public object insertToHashTableFromDB(SqlDataReader reader)
        {
            Hashtable products_bought = new Hashtable();
            while (reader.Read())
            {
                Model.Product_Bought product = new Model.Product_Bought();
                product.ID = reader.GetInt32(0);
                product.Name = reader.GetString(1);
                product.Quantity = reader.GetInt32(2);
                product.Social_Activist_ID = reader.GetInt32(3);
                product.TweetID = reader.GetInt32(5);

                products_bought.Add(product.ID, product);
            }
            return products_bought;
        }
    }

    public class Campaigns
    {
        public object LoadCampaigns()
        {
            {
                return SqlQuery.RunCommandResult("Select * from Campaign", insertToHashTableFromDB);
            }
        }
        public object insertToHashTableFromDB(SqlDataReader reader)
        {
           Hashtable campaigns = new Hashtable();
            while (reader.Read())
            {
                Model.Campaign campaign = new Model.Campaign();
                campaign.ID = reader.GetInt32(0);
                campaign.CampaignName = reader.GetString(1);
                campaign.Link_Campaign_URL = reader.GetString(2);
                campaign.HashTag = reader.GetString(3);
                campaign.Non_Profit_ID = reader.GetInt32(4);

                campaigns.Add(campaign.ID, campaign);
            }
            return campaigns;
        }
        public void addCampaign(Campaign campaign)
        {
            string Query = string.Format("INSERT INTO Campaign (CampaignName, Link_Campaign_URL, HashTag, Non_Profit_ID) VALUES ('{0}', '{1}', '{2}',{3})", campaign.CampaignName, campaign.Link_Campaign_URL, campaign.HashTag, campaign.Non_Profit_ID);
            SqlQuery.RunNonQuery(Query);
        }
    }

    public class UserMessages
    {
        public void addMessage(UserMessage message)
        {
            string Query = string.Format("INSERT INTO ContactUs (FullName, UserMessage, Email) VALUES ('{0}', '{1}', '{2}')", message.FullName, message.User_Message, message.Email);
            SqlQuery.RunNonQuery(Query);
        }
    }

    public class Users
    {
        string Query;
        public void addUser(User user)
        {
            if(user.Role == "Non-Profit Organization Representative")
            {
                 Query = string.Format($"declare @UserID int \r\n INSERT INTO Users (Role, Email) VALUES ('{user.Role}', '{user.Email}') \r\n select @UserID = (Select ID from Users where Email = '{user.Email}')\r\n " +
                     $"INSERT INTO Non_Profit_Organization (Name, Email, Link, User_ID) VALUES ('{user.Name}','{user.Email}', '{user.Link}',@UserID)");
            }
            else if (user.Role == "Business Company Representative")
            {
                Query = string.Format($"declare @UserID int \r\n INSERT INTO Users (Role, Email) VALUES ('{user.Role}', '{user.Email}')\r\n select @UserID = (Select ID from Users where Email = '{user.Email}')\r\n INSERT INTO Business_Company (Name, Email, Link, User_ID) VALUES ('{user.Name}','{user.Email}', '{user.Link}',@UserID)");
            }
            else
            {
                Query = string.Format($"declare @UserID int \r\n INSERT INTO Users (Role, Email) VALUES ('{user.Role}', '{user.Email}')\r\n select @UserID = (Select ID from Users where Email = '{user.Email}')\r\n INSERT INTO Social_Activist ( Email, Address, Link, User_ID) VALUES ('{user.Email}', '{user.Address}', '{user.Link}',@UserID)");
            }
            SqlQuery.RunNonQuery(Query);
        }
   
        public object LoadUsers()
        {
            {
                return SqlQuery.RunCommandResult("Select * from Users", insertToHashTableFromDB);
            }
        }
        public object insertToHashTableFromDB(SqlDataReader reader)
        {
            Hashtable users = new Hashtable();
            while (reader.Read())
            {
                Model.User user = new Model.User();
                user.ID = reader.GetInt32(0);
                user.Role = reader.GetString(1);
                user.Email = reader.GetString(2);

                users.Add(user.ID, user);
            }
            return users;
        }
      
        public Boolean checkUser(string email)
        {
            string answer = SqlQuery.RunCommandCheck("declare @answer nvarchar(10)\r\n\r\nif Exists (Select * from Users where Email = @email)\r\nbegin\r\n\tselect @answer = 'True'\r\nend\r\nelse\r\nbegin\r\n\tselect @answer = 'False'\r\nend\r\n\r\nselect @answer",email);
            if (answer == "True")
            {
                return true;
            }
            return false;
        }
        
    }
}

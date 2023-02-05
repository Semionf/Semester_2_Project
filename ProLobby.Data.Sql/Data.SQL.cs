using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoteIt.DAL;
using PromoteIt.Model;
using Tweetinvi.Core.Models;

namespace PromoteIt.Data.Sql
{

    public class Social_Activists
    {
        public void AddUser(Social_Activist user)
        {
            string Query = string.Format($"INSERT INTO Social_Activist (Email, Address, Phone, MoneyEarned) VALUES ('{user.Email}', '{user.Address}', '{user.Phone}', 0)");
            SqlQuery.RunNonQuery(Query);
        }
    }

    public class Non_Profities
    {
        public void AddUser(Non_Profit user)
        {
            string Query = string.Format($"INSERT INTO Non_Profit (Name, Email, Link) VALUES ('{user.Non_Profit_Name}', '{user.Email}', '{user.Link_Non_Profit_Website}')");
            SqlQuery.RunNonQuery(Query);
        }
    }

    public class Business_Companies
    {
        public void AddUser(Business_Company user)
        {
            string Query = string.Format($"INSERT INTO Business_Company  (Name, Email, Link) VALUES ('{user.Business_Company_Name}', '{user.Email}', '{user.Link_Business_Website}')");
            SqlQuery.RunNonQuery(Query);
        }
    }

    public class Tweets
    {
        public void addTweet(myTweet tweet)
        {
            string Query = string.Format($"if not exists(select * from Tweet where Text = '{tweet.Text}') begin insert into Tweet (Social_Activist_Email, Campaign_Hashtag, Text, TimesTweeted) VALUES ('{tweet.Social_Activist_Email}', '{tweet.Campaign_Hashtag}', '{tweet.Text}', 1) end else begin update Tweet set TimesTweeted = TimesTweeted + 1 where Text = '{tweet.Text}' and Social_Activist_Email = '{tweet.Social_Activist_Email}' end update Social_Activist set MoneyEarned = MoneyEarned + 1 where Email = '{tweet.Social_Activist_Email}'");
            SqlQuery.RunNonQuery(Query);
        }

        public Dictionary<int, object> LoadTweets()
        {
            {
                return SqlQuery.RunCommandResult($"Select * from Tweet", insertTweetsToDictionaryFromDB);
            }
        }
        public Dictionary<int, object> LoadTweets(string Email)
        {
            {
                return SqlQuery.RunCommandResultTweet($"Select * from Tweet where Social_Activist_Email = '{Email}'", insertTweetsToDictionaryFromDB);
            }
        }
        public Dictionary<int, object> insertTweetsToDictionaryFromDB(SqlDataReader reader)
        {
            Dictionary<int, object> tweets = new Dictionary<int, object>();
            try
            {
                while (reader.Read())
                {
                    Model.myTweet tweet = new Model.myTweet();
                    tweet.ID = reader.GetInt32(0);
                    tweet.Social_Activist_Email = reader.GetString(1);
                    tweet.Campaign_Hashtag = reader.GetString(2);
                    tweet.Text = reader.GetString(3);
                    tweet.TimesTweeted = reader.GetInt32(4);
                    tweets.Add(tweet.ID, tweet);
                }
            }
            catch (Exception ex)
            {

                return null;
            }
           
            return tweets;
        }
    }

    public class Products
    {

        public void addProduct(Product product)
        {
            string Query = string.Format($"declare @email nvarchar(40) select @email = (select Non_Profit_Organization.Organization_Email from \r\nNon_Profit_Organization inner join\r\nCampaign on Campaign.Organization_Email = Non_Profit_Organization.Organization_Email\r\nwhere Hashtag = '{product.CampaignHashtag}')  \r\nINSERT INTO Donation (Name, Price, Quantity, Business_Email, Organization_Email, Campaign_Hashtag) VALUES ('{product.Name}', {product.Price}, {product.Quantity},'{product.Business_Email}',@email, '{product.CampaignHashtag}')");
            SqlQuery.RunNonQuery(Query);
        }
        public void supply(Product product)
        {
            string Query = string.Format($"  UPDATE Supply Set isSent = 1 where Products_Bought_ID = {product.ID}");
            SqlQuery.RunNonQuery(Query);
        }
        public void buyProduct(Product product)
        {
            string Query = string.Format($"declare @Email nvarchar(40), @ID int select @Email = (select Business_Email from Donation where Name = '{product.Name}' and ID = {product.ID})\r\nif not exists(Select DonationID from Products_Bought where DonationID = {product.ID} and Social_Activist_Email = '{product.Activist_Email}')\r\n\tbegin\r\n\t\tInsert into Products_Bought (Name, Quantity, Social_Activist_Email,\r\n\t\tBusiness_Email, DonationID, Price, Hashtag) Values ('{product.Name}',{product.Quantity},'{product.Activist_Email}',@Email,{product.ID},{product.Price}, '{product.CampaignHashtag}')\r\n\tend\r\nelse\r\n\tbegin\r\n\t\tUPDATE Products_Bought SET Quantity = Quantity + {product.Quantity}, Price = Price + {product.Price} where DonationID = {product.ID}\r\n\tend Update Donation Set Quantity = Quantity - {product.Quantity} where ID = {product.ID} Update Social_Activist SET MoneyEarned = MoneyEarned - {product.Price} where Email = '{product.Activist_Email}' select @ID = (Select ID from Products_Bought where DonationID = {product.ID} and Social_Activist_Email = '{product.Activist_Email}' )\r\n\tif not exists (select * from Supply where Products_Bought_ID = @ID) begin insert into Supply (Products_Bought_ID, IsSent) values(@ID,0) end");
            SqlQuery.RunNonQuery(Query);
        }
        public Dictionary<int, object> LoadProducts(string Email)
        {
            {
                return SqlQuery.RunCommandResult($"if exists(SELECT * FROM Donation WHERE Organization_Email = '{Email}')\r\nbegin\r\n\tselect * From Donation where Organization_Email in ('{Email}') and Quantity <> 0\r\nend", insertDonatedToDictionaryFromDB);
            }
        }
        public Dictionary<int, object> LoadProductsBought(string Email)
        {
            {
                return SqlQuery.RunCommandResult($"if exists(SELECT Business_Email FROM Products_Bought WHERE Business_Email = '{Email}')\r\n\t\tbegin\r\n\t\t\tselect * From Products_Bought inner join Supply ON Products_Bought_ID = Products_Bought.ID\r\n\t\t\twhere Business_Email in ('{Email}')\r\n\t\tend", insertBoughtToDictionaryFromDB);
            }
        }
        public Dictionary<int, object> LoadMyProductsSupplied(string Email)
        {
            {
                return SqlQuery.RunCommandResult($" if exists (select * from Products_Bought inner join Supply ON Products_Bought_ID = Products_Bought.ID \r\n  where Social_Activist_Email = '{Email}' and IsSent = 1 and Quantity > 0 )\r\n\tbegin\r\n\t\tselect * from Products_Bought inner join Supply ON Products_Bought_ID = Products_Bought.ID where Social_Activist_Email = '{Email}' and IsSent = 1\r\n\tend", insertActivistProductsToDictionaryFromDB);
            }
        }
        public Dictionary<int, object> LoadMyProductsNotSupplied(string Email)
        {
            {
                return SqlQuery.RunCommandResult($" if exists (select * from Products_Bought inner join Supply ON Products_Bought_ID = Products_Bought.ID \r\n  where Social_Activist_Email = '{Email}' and IsSent = 0 and Quantity > 0 )\r\n\tbegin\r\n\t\tselect * from Products_Bought inner join Supply ON Products_Bought_ID = Products_Bought.ID where Social_Activist_Email = '{Email}' and IsSent = 0\r\n\tend", insertActivistProductsToDictionaryFromDB);
            }
        }
        public Dictionary<int, object> LoadProducts()
        {
            {
                return SqlQuery.RunCommandResult($"Select * from Donation where Quantity <> 0", insertListToDictionaryFromDB);
            }
        }
        public Dictionary<int, object> insertBoughtToDictionaryFromDB(SqlDataReader reader)
        {
            Dictionary<int, object> products = new Dictionary<int, object>();
            try
            {
                while (reader.Read())
                {
                    Model.Product product = new Model.Product();
                    product.ID = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Quantity = reader.GetInt32(2);
                    product.Price = reader.GetDecimal(3);
                    product.Activist_Email = reader.GetString(4);
                    product.Business_Email = reader.GetString(5);
                    product.CampaignHashtag = reader.GetString(8);
                    product.isSent = reader.GetBoolean(11);
                    products.Add(product.ID, product);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
           
            return products;
        }
        public Dictionary<int, object> insertDonatedToDictionaryFromDB(SqlDataReader reader)
        {
            Dictionary<int, object> products = new Dictionary<int, object>();
            try
            {
                while (reader.Read())
                {
                    Model.Product product = new Model.Product();
                    product.ID = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Price = reader.GetDecimal(2);
                    product.Quantity = reader.GetInt32(3);
                    product.Business_Email = reader.GetString(4);
                    product.CampaignHashtag = reader.GetString(5);


                    products.Add(product.ID, product);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
           
            return products;
        }
        public Dictionary<int, object> insertActivistProductsToDictionaryFromDB(SqlDataReader reader)
        {
            Dictionary<int, object> products = new Dictionary<int, object>();
            try
            {
                while (reader.Read())
                {
                    Model.Product product = new Model.Product();
                    product.ID = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Quantity = reader.GetInt32(2);
                    product.Price = reader.GetDecimal(3);
                    product.Activist_Email = reader.GetString(4);
                    product.Business_Email = reader.GetString(5);
                    product.CampaignHashtag = reader.GetString(8);
                    products.Add(product.ID, product);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
           
            return products;
        }
        public Dictionary<int, object> insertListToDictionaryFromDB(SqlDataReader reader)
        {
            Dictionary<int, object> productsList = new Dictionary<int, object>();
            try
            {
                while (reader.Read())
                {
                    Model.Product product = new Model.Product();
                    product.ID = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Price = reader.GetDecimal(2);
                    product.Quantity = reader.GetInt32(3);
                    product.CampaignHashtag = reader.GetString(5);

                    productsList.Add(product.ID, product);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
           
            return productsList;
        }
    }

    public class Campaigns
    {
        public Dictionary<int, object> LoadCampaigns(string Email)
        {
            {
                return SqlQuery.RunCommandResult($"Select * from Campaign where Organization_Email in ('{Email}')", insertToDictionaryFromDB);
            }
        }
        public Dictionary<int, object> LoadCampaigns()
        {
            {
                return SqlQuery.RunCommandResult($"Select * from Campaign", insertToDictionaryFromDB);
            }
        }
        public Dictionary<int, object> insertToDictionaryFromDB(SqlDataReader reader)
        {
            Dictionary<int, object> campaigns = new Dictionary<int, object>();
            try
            {
                while (reader.Read())
                {
                    Model.Campaign campaign = new Model.Campaign();
                    campaign.ID = reader.GetInt32(0);
                    campaign.Link = reader.GetString(1);
                    campaign.Hashtag = reader.GetString(2);
                    campaign.Email = reader.GetString(3);

                    campaigns.Add(campaign.ID, campaign);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
           
            return campaigns;
        }
        public void addCampaign(Campaign campaign)
        {
            string Query = string.Format($"INSERT INTO Campaign (Link, Hashtag, Organization_Email) VALUES ('{campaign.Link}', '{campaign.Hashtag}', '{campaign.Email}')");
            SqlQuery.RunNonQuery(Query);
        }
    }

    public class UserMessages
    {
        public void addMessage(UserMessage message)
        {
            string Query = string.Format($"INSERT INTO ContactUs (FullName, UserMessage, Email) VALUES ('{message.FullName}', '{message.User_Message}', '{message.Email}')");
            SqlQuery.RunNonQuery(Query);
        }
    }
    public class Users
    {
        string Query;
        public void addUser(myUser user)
        {
            if (user.Role == "Non-Profit Organization Representative")
            {
                Query = string.Format($"INSERT INTO Users (Role, Email) VALUES ('{user.Role}', '{user.Email}') \r\n INSERT INTO Non_Profit_Organization (Name, Email, Link, Organization_Email) VALUES ('{user.Name}','{user.Email}', '{user.Link}','{user.Email}')");
            }
            else if (user.Role == "Business Company Representative")
            {
                Query = string.Format($"INSERT INTO Users (Role, Email) VALUES ('{user.Role}', '{user.Email}')\r\n \r\n INSERT INTO Business_Company (Name, Email, Link, Business_Email) VALUES ('{user.Name}','{user.Email}', '{user.Link}', '{user.Email}')");
            }
            else
            {
                Query = string.Format($"INSERT INTO Users (Role, Email) VALUES ('{user.Role}', '{user.Email}')\r\n INSERT INTO Social_Activist ( Email, Address, Phone, Activist_Email, MoneyEarned) VALUES ('{user.Email}', '{user.Address}', '{user.Phone}''{user.Link}','{user.Email}',0)");
            }
            SqlQuery.RunNonQuery(Query);
        }

        public Dictionary<int, object> LoadUsers()
        {
            {
                return SqlQuery.RunCommandResult("Select * from Users", insertToDictionaryFromDB);
            }
        }

        public int LoadBalance(string Email)
        {
            return SqlQuery.RunCommandResultInt($"Select MoneyEarned from Social_Activist where Email = '{Email}'", getBalance);
        }
        public Dictionary<int, object> insertToDictionaryFromDB(SqlDataReader reader)
        {
            Dictionary<int, object> users = new Dictionary<int, object>();
            try
            {
                while (reader.Read())
                {
                    Model.myUser user = new Model.myUser();
                    user.ID = reader.GetInt32(0);
                    user.Role = reader.GetString(1);
                    user.Email = reader.GetString(2);

                    users.Add(user.ID, user);
                }
            }
            catch (Exception ex)
            {

            }
            return users;
        }

        public int getBalance(SqlDataReader reader)
        {
            int balance = 0;
            try
            {
                while (reader.Read())
                {
                    balance = (int)reader.GetDecimal(0);
                }
            }
            catch (Exception ex)
            {

            }
            return balance;
        }

        public bool checkUser(string email)
        {
            string answer = SqlQuery.RunCommandCheck("declare @answer nvarchar(10)\r\n\r\nif Exists (Select * from Users where Email = @email)\r\nbegin\r\n\tselect @answer = 'True'\r\nend\r\nelse\r\nbegin\r\n\tselect @answer = 'False'\r\nend\r\n\r\nselect @answer", email);
            if (answer == "True")
            {
                return true;
            }
            return false;
        }

    }
}

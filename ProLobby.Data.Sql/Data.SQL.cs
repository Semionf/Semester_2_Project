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
            string Query = string.Format($"declare @email nvarchar(40) select @email = (select Non_Profit_Organization.Organization_Email from \r\nNon_Profit_Organization inner join\r\nCampaign on Campaign.Organization_Email = Non_Profit_Organization.Organization_Email\r\nwhere Hashtag = '{product.CampaignHashtag}')  \r\nINSERT INTO Donation (Name, Price, Quantity, Business_Email, Organization_Email, Campaign_Hashtag) VALUES ('{product.Name}', {product.Price}, {product.Quantity},'{product.Business_Email}',@email, '{product.CampaignHashtag}')");
            SqlQuery.RunNonQuery(Query);
        }
        public object LoadProducts(string Email)
        {
            {
                return SqlQuery.RunCommandResult($"if exists(SELECT * FROM Donation WHERE Organization_Email = '{Email}')\r\nbegin\r\n\tselect * From Donation where Organization_Email in ('{Email}')\r\nend", insertBoughtToHashTableFromDB);
            }
        }
        public object LoadProductsBought(string Email)
        {
            {
                return SqlQuery.RunCommandResult($"if exists(SELECT Business_Email FROM Products_Bought WHERE Business_Email = '{Email}')\r\nbegin\r\n\tselect * From Products_Bought where Business_Email in ('{Email}')\r\nend", insertBoughtToHashTableFromDB);
            }
        }
        public object LoadMyProductsBought(string Email)
        {
            {
                return SqlQuery.RunCommandResult($"if exists(SELECT Social_Activist_Email FROM Products_Bought WHERE Social_Activist_Email = '{Email}')\r\nbegin\r\n\tselect * From Products_Bought where Social_Activist_Email in ('{Email}')\r\nend", insertBoughtToHashTableFromDB);
            }
        }
        public object LoadProducts()
        {
            {
                return SqlQuery.RunCommandResult($"Select * from Donation", insertListToHashTableFromDB);
            }
        }
        public object insertBoughtToHashTableFromDB(SqlDataReader reader)
        {
            Hashtable products = new Hashtable();
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
            return products;
        }
        public object insertListToHashTableFromDB(SqlDataReader reader)
        {
            Hashtable productsList = new Hashtable();
            while (reader.Read())
            {
                Model.Product product = new Model.Product();
                product.ID = reader.GetInt32(0);
                product.Name = reader.GetString(1);
                product.Price = reader.GetDecimal(2);
                product.Quantity = reader.GetInt32(3);
                product.CampaignHashtag= reader.GetString(5);

                productsList.Add(product.ID, product);
            }
            return productsList;
        }
    }

    public class Campaigns
    {
        public object LoadCampaigns(string Email)
        {
            {
                return SqlQuery.RunCommandResult($"Select * from Campaign where Organization_Email in ('{Email}')", insertToHashTableFromDB);
            }
        }
        public object LoadCampaigns()
        {
            {
                return SqlQuery.RunCommandResult($"Select * from Campaign", insertToHashTableFromDB);
            }
        }
        public object insertToHashTableFromDB(SqlDataReader reader)
        {
           Hashtable campaigns = new Hashtable();
            while (reader.Read())
            {
                Model.Campaign campaign = new Model.Campaign();
                campaign.ID = reader.GetInt32(0);
                campaign.Link = reader.GetString(1);
                campaign.Hashtag = reader.GetString(2);
                campaign.Email = reader.GetString(3);

                campaigns.Add(campaign.ID, campaign);
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
   
        public object LoadUsers()
        {
            {
                return SqlQuery.RunCommandResult("Select * from Users", insertToHashTableFromDB);
            }
        }

        public object LoadBalance(string Email)
        {
            return SqlQuery.RunCommandResult($"Select MoneyEarned from Social_Activist where Email = '{Email}'", getBalance);
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

        public object getBalance(SqlDataReader reader)
        {
            int balance = 0;
            while (reader.Read())
            {
                balance = (int)reader.GetDecimal(0);
            }
            return balance;
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

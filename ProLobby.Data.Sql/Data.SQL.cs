using System;
using System.Collections.Generic;
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
       public void AddTweet(Tweet)
    }

    public class Products
    {
        public void BuyProduct(Product product)
        {
            
            string Query = string.Format("INSERT INTO Products_Bought  (Name, Quantity, Social_Activist_ID, ProductID, TweetID) VALUES ('{0}', '{1}', '{2}', 0)", product.Name, product.Quantity, product.Social_Activist_ID);
            
            SqlQuery.RunNonQuery(Query);
        }
    }

    public class Campaigns
    { 

    }

}

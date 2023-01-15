using PromoteIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Entities
{
    public class Tweets
    {
        PromoteIt.Data.Sql.Tweets dataSql = new PromoteIt.Data.Sql.Tweets();
        public void addTweet(myTweet tweet)
        {
            dataSql.addTweet(tweet);
        }
      
        public object LoadTweets()
        {

            return dataSql.LoadTweets();
        }
        public object LoadTweets(string Email)
        {

            return dataSql.LoadTweets(Email);
        }
    }
}

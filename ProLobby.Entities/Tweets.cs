using PromoteIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
namespace PromoteIt.Entities
{
    public class Tweets:BaseEntity
    {
        PromoteIt.Data.Sql.Tweets dataSql = new PromoteIt.Data.Sql.Tweets();

        public Tweets(Logger log) : base(log)
        {
        }

        public void addTweet(myTweet tweet)
        {
            dataSql.addTweet(tweet);
        }
      
        public Dictionary<int, object> LoadTweets()
        {

            return dataSql.LoadTweets();
        }
        public Dictionary<int, object> LoadTweets(string Email)
        {

            return dataSql.LoadTweets(Email);
        }
    }
}

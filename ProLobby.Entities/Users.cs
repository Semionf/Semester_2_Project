using PromoteIt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoteIt.Entities
{
    public class Users:BaseEntity
    {
        PromoteIt.Data.Sql.Users dataSql = new PromoteIt.Data.Sql.Users();
        
        public Users(Logger log) : base(log)
        {
        }
        
        public void addUser(myUser user)
        {
            dataSql.addUser(user);
        }
        public Dictionary<int, object> LoadUsers()
        {
            return dataSql.LoadUsers();
        }
        public int LoadBalance(string Email)
        {
            return dataSql.LoadBalance(Email);
        }
        public bool checkUser(string email)
        {
            return dataSql.checkUser(email);
        }
    }
}

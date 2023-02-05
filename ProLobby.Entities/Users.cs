using PromoteIt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Entities
{
    public class Users
    {
        PromoteIt.Data.Sql.Users dataSql = new PromoteIt.Data.Sql.Users();
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

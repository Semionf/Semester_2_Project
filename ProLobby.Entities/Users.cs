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
        public void addUser(User user)
        {
            dataSql.addUser(user);
        }
        public object LoadUsers()
        {

            return dataSql.LoadUsers();
        }
       
        public object checkUser(string email)
        {
            return dataSql.checkUser(email);
        }
    }
}

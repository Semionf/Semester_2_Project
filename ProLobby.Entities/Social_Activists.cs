using PromoteIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PromoteIt.Entities
{
    public class Social_Activists:BaseEntity
    {

        public Social_Activists(Logger log) : base(log)
        {
        }

        public void Add_User(Social_Activist user)
        {
            Data.Sql.Social_Activists DataSql = new Data.Sql.Social_Activists();
            DataSql.AddUser(user);
        }
    }
}

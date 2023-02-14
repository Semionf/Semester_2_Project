using PromoteIt.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromoteIt.Model;
using Utilities;

namespace PromoteIt.Entities
{
    public class UserMessages:BaseEntity
    {
        public UserMessages(Logger log) : base(log)
        {
            
        }
       
        public void addMessage(UserMessage message)
        {
            PromoteIt.Data.Sql.UserMessages dataSql = new PromoteIt.Data.Sql.UserMessages();
            dataSql.addMessage(message);
        }
    }
}

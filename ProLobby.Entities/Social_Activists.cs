using PromoteIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Entities
{
    public class Social_Activists
    {
        public void Add_User(Social_Activist user)
        {
            Data.Sql.Social_Activists DataSql = new Data.Sql.Social_Activists();
            DataSql.AddUser(user);
        }

        public void Buy_Product(Product product)
        {
            Data.Sql.Products DataSql = new Data.Sql.Products();
            DataSql.BuyProduct(product);
        }
    }
}

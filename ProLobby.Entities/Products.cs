using PromoteIt.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoteIt.Entities
{
    public class Products
    {
        private static PromoteIt.Data.Sql.Products dataSql = new PromoteIt.Data.Sql.Products();
        public void addProduct(Product product)
        {
            dataSql.addProduct(product);
        }
        public object LoadProducts()
        {
            return dataSql.LoadProducts();
        }
    }
}

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
        public object LoadProducts(string Email)
        {
            return dataSql.LoadProducts(Email);
        }
        public object LoadProductsBought(string Email)
        {
            return dataSql.LoadProductsBought(Email);
        }
        public object LoadMyProductsBought(string Email)
        {
            return dataSql.LoadMyProductsBought(Email);
        }
        public object LoadProducts()
        {
            return dataSql.LoadProducts();
        }
    }
}

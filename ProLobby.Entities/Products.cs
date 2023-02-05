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
        public void supply(Product product)
        {
            dataSql.supply(product);
        }
        public void buyProduct(Product product)
        {
            dataSql.buyProduct(product);
        }
        public Dictionary<int, object> LoadProducts(string Email)
        {
            return dataSql.LoadProducts(Email);
        }
        public Dictionary<int, object> LoadProductsBought(string Email)
        {
            return dataSql.LoadProductsBought(Email);
        }
        public Dictionary<int, object> LoadMyProductsSupplied(string Email)
        {
            return dataSql.LoadMyProductsSupplied(Email);
        }

        public Dictionary<int, object> LoadMyProductsNotSupplied(string Email)
        {
            return dataSql.LoadMyProductsNotSupplied(Email);
        }

        public Dictionary<int, object> LoadProducts()
        {
            return dataSql.LoadProducts();
        }
    }
}

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
        public object LoadProducts(string Email)
        {
            return dataSql.LoadProducts(Email);
        }
        public object LoadProductsBought(string Email)
        {
            return dataSql.LoadProductsBought(Email);
        }
        public object LoadMyProductsSupplied(string Email)
        {
            return dataSql.LoadMyProductsSupplied(Email);
        }

        public object LoadMyProductsNotSupplied(string Email)
        {
            return dataSql.LoadMyProductsNotSupplied(Email);
        }

        public object LoadProducts()
        {
            return dataSql.LoadProducts();
        }
    }
}

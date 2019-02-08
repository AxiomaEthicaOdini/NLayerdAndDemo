using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Concrete.NHiberrnate
{
    public class NhProductDal : IProductDal
    {
        public void Add(Product product)
        {
    
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            List<Product> products = new List<Product>
            {
                new Product{ProductId=1, CategoryId=1, ProductName="Laptop", QuantityPerUnit= "1 in a box", UnitPrice=3000, UnitsInStock=10},
                new Product{ProductId=2, CategoryId=2, ProductName="Monitör", QuantityPerUnit= "1 in a box", UnitPrice=1200, UnitsInStock=25}

            };
            return products;
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}

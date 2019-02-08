using Business.Abstract;
using Business.Utilities;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager:IProductService
    {
        private IProductDal _productdal; 

        public ProductManager(IProductDal productdal)
        {
            _productdal = productdal;
        }

        public void Add(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);
            _productdal.Add(product);
        }

        public void Delete(Product product)
        {
            try
            {
                _productdal.Delete(product);    
            }
            catch
            {
                throw new Exception("Silme İşlemi Başarısız.");
            }
        }

        public List<Product> GetAll()
        {
            return _productdal.GetAll();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productdal.GetAll(p => p.CategoryId == categoryId);
        }     

        public List<Product> GetProductsByProductName(string productName)
        {
            return _productdal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
        }

        public void Update(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);

            _productdal.Update(product);
        }
    }
}

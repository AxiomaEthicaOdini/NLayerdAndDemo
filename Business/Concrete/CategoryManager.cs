using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;    

        public CategoryManager (ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        
        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }
    }
}

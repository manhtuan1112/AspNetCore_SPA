using Samples.AspCoreEF.DAL.EF.Infrastructure;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.DAL.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory ProductCategory);
        void Update(ProductCategory ProductCategory);
        void Delete(long id);
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string keyword);
        IEnumerable<ProductCategory> GetAllByParentId(long parentId);
        ProductCategory GetById(long id);
    }
    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _ProductCategoryRepository;
        public ProductCategoryService(IProductCategoryRepository ProductCategoryRepository)
        {
            this._ProductCategoryRepository = ProductCategoryRepository;
        }

        public ProductCategory Add(ProductCategory ProductCategory)
        {
           return _ProductCategoryRepository.Insert(ProductCategory);
        }

        public void Delete(long id)
        {
            ProductCategory productCate = _ProductCategoryRepository.Get(id);
            _ProductCategoryRepository.Delete(productCate);
            
        }

        public IEnumerable<ProductCategory> GetAll()
        {
           return _ProductCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _ProductCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            else
            {
                return _ProductCategoryRepository.GetAll();
            }
        }

        public IEnumerable<ProductCategory> GetAllByParentId(long parentId)
        {
            throw new NotImplementedException();
        }

        public ProductCategory GetById(long id)
        {
            return _ProductCategoryRepository.GetSingleByCondition(x => x.Id == id);
        }

        public void Update(ProductCategory productCategory)
        {
            _ProductCategoryRepository.Update(productCategory);
        }
    }
}

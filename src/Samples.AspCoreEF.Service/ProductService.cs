using Sample.AspCoreEF.Common;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.DAL.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.Service
{
        public interface IProductService
        {
            void Add(Product Product);
            void Update(Product Product);
            void Delete(long id);
            IEnumerable<Product> GetAll();
            IEnumerable<Product> GetAll(string keyword);
            IEnumerable<Product> GetListProductByCategoryIdPaging(long categoryId, int page, int pageSize, string sort, out int totalRow);
            IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);
            Product GetById(long id);
        }
        public class ProductService : IProductService
        {
            private IProductRepository _productRepository;
            private ITagRepository _tagRepository;
            private IProductTagRepository _productTagRepository;
            
            public ProductService(IProductRepository productRepository,IProductTagRepository productTagRepository, ITagRepository tagRepository)
            {
                this._productRepository = productRepository;
                this._tagRepository = tagRepository;
                this._productTagRepository = productTagRepository;
            }

            public void Add(Product Product)
            {
                 _productRepository.Insert(Product);
                _productRepository.SaveChange();
                if (!string.IsNullOrEmpty(Product.Tags))
                {
                    string[] tags = Product.Tags.Split(',');
                    for(int i = 0; i < tags.Length; i++)
                    {
                        var tagId = StringHelper.ToUnsignString(tags[i]);
                        if (_tagRepository.Count(x => x.ID == tagId) == 0)
                        {
                            Tag tag = new Tag();
                            tag.ID = tagId;
                            tag.Name = tags[i];
                            tag.Type = CommonConstants.ProductTag;
                            _tagRepository.Insert(tag);
                            
                        }
                        ProductTag productTag = new ProductTag();
                        productTag.ProductID = Product.Id;
                        productTag.TagID = tagId;
                        _productTagRepository.Insert(productTag);
                    }
                }
            }

            public void Update(Product Product)
            {
                _productRepository.Update(Product);

            }

            public void Delete(long id)
            {
                Product product = _productRepository.Get(id);
                _productRepository.Delete(product);

            }

            public IEnumerable<Product> GetAll()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Product> GetAll(string keyword)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
                }
                else
                {
                return _productRepository.GetAll();
                }
            }

            public IEnumerable<Product> GetListProductByCategoryIdPaging(long categoryId, int page, int pageSize, string sort, out int totalRow)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
            {
                throw new NotImplementedException();
            }

            public Product GetById(long id)
            {
            return _productRepository.Get(id);
            }
        }
    
}

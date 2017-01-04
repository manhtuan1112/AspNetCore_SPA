using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.Infrastructure.Core;
using Samples.AspCoreEF.Models;
using Samples.AspCoreEF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
namespace Samples.AspCoreEF.Controllers
{
    [Route("api/[controller]")]
    public class ProductCategoryController : Controller
    {
        #region Initialize

        private IMapper _mapper;
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService, IMapper mapper)
        {
            this._mapper = mapper;
            this._productCategoryService = productCategoryService;
        }

        #endregion Initialize
        [Route("getallparents")]
        [HttpGet]
        public IEnumerable<ProductCategoryViewModel> GetAll()
        {
            var model = _productCategoryService.GetAll();
            return _mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
        }
        [HttpGet("getall")]
        public PaginationSet<ProductCategoryViewModel> GetAll(string keyword,int page,int pageSize)
        {
            int totalRow = 0;
            var dataModel = _productCategoryService.GetAll(keyword);
            totalRow = dataModel.Count();
            var query = dataModel.OrderByDescending(x => x.AddedDate).Skip(page * pageSize).Take(pageSize);
            var responseData= _mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);
            var paginationSet = new PaginationSet<ProductCategoryViewModel>()
            {
                Items = responseData,
                Page = page,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
            };
            
            return paginationSet;
        }

        
        [HttpDelete("delete")]
        public IActionResult Delete(long id)
        {
            var productCategory = _productCategoryService.GetById(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _productCategoryService.Delete(id);
            return new NoContentResult();
        }

        [HttpDelete("deletemulti")]
        public IActionResult DeleteMulti(string checkedProductCategories)
        {
            if (String.IsNullOrEmpty(checkedProductCategories))
            {
                return NotFound();
            }

            return NotFound();
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ProductCategoryViewModel productCategory)
        {
            if (productCategory == null) return BadRequest();
            _productCategoryService.Add(_mapper.Map<ProductCategoryViewModel,ProductCategory>(productCategory));
            return new NoContentResult();
        }
        [Route("getbyid/{id:long}")]
        [HttpGet]
        public IActionResult GetById(long id)
        {
            var productCategory = _productCategoryService.GetById(id);
            if (productCategory == null) return NotFound();
            return new ObjectResult(_mapper.Map<ProductCategory,ProductCategoryViewModel>(productCategory));
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] ProductCategoryViewModel productCategory)
        {
            if (productCategory == null) return BadRequest();
            _productCategoryService.Update(_mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategory));
            return new ObjectResult(_mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategory));
        }
    }
}
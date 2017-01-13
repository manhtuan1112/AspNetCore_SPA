using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.Infrastructure.Core;
using Samples.AspCoreEF.Models;
using Samples.AspCoreEF.Service;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Samples.AspCoreEF.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        #region Initialize

        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        private IMapper _mapper;

        public ProductController(IProductCategoryService productCategoryService,IProductService productService, IMapper mapper)
        {
            this._mapper = mapper;
            this._productCategoryService = productCategoryService;
            this._productService = productService;
        }

        #endregion Initialize

        [HttpGet("getall")]
        public PaginationSet<ProductViewModel> GetAll(string keyword, int page, int pageSize)
        {
            int totalRow = 0;
            var dataModel = _productService.GetAll(keyword);

            totalRow = dataModel.Count();
            var query = dataModel.OrderByDescending(x => x.AddedDate).Skip(page * pageSize).Take(pageSize);
            var responseData = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);
            foreach (var proVm in responseData)
            {
                var proCate = _productCategoryService.GetById(proVm.CategoryID);
                proVm.CategoryName = proCate.Name;
            }
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = responseData,
                Page = page,
                TotalCount = totalRow,
                TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
            };

            return paginationSet;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ProductViewModel productVm)
        {
            if (productVm == null) return BadRequest();
            _productService.Add(_mapper.Map<ProductViewModel, Product>(productVm));
            return new NoContentResult();
        }
        
        [HttpDelete("delete")]
        public IActionResult Delete(long id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.Delete(id);
            return new NoContentResult();
        }
        [Route("getbyid/{id:long}")]
        [HttpGet]
        public IActionResult GetById(long id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return new ObjectResult(_mapper.Map<Product, ProductViewModel>(product));
        }
        [HttpPut("update")]
        public IActionResult Update([FromBody] ProductViewModel product)
        {
            if (product == null) return BadRequest();
            _productService.Update(_mapper.Map<ProductViewModel, Product>(product));
            return new ObjectResult(_mapper.Map<ProductViewModel, Product>(product));
        }

    }
}
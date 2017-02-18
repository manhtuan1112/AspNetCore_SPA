using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sample.AspCoreEF.Common.Exceptions;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.Infrastructure.Core;
using Samples.AspCoreEF.Infrastructure.Extensions;
using Samples.AspCoreEF.Models;
using Samples.AspCoreEF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.Controllers
{
    [Route("api/applicationRole")]
    public class ApplicationRoleController : Controller
    {
        private IApplicationRoleService _appRoleService;
        private IMapper _mapper;

        public ApplicationRoleController(IApplicationRoleService appRoleService, IMapper mapper)
        {
            this._appRoleService = appRoleService;
            this._mapper = mapper;
        }
        [Route("getlistpaging")]
        [HttpGet]
        public PaginationSet<ApplicationRoleViewModel> GetListPaging(int page, int pageSize, string filter = null)
        {
                int totalRow = 0;
                var model = _appRoleService.GetAll(page, pageSize, out totalRow, filter);
                IEnumerable<ApplicationRoleViewModel> modelVm = _mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);

                PaginationSet<ApplicationRoleViewModel> pagedSet = new PaginationSet<ApplicationRoleViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };
                return pagedSet;
        }

        [Route("getlistall")]
        [HttpGet]
        public IEnumerable<ApplicationRoleViewModel> GetAll()
        {
                var model = _appRoleService.GetAll();
                IEnumerable<ApplicationRoleViewModel> modelVm = _mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);

                return modelVm;
           
        }
        [Route("detail/{id}")]
        [HttpGet]
        public IActionResult Details(string id)
        {
            ApplicationRole appRole = _appRoleService.GetDetail(id);
            if (appRole == null)
            {
                return NotFound();
            }
            var appRoleVm = _mapper.Map<ApplicationRole, ApplicationRoleViewModel>(appRole);
            return new OkObjectResult(appRoleVm);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Create([FromBody] ApplicationRoleViewModel applicationRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppRole = new ApplicationRole();
                newAppRole.UpdateApplicationRole(applicationRoleViewModel);
                newAppRole.NormalizedName = newAppRole.Name.ToUpper();
                try
                {
                     _appRoleService.Add(newAppRole);
                    return new OkObjectResult(applicationRoleViewModel);
                }
                catch (NameDuplicatedException dex)
                {
                    return new BadRequestObjectResult(dex.Message);
                }
            }
            else
            {
                return new BadRequestObjectResult(ModelState);
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody]ApplicationRoleViewModel applicationRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var appRole = _appRoleService.GetDetail(applicationRoleViewModel.Id);
                try
                {
                    appRole.UpdateApplicationRole(applicationRoleViewModel, "update");
                    appRole.NormalizedName = appRole.Name.ToUpper();
                    _appRoleService.Update(appRole);
                    return new OkObjectResult(_mapper.Map<ApplicationRole, ApplicationRoleViewModel>(appRole));
                }
                catch (NameDuplicatedException dex)
                {
                    return new BadRequestObjectResult(dex.Message);
                }
            }
            else
            {
                return new BadRequestObjectResult(ModelState);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(string id)
        {
            if (_appRoleService.GetDetail(id) == null)
                return NotFound();
            _appRoleService.Delete(id);
            return new OkResult();
        }

    }
}

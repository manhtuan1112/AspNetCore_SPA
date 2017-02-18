using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    [Route("api/[controller]")]
    public class ApplicationGroupController : Controller
    {
        private IApplicationGroupService _appGroupService;
        private IApplicationRoleService _appRoleService;
        private UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;
        public ApplicationGroupController(IApplicationRoleService appRoleService,IApplicationGroupService appGroupService, UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            _appGroupService = appGroupService;
            _appRoleService = appRoleService;
            this._userManager = userManager;
            this._mapper = mapper;
        }
        [Route("getlistpaging")]
        [HttpGet]
        public PaginationSet<ApplicationGroupViewModel> GetListPaging(int page, int pageSize, string filter = null)
        {
                int totalRow = 0;
                var model = _appGroupService.GetAll(page, pageSize, out totalRow, filter);
                IEnumerable<ApplicationGroupViewModel> modelVm = _mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);

                PaginationSet<ApplicationGroupViewModel> pagedSet = new PaginationSet<ApplicationGroupViewModel>()
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
        public IEnumerable<ApplicationGroupViewModel> GetAll()
        {
                var model = _appGroupService.GetAll();
                IEnumerable<ApplicationGroupViewModel> modelVm = _mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);
                 return modelVm;
        }

        [Route("detail/{id:long}")]
        [HttpGet]
        public IActionResult Details(long id)
        {
            if (id == 0)
            {
                return new BadRequestResult();
            }
            ApplicationGroup appGroup = _appGroupService.Detail(id);
            var appGroupViewModel = _mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(appGroup);
            if (appGroup == null)
            {
                return new NoContentResult();
            }
            var listRole = _appRoleService.GetListRoleByGroupId(appGroupViewModel.ID);
            appGroupViewModel.Roles = _mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(listRole);
            return new OkObjectResult(appGroupViewModel);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Create([FromBody] ApplicationGroupViewModel appGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppGroup = new ApplicationGroup();
                newAppGroup.UpdateApplicationGroup(appGroupViewModel);
                try
                {
                    var appGroup= _appGroupService.Add(newAppGroup);

                    //save group
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var role in appGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup()
                        {
                            GroupId = appGroup.ID,
                            RoleId = role.Id
                        });
                    }
                    _appRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID);
                    return Ok(newAppGroup);


                }
                catch (NameDuplicatedException dex)
                {
                    return new BadRequestObjectResult(appGroupViewModel);
                }

            }
            else
            {
                return new BadRequestObjectResult(appGroupViewModel);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ApplicationGroupViewModel appGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var appGroup = _appGroupService.Detail(appGroupViewModel.ID);
                try
                {
                    appGroup.UpdateApplicationGroup(appGroupViewModel);
                    _appGroupService.Update(appGroup);
                    //_appGroupService.Save();

                    //save group
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var role in appGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup()
                        {
                            GroupId = appGroup.ID,
                            RoleId = role.Id
                        });
                    }
                    _appRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID);
                  

                    //add role to user
                    var listRole = _appRoleService.GetListRoleByGroupId(appGroup.ID).ToList();
                    var listUserInGroup = _appGroupService.GetListUserByGroupId(appGroup.ID).ToList();
                    foreach (var user in listUserInGroup)
                    {
                        var listRoleName = listRole.Select(x => x.Name).ToList();
                        foreach (var roleName in listRoleName)
                        {
                         await _userManager.RemoveFromRoleAsync(user, roleName);
                         await _userManager.AddToRoleAsync(user, roleName);
                        }
                    }
                    return new OkObjectResult(appGroup);
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
        public IActionResult Delete(long id)
        {
            var appGroup = _appGroupService.Detail(id);
            if (appGroup == null) return NotFound();
             _appGroupService.Delete(id);
            return new OkResult();
        }

    }
}

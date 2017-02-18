using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sample.AspCoreEF.Common;
using Sample.AspCoreEF.Common.Exceptions;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.Infrastructure.Core;
using Samples.AspCoreEF.Infrastructure.Extensions;
using Samples.AspCoreEF.Infrastructure.Services;
using Samples.AspCoreEF.Models;
using Samples.AspCoreEF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.AspCoreEF.Controllers
{
    [Route("api/applicationUser")]
    public class ApplicationUserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IApplicationGroupService _appGroupService;
        private IApplicationRoleService _appRoleService;
        private IMapper _mapper;
        private IEmailSender _emailSender;
        private static string confirmCode;
        public ApplicationUserController(UserManager<ApplicationUser> userManager,IApplicationGroupService appGroupService,IApplicationRoleService appRoleService,IMapper mapper,IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._appGroupService = appGroupService;
            this._appRoleService = appRoleService;
            this._emailSender = emailSender;
            this._mapper = mapper;
        }

        [Route("getlistpaging")]
        [HttpGet]
        
        public PaginationSet<ApplicationUserViewModel> GetListPaging(int page, int pageSize, string filter = null)
        {
           
                int totalRow = 0;
                var model = _userManager.Users;
                IEnumerable<ApplicationUserViewModel> modelVm = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(model);

                PaginationSet<ApplicationUserViewModel> pagedSet = new PaginationSet<ApplicationUserViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };
            return pagedSet;
        }

        [Route("detail/{id}")]
        [HttpGet]        
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new NotFoundResult();
            }
            else
            {
                var applicationUserViewModel = _mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);
                var listGroup = _appGroupService.GetListGroupByUserId(applicationUserViewModel.Id);
                applicationUserViewModel.Groups = _mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(listGroup);
                return new OkObjectResult(applicationUserViewModel);
            }

        }

        [HttpGet]
        [Route("confirmation")]
        public async Task<IActionResult> ConfirmationAccount(string userId,string code) 
        {
            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(code))
            {
                return new BadRequestResult();
            }

            var user = _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new NotFoundResult();
            }
            else
            {
                if (code != confirmCode) return new NotFoundResult();
                user.Result.EmailConfirmed = true;
                await _userManager.UpdateAsync(user.Result);
                return Redirect("http://localhost:5000/admin");
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Create([FromBody] ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppUser = new ApplicationUser();
                newAppUser.UpdateUser(applicationUserViewModel);
                var userByEmail = await _userManager.FindByEmailAsync(applicationUserViewModel.Email);
                if (userByEmail != null)
                {
                    ModelState.AddModelError("Message", "Email already exists");
                    return new BadRequestObjectResult(ModelState);
                }
                var userByUserName = await _userManager.FindByNameAsync(applicationUserViewModel.UserName);
                if (userByUserName != null)
                {
                    ModelState.AddModelError("Message", "Username already exists");
                    return new BadRequestObjectResult(ModelState);
                }
                try
                {
                    newAppUser.Id = Guid.NewGuid().ToString();
                  
                    var result = await _userManager.CreateAsync(newAppUser, applicationUserViewModel.Password);
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(newAppUser);
                        confirmCode = code;
                        //var callbackUrl =  $"localhost:5000/api/applicationUser/confirmation?userId={newAppUser.Id}&code={code}";

                        var callbackUrl = Url.Action("confirmation", "api/applicationUser", new { userId = newAppUser.Id, code = code }, protocol: HttpContext.Request.Scheme);

                        await _emailSender.SendEmailAsync(applicationUserViewModel.Email, "Confirm your account",
                      $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        

                        var listAppUserGroup = new List<ApplicationUserGroup>();
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup()
                            {
                                GroupId = group.ID,
                                UserId = newAppUser.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.ID);
                            foreach (var role in listRole)
                            {
                                await _userManager.RemoveFromRoleAsync(newAppUser, role.Name);
                                await _userManager.AddToRoleAsync(newAppUser, role.Name);
                                newAppUser.DataEventRecordsRole = "dataEventRecords." + role.Name.ToLower();
                                newAppUser.SecuredFilesRole = "securedFiles." + role.Name.ToLower();
                                await _userManager.UpdateAsync(newAppUser);
                            }

                        }
                        _appGroupService.AddUserToGroups(listAppUserGroup, newAppUser.Id);
                        return new OkObjectResult(applicationUserViewModel);

                    }
                    else {
                        ModelState.AddModelError("Message", result.Errors.First().Description);
                        return new BadRequestObjectResult(ModelState);
                        
                    }
                }
                catch (NameDuplicatedException dex)
                {
                    return new BadRequestObjectResult(dex.Message);
                }
                catch (Exception ex)
                {
                    return new BadRequestObjectResult(ex.Message);
                }
            }
            else
            {
                return new BadRequestObjectResult(ModelState);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByIdAsync(applicationUserViewModel.Id);
                try
                {
                    appUser.UpdateUser(applicationUserViewModel);
                    var result = await _userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();
                       
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup()
                            {
                                GroupId = group.ID,
                                UserId = applicationUserViewModel.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.ID).ToList();
                            foreach (var role in listRole)
                            {
                                await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                                await _userManager.AddToRoleAsync(appUser, role.Name);
                                
                            }
                        }
                        _appGroupService.AddUserToGroups(listAppUserGroup, applicationUserViewModel.Id);
                        return new OkObjectResult(applicationUserViewModel);

                    }
                    else
                        return new BadRequestObjectResult(string.Join(",", result.Errors));
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
        
        public async Task<IActionResult> Delete(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null) return NotFound();
            var result = await _userManager.DeleteAsync(appUser);
            if (result.Succeeded)
                return new OkObjectResult(appUser);
            else
                return new BadRequestObjectResult(string.Join(",", result.Errors));
        }

        [Route("testSend")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> TestSendMail(string email, string subject, string message)
        {
            await MailHelper.SendEmailAsync(email, subject, message);

            return new OkResult();
        }
    }
}

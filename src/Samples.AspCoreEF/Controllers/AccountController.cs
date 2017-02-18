using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using Samples.AspCoreEF.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.Infrastructure.Extensions;
using System;
using Microsoft.AspNetCore.Cors;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Samples.AspCoreEF.Controllers
{
    [EnableCors("AllowCors"), Route("api/[controller]")]
    public class AccountController : Controller
        {
            private IMapper _mapper;
            //private SignInManager<ApplicationUser> _signInManager;
            private UserManager<ApplicationUser> _userManager;
            public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper)
            {
                this._userManager = userManager;
                //this._signInManager = signInManager;
                this._mapper = mapper;
            }

            [HttpGet]
            [Route("testmethod")]
         
            public IActionResult TestMethod()
            {
                return new OkObjectResult("Success");
            }


            // GET: api/values
            [HttpPost]
            [AllowAnonymous]
            [Route("checkaccount")]
            public async Task<IActionResult> CheckAccount(string username, string password)
            {
                // discover endpoints from metadata
                var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
                // request token
                var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
                var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(username, password, "api1");
                if (tokenResponse.IsError)
                {
                    return new UnauthorizedResult();
                }
                return new OkObjectResult(tokenResponse.Json);
            }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] ApplicationUserViewModel appUserVm)
            {
                if (ModelState.IsValid)
                {
                    var userByEmail = await _userManager.FindByEmailAsync(appUserVm.Email);
                    if (userByEmail != null)
                    {
                        ModelState.AddModelError("email", "Email already exists");
                        return new BadRequestResult();
                    }
                    var userByUserName = await _userManager.FindByNameAsync(appUserVm.UserName);
                    if (userByUserName != null)
                    {
                        ModelState.AddModelError("email", "Username already exists");
                        return new BadRequestResult();
                    }
                    var newUser = new ApplicationUser();
                    newUser.UpdateUser(appUserVm);
                    try
                    {
                        newUser.Id = Guid.NewGuid().ToString();
                        var result = await _userManager.CreateAsync(newUser, appUserVm.Password);
                        if (result.Succeeded)
                        {
                            return new OkObjectResult(appUserVm);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new BadRequestResult();
                    }
                }
                return new BadRequestResult();
            }
        }
    }

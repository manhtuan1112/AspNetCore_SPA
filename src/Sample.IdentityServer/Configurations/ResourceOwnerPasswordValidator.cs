using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Sample.IdentityServer.Models;
using static IdentityModel.OidcConstants;

namespace Sample.IdentityServer.Configurations
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //Using Dapper
            using (IDbConnection db = new SqlConnection("Server=TUANDUONG\\SQLEXPRESS;Database=DapperTest;User ID=sa;Password=12345678;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                var user = db.Query<User>("select * from Accounts where UserName=@UserName and Password=@Pass",
                    new { UserName = context.UserName, Pass = context.Password }).SingleOrDefault<User>();

                if (user == null)
                {
                    context.Result = new GrantValidationResult(TokenErrors.InvalidRequest, "Username Or Password is incorrect");
                    return Task.FromResult(0);
                }
                context.Result = new GrantValidationResult(user.Id.ToString(), "password");
                return Task.FromResult(0);
            }
        }
    }
}

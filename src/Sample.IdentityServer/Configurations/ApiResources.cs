using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.IdentityServer.Configurations
{
    public class ApiResources
    {
        public static List<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource()
                {
                    Name="myApi",
                    Description="Some description for myApi"
                }
            };
        }
    }
}

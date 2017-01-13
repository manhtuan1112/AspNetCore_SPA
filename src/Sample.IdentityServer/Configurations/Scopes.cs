using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.IdentityServer.Configurations
{
    public class Scopes
    {
        public static List<Scope> GetApiResources()
        {
            return new List<Scope>()
            {
                new Scope()
                {
                    Name="myApi",
                    Description="Some description for myApi"
                }
            };
        }
    }
}

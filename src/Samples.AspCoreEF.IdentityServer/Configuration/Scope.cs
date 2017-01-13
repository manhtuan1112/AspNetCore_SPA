using IdentityServer4.Models;
using System.Collections;
using System.Collections.Generic;

namespace Samples.AspCoreEF.IdentityServer.Configuration
{
    public class Scopes
    {
        public static IEnumerable Get()
        {
            return new List<Scope>
            {
                new Scope()
                {
                    Name="api1",
                    DisplayName="API 1",
                    Description="API 1 feature and data",
                }
            };
        }
    }
}
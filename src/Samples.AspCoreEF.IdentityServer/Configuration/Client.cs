using IdentityServer4.Models;
using System.Collections;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace Samples.AspCoreEF.IdentityServer.Configuration
{
    public class Clients
    {
        public static IEnumerable Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Resource owner Client",
                    ClientId = "roclient",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("madeupsecret".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowedScopes = new List<string>
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        "api1"
                    }
                }
            };
        }
    }
}
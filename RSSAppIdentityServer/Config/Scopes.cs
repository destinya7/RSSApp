using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace RSSAppIdentityServer.Config
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                new Scope
                {
                    Enabled = true,
                    Name = "roles",
                    DisplayName  = "Roles",
                    Description = "The roles you belong to.",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                },
                new Scope
                {
                    Enabled = true,
                    Emphasize = false,
                    DisplayName = "RSS Application API",
                    Name = "rssapplicationapi",
                    Description = "Access to RSS Application API",
                    Type = ScopeType.Resource
                }
            };
        }
    }
}
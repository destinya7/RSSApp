using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace RSSAppIdentityServer.Config
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "User1",
                    Password = "secret",
                    Subject = "1",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "User"),
                        new Claim(Constants.ClaimTypes.FamilyName, "One"),
                        new Claim(Constants.ClaimTypes.Role, "WebWriteUser"),
                        new Claim(Constants.ClaimTypes.Role, "WebReadUser")
                    }
                }
            };
        }
    }
}
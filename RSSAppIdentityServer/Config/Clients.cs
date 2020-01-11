using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace RSSAppIdentityServer.Config
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientId = "mvc",
                    ClientName = "RSSApplication Web Client",
                    Flow = Flows.Hybrid,
                    RequireConsent = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44323/"
                    },
                    AllowAccessToAllScopes = true
                }
            };
        }
    }
}
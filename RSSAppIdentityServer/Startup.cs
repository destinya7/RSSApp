using System;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using Microsoft.Owin;
using Owin;
using RSSAppIdentityServer.Config;

[assembly: OwinStartup(typeof(RSSAppIdentityServer.Startup))]

namespace RSSAppIdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idServerApp =>
            {
                idServerApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "RSSApp IdentityServer",
                    SigningCertificate = LoadCertificate(),

                    Factory = new IdentityServerServiceFactory()
                        .UseInMemoryClients(Clients.Get())
                        .UseInMemoryUsers(Users.Get())
                        .UseInMemoryScopes(Scopes.Get())
                });
            });
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\Certificates\idsrv3test.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}

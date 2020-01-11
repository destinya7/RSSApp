using IdentityServer3.Core;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json.Linq;
using Owin;
using RSSAppWebClient.Helpers;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(RSSAppWebClient.Startup))]

namespace RSSAppWebClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "unique_userid";
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:44341/identity",
                ClientId = "mvc",
                Scope = "openid profile roles rssapplicationapi",
                RedirectUri = "https://localhost:44323/",
                ResponseType = "code id_token token",
                SignInAsAuthenticationType = "Cookies",

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var issuerUri = n.AuthenticationTicket.Identity
                            .FindFirst(Constants.ClaimTypes.Issuer).Value;
                        var userInfoClient = new UserInfoClient();
                        var userInfo = await userInfoClient.CallUserInfoEndpoint(
                            n.ProtocolMessage.AccessToken);

                        var givenNameClaim = new Claim(
                            Constants.ClaimTypes.GivenName,
                            userInfo.Value<string>("given_name"));
                        var familyNameClaim = new Claim(
                            Constants.ClaimTypes.FamilyName,
                            userInfo.Value<string>("family_name"));
                        var subClaim = new Claim(
                            Constants.ClaimTypes.Subject,
                            userInfo.Value<string>("sub"));
                        var roles = userInfo.Value<JArray>("role").ToList();

                        var newIdentity = new ClaimsIdentity(
                            n.AuthenticationTicket.Identity.AuthenticationType,
                            Constants.ClaimTypes.GivenName,
                            Constants.ClaimTypes.Role);

                        newIdentity.AddClaim(givenNameClaim);
                        newIdentity.AddClaim(familyNameClaim);
                        newIdentity.AddClaim(subClaim);

                        foreach (var role in roles)
                        {
                            newIdentity.AddClaim(new Claim(
                                Constants.ClaimTypes.Role,
                                role.ToString()));
                        }

                        newIdentity.AddClaim(new Claim(
                            "unique_userid",
                            issuerUri + "_" + subClaim.Value));
                        newIdentity.AddClaim(new Claim(
                            "access_token", n.ProtocolMessage.AccessToken));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            newIdentity,
                            n.AuthenticationTicket.Properties);
                    }
                }
            });
        }
    }
}

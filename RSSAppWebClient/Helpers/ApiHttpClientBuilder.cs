using RSSAppWebClient.Config;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;

namespace RSSAppWebClient.Helpers
{
    public class ApiHttpClientBuilder : IApiHttpClientBuilder
    {
        private readonly IAppConfigManager _configurationManager;

        public ApiHttpClientBuilder(IAppConfigManager configurationManager)
        {
            this._configurationManager = configurationManager;
        }

        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient();

            var token = (HttpContext.Current.User.Identity as ClaimsIdentity)
                .FindFirst("access_token").Value;

            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            client.BaseAddress = new Uri(_configurationManager.GetApiUri());

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
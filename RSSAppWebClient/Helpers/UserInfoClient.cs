using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RSSAppWebClient.Helpers
{
    public class UserInfoClient
    {
        public async Task<JObject> CallUserInfoEndpoint(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync(
                "https://localhost:44341/identity/connect/userinfo");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JObject.Parse(json);
            }

            return null;
        }
    }
}
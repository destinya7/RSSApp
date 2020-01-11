using System.Net.Http;

namespace RSSAppWebClient.Helpers
{
    public interface IApiHttpClientBuilder
    {
        HttpClient GetClient();
    }
}
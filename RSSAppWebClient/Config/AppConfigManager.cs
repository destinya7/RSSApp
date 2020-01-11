using System.Configuration;

namespace RSSAppWebClient.Config
{
    public class AppConfigManager : IAppConfigManager
    {
        private const string AppUri = "RSS_API_URL";

        public string GetApiUri()
        {
            return GetEnvironmentVariable(AppUri);
        }

        private string GetEnvironmentVariable(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
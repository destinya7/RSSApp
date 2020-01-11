using Autofac;
using Autofac.Integration.Mvc;
using RSSAppWebClient.Config;
using RSSAppWebClient.Helpers;
using System.Web.Mvc;

namespace RSSAppWebClient
{
    public class ContainerConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<AppConfigManager>().As<IAppConfigManager>();
            builder.RegisterType<ApiHttpClientBuilder>().As<IApiHttpClientBuilder>();

            var container = builder.Build();
            DependencyResolver.SetResolver(
                new AutofacDependencyResolver(container));
        }
    }
}
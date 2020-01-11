using Autofac;
using Autofac.Integration.WebApi;
using RSSAppApi.Config;
using RSSAppApi.Factories;
using RSSAppApi.Services;
using System.Web.Http;

namespace RSSAppApi
{
    public class ContainerConfig
    {
        public static void RegisterContainer(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            builder.RegisterType<AppConfigManager>().As<IAppConfigManager>();
            builder.RegisterType<WindowsEventLoggerService>().As<ILoggerService>();
            builder.RegisterType<WorkerQueuePublisher>()
               .As<IWorkerQueuePublisher>()
               .SingleInstance();
            builder.RegisterType<MessageQueueConsumer>()
                .As<IMessageQueueConsumer>()
                .SingleInstance();

            builder.RegisterType<ArticleFactory>().As<IArticleFactory>();
            builder.RegisterType<ChannelFactory>().As<IChannelFactory>();

            var container = builder.Build();
            configuration.DependencyResolver =
                new AutofacWebApiDependencyResolver(container);
        }
    }
}
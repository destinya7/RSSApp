using Microsoft.AspNet.SignalR;
using RSSAppApi.Hubs;
using RSSAppApi.Services;
using System.Diagnostics;
using System.Text;
using System.Web.Http;

namespace RSSAppApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ContainerConfig.RegisterContainer(GlobalConfiguration.Configuration);

            var queuePublisher = GlobalConfiguration.Configuration
                .DependencyResolver.GetService(typeof(IWorkerQueuePublisher))
                as IWorkerQueuePublisher;

            var mqConsumer = GlobalConfiguration.Configuration
                .DependencyResolver.GetService(typeof(IMessageQueueConsumer))
                as IMessageQueueConsumer;

            queuePublisher.SetupConnection();

            mqConsumer.SetupConnection();
            mqConsumer.Consumer.Received += Consumer_Received;
            mqConsumer.StartListening();
        }

        private void Consumer_Received(object sender, RabbitMQ.Client.Events.BasicDeliverEventArgs e)
        {
            Debug.Write("Message Received: " + e.Body);
            var message = Encoding.UTF8.GetString(e.Body);
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.NewArticle(message);
        }
    }
}

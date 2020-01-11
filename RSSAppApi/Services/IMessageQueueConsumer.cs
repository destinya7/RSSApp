using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RSSAppApi.Services
{
    public interface IMessageQueueConsumer
    {
        void SetupConnection();

        void StartListening();

        void CloseConnection();

        EventingBasicConsumer Consumer { get; }

        IModel Channel { get; }

        IConnection Connection { get; }
    }
}
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RSSAppApi.Config;
using System;

namespace RSSAppApi.Services
{
    public class MessageQueueConsumer : IMessageQueueConsumer
    {
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;

        ILoggerService _logger;
        IAppConfigManager _configurationManager;

        public MessageQueueConsumer(
            ILoggerService logger,
            IAppConfigManager configurationManager
        )
        {
            _logger = logger;
            _configurationManager = configurationManager;
        }

        public void SetupConnection()
        {
            var credential =
                _configurationManager.GetWorkerQueueEnvironmentVariable();

            var factory = new ConnectionFactory()
            {
                HostName = credential.Hostname,
                UserName = credential.Username,
                Password = credential.Password
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.QueueDeclare(queue: credential.QueueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;

                _consumer = new EventingBasicConsumer(_channel);
            }
            catch (Exception e)
            {
                _logger.Debug(e.ToString());
            }
        }

        public void StartListening()
        {
            _channel.BasicConsume(queue: "message_queue",
                                  autoAck: true,
                                  consumer: _consumer);
        }

        public void CloseConnection()
        {
            if (!_channel.IsClosed) _channel.Close();

            if (_connection != null) _connection.Close();
        }

        public EventingBasicConsumer Consumer => _consumer;

        public IModel Channel => _channel;

        public IConnection Connection => _connection;
    }
}
namespace AtlasBotCommander.Communication
{
    using System;
    using System.Text;
    using Interfaces;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class MessageHub : IAtlasHub
    {
        private IConnectionFactory _factory;

        private IConnection _connection;

        public void Setup()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void Connect()
        {
            _connection = _factory.CreateConnection();
        }

        public void Subscribe()
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "logs",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };

                channel.BasicConsume(
                    queue: "logs",
                    autoAck: true,
                    consumer: consumer);
            }
        }
    }
}

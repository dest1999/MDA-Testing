using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Messaging
{
    public class Consumer
    {
        private string queueName;
        private string hostName;
        private IConnection connection;
        private IModel channel;

        public Consumer(string QueueName, string HostName)
        {
            queueName = QueueName;
            hostName = HostName;
            var factory = new ConnectionFactory()
            {
                HostName = hostName
            };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void Receive (EventHandler<BasicDeliverEventArgs> receiveCallback)
        {
            channel.ExchangeDeclare("direct_exchange", "direct");
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, "direct_exchange", queueName);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += receiveCallback;
            channel.BasicConsume(queueName, true, consumer);
        }

    }
}
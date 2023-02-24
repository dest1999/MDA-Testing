using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
    public class Producer
    {
        private string queueName;
        private string hostName;
        private IConnection connection;
        //private IModel channel;

        public Producer(string QueueName, string HostName)
        {
            queueName = QueueName;
            hostName = HostName;
            var factory = new ConnectionFactory()
            {
                HostName = hostName
            };
            connection = factory.CreateConnection();
            //channel = connection.CreateModel();
        }

        public void Send(string Message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = hostName,
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("direct_exchange", "direct", false, false, null);

            var body = Encoding.UTF8.GetBytes(Message);

            channel.BasicPublish("direct_exchange", queueName, null, body);
        }

    }
}

using Messaging;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace RestaurantNotification
{
    public class Worker : BackgroundService
    {
        private readonly Consumer consumer;

        public Worker()
        {
            consumer = new Consumer("BookingNotification", "localhost");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            consumer.Receive((sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received {message}");
            });
        }


    }
}
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace RestaurantNotification
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) => 
            {
                services.AddHostedService<Worker>();
            });

    }
}
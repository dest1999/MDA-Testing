using System.Diagnostics;

namespace Restaurant
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Restaurant restaurant = new(5);

            while (true)
            {
                await Task.Delay(10_000);
                Console.WriteLine("Бронирование столика, метод Main");

                Stopwatch stopWatch = new();
                stopWatch.Start();

                restaurant.BookFreeTableAsync(3);

                stopWatch.Stop();
                var ts = stopWatch.Elapsed;
                Console.WriteLine($"{ts.Seconds}:{ts.Milliseconds}");
            }


            
        }
    }
}
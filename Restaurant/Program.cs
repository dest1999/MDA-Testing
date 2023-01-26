using System.Diagnostics;

namespace Restaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Restaurant restaurant = new(5);

            while (true)
            {
                Console.WriteLine("\n1 - async, 2 - sync");
                if (!int.TryParse(Console.ReadLine(), out var choice) && choice is not (1 or 2) )
                {
                    Console.Write("input 1 or 2: ");
                    continue;
                }

                Stopwatch stopWatch = new();
                stopWatch.Start();

                if (choice == 1)
                {
                    restaurant.BookFreeTableAsync(3);
                }
                else
                {
                    restaurant.BookFreeTable(3);
                }

                stopWatch.Stop();
                var ts = stopWatch.Elapsed;
                Console.WriteLine($"{ts.Seconds}:{ts.Milliseconds}");
            }


            
        }
    }
}
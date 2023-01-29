using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Notification
    {
        public static void SendNotifyAsync(string Message)
        {
            Task.Run(() =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Message from Notification Class:\n{Message}");
                Console.ResetColor();
            });
        }
    }
}

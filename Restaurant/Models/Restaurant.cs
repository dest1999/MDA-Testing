using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Restaurant
    {
        private List<Table> tables = new();

        private int managerSpeed = 5;

        public Restaurant()
        {

        }
        public Restaurant(int TableCount)
        {
            for (int i = 1; i <= TableCount; i++)
            {
                tables.Add(new Table(i));
            }
        }

        public void BookFreeTable(int countOfPersons)
        {
            Console.WriteLine("Hi, wait for table confirm");

            Thread.Sleep(1000 * managerSpeed);
            var table = tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.isFree);
            table?.SetState(false);

            if (table is null)
            {
                Console.WriteLine("All tables are busy, sorry");
            } else
            {
                Console.WriteLine($"OK, table No {table.Id}");
            }
        }

        public void BookFreeTableAsync(int countOfPersons)
        {
            Console.WriteLine("Hi, we will send a message");
            Task.Run(async () =>
            {
                var table = tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.isFree);
                await Task.Delay(1000 * managerSpeed);
                table?.SetState(false);

                if (table is null)
                {
                    Console.WriteLine("MESSAGE: All tables are busy, sorry");
                }
                else
                {
                    Console.WriteLine($"MESSAGE: OK, table No {table.Id}");
                }
            });
        }
    }
}

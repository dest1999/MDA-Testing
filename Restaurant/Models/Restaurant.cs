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
        private static readonly object locker = new();
        private int managerSlow = 5;

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

            var table = tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == State.Free );
            Thread.Sleep(1000 * managerSlow);
            table?.SetState(State.Booked);

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
                await Task.Delay(1000 * managerSlow);
                lock (locker)
                {
                    var table = tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == State.Free);
                    table?.SetState(State.Booked);
                    if (table is null)
                    {
                        Console.WriteLine("MESSAGE: All tables are busy, sorry");
                    }
                    else
                    {
                        Console.WriteLine($"MESSAGE: OK, table No {table.Id}");
                    }

                }

            });
        }

        public void UnBookTable(int tableId)
        {
            var table = tables.FirstOrDefault(t => t.Id == tableId);
            Thread.Sleep(1000 * managerSlow);

            if (table is not null)
            {
                if (table.State == State.Booked)
                {
                    table.SetState(State.Free);
                    Console.WriteLine($"Table {table.Id} is free");
                }
                else
                {
                    Console.WriteLine($"Table {table.Id} already free");
                }
            }
            else
            {
                Console.WriteLine("Table is not exist");
            }
        }

        public void UnBookTableAsync(int tableId)
        {
            Task.Run(async () =>
            {
                var table = tables.FirstOrDefault(t => t.Id == tableId);
                await Task.Delay(1000 * managerSlow);

                if (table is not null)
                {
                    if (table.State == State.Booked)
                    {
                        table.SetState(State.Free);
                        Console.WriteLine($"Table {table.Id} is free");
                    }
                    else
                    {
                        Console.WriteLine($"Table {table.Id} already free");
                    }
                }
                else
                {
                    Console.WriteLine("Table is not exist");
                }
            });


        }


    }
}

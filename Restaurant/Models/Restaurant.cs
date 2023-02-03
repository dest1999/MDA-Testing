using Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Restaurant
    {
        private readonly List<Table> tables = new();
        private static readonly object locker = new();
        private readonly int managerSlow = 5;
        private readonly TimeSpan AutoUnBookTimeSpan = TimeSpan.FromSeconds(20);
        private bool isAutoUnBookingRunning = false;
        private Producer producer = new("BookingNotification", "localhost");

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
            Notification.SendNotifyAsync("Hi, wait for table confirm");

            var table = tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == State.Free );
            Thread.Sleep(1000 * managerSlow);
            table?.SetState(State.Booked);

            if (table is null)
            {
                Notification.SendNotifyAsync("All tables are busy, sorry");
            }
            else
            {
                Notification.SendNotifyAsync($"OK, table No {table.Id}");
                AutoUnBookTableAsync();
            }
        }

        public void BookFreeTableAsync(int countOfPersons)
        {
            Notification.SendNotifyAsync("Hi, we will send a message");
            Task.Run(async () =>
            {
                await Task.Delay(1000 * managerSlow);
                lock (locker)
                {
                    var table = tables.FirstOrDefault(t => t.SeatsCount >= countOfPersons && t.State == State.Free);
                    table?.SetState(State.Booked);

                    if (table is null)
                    {
                        producer.Send("MESSAGE: All tables are busy, sorry");
                    }
                    else
                    {
                        producer.Send($"MESSAGE: OK, table No {table.Id}");
                        AutoUnBookTableAsync();
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
                    Notification.SendNotifyAsync($"Table {table.Id} is free");
                }
                else
                {
                    Notification.SendNotifyAsync($"Table {table.Id} already free");
                }
            }
            else
            {
                Notification.SendNotifyAsync("Table is not exist");
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
                        Notification.SendNotifyAsync($"Table {table.Id} is free");
                    }
                    else
                    {
                        Notification.SendNotifyAsync($"Table {table.Id} already free");
                    }
                }
                else
                {
                    Notification.SendNotifyAsync("Table is not exist");
                }
            });


        }

        private async void AutoUnBookTableAsync()
        {
            if (!isAutoUnBookingRunning)
            {
                isAutoUnBookingRunning = true;
                while (true)
                {
                    await Task.Delay(AutoUnBookTimeSpan);
                    foreach (var item in tables)
                    {
                        item.SetState(State.Free);
                    }
                    Notification.SendNotifyAsync("Run AutoUnbooking");
                }
            }
        }

    }
}

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

        public Restaurant(int TableCount)
        {
            for (int i = 1; i <= TableCount; i++)
            {
                tables.Add(new Table(i));
            }
        }

    }
}

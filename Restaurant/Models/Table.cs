using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    internal class Table
    {
        private static Random rnd = new Random();
        public bool isFree { get; private set; }
        public int Id { get; private set; }
        public int SeatsCount { get; private set; }

        public Table(int Id)
        {
            this.Id = Id;
            isFree = true;
            SeatsCount = rnd.Next(2, 7);
        }

        public bool SetState(bool state)
        {
            if (state == isFree)
            {
                return false;
            }
            isFree = state;
            return true;
        }

    }
}

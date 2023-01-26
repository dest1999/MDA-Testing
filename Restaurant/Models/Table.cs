using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public enum State
    {
        Free = 0,
        Booked = 1,
    }

    internal class Table
    {
        private static Random rnd = new Random();
        public State State { get; private set; }
        public int Id { get; private set; }
        public int SeatsCount { get; private set; }

        public Table(int Id)
        {
            this.Id = Id;
            State = State.Free;
            SeatsCount = rnd.Next(2, 7);
            Console.Write($"{SeatsCount} ");//for debug
        }

        public bool SetState(State state)
        {
            if (state == State)
            {
                return false;
            }
            State = state;
            return true;
        }

    }
}

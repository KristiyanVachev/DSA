using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Unit> units = new List<Unit>
            {
                new Unit {Name = "B", Type = "Poop", Attack = "20"},
                //new Unit {Name = "D", Type = "Poop", Attack = "20"},
                //new Unit {Name = "E", Type = "Poop", Attack = "20"},
                //new Unit {Name = "F", Type = "Poop", Attack = "20"},
                //new Unit {Name = "H", Type = "Poop", Attack = "20"},
                //new Unit {Name = "K", Type = "Poop", Attack = "20"},
                //new Unit {Name = "L", Type = "Poop", Attack = "20"},
                //new Unit {Name = "M", Type = "Poop", Attack = "20"}
            };

            Quicksort(units, new Unit { Name = "I", Type = "Pet", Attack = "40" });
            //1
        }

        public static void Quicksort(IList<Unit> elements, Unit unit)
        {
            int left = 0;
            int right = elements.Count - 1;

            int middle = -1;
            Unit pivot;


            while (right - left > 1)
            {
                if (right - left == 1)
                {
                    break;
                }

                middle = (left + right) / 2;
                pivot = elements[middle];

                int bigger = string.Compare(unit.Name, pivot.Name, StringComparison.Ordinal);

                if (bigger < 0)
                {
                    //less than
                    right = middle;
                }
                else if (bigger > 0)
                {
                    //more
                    left = middle;
                }
                else
                {
                    //just right
                    //exists
                }
            }

            Console.WriteLine(left);
            Console.WriteLine(right);

            //Console.WriteLine(middle);

        }
    }


    internal class Unit
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Attack { get; set; }
    }
}

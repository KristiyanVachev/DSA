using System;
using System.Collections.Generic;

namespace Test
{
    class FindPlace
    {
        static void Main()
        {
            IList<Unit> input = new List<Unit>
            {
                new Unit {Name = "F", Type = "None", Attack = "20"},
                new Unit {Name = "L", Type = "None", Attack = "20"},
                new Unit {Name = "D", Type = "None", Attack = "20"},
                new Unit {Name = "E", Type = "None", Attack = "20"},
                new Unit {Name = "B", Type = "None", Attack = "20"},
                new Unit {Name = "E", Type = "None", Attack = "20"},
                new Unit {Name = "M", Type = "None", Attack = "20"},
                new Unit {Name = "H", Type = "None", Attack = "20"}
            };

            IList<Unit> units = new List<Unit>();

            foreach (var inputUnit in input)
            {
                int index = Find(units, inputUnit);

                if (index == units.Count)
                {
                    units.Add(inputUnit);
                }
                else if (index < 0)
                {
                    Console.WriteLine("Exists");
                }
                else
                {
                    units.Insert(index, inputUnit);
                }

            }

            foreach (var unit in units)
            {
                Console.WriteLine(unit.Name);
            }

        }

        public static int Find(IList<Unit> elements, Unit unit)
        {
            if (elements.Count == 0)
            {
                return 0;
            }

            int left = 0;
            int right = elements.Count - 1;

            while (right - left > 1)
            {
                if (right - left == 1)
                {
                    break;
                }

                int middle = (left + right) / 2;
                Unit pivot = elements[middle];

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
                    //already exists
                    return -1;
                }
            }

            if (left == 0)
            {
                if (string.Compare(unit.Name, elements[0].Name, StringComparison.Ordinal) < 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            if (right == elements.Count - 1)
            {
                if (string.Compare(unit.Name, elements[elements.Count - 1].Name, StringComparison.Ordinal) < 0)
                {
                    return elements.Count - 1;
                }
                else
                {
                    return elements.Count;
                }
            }

            return left + 1;
        }
    }


    internal class Unit
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Attack { get; set; }
    }
}

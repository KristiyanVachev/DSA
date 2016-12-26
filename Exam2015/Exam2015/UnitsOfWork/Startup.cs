using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitsOfWork
{
    class Startup
    {
        static void Main()
        {
            string currCommand = Console.ReadLine();
            var units = new List<Unit>();
            var finalResult = new StringBuilder();
            var unitsByAttack = new SortedSet<Unit>();

            while (!currCommand.Contains("end"))
            {
                string[] command = currCommand.Split(' ');

                switch (command[0])
                {
                    case "add":
                        string addResult = Add(units, unitsByAttack, command);
                        finalResult.AppendLine(addResult);
                        break;

                    case "remove":
                        if (Remove(units, command[1]))
                        {
                            finalResult.AppendLine("SUCCESS: " + command[1] + " removed!");
                        }
                        else
                        {
                            finalResult.AppendLine("FAIL: " + command[1] + " could not be found!");
                        }
                        break;

                    case "find":
                        var selected =
                            units.Where(x => x.Type == command[1])
                                .OrderByDescending(x => x.Attack)
                                .ThenBy(x => x.Name)
                                .ToList();

                        finalResult.AppendLine(Result(selected));

                        break;

                    case "power":
                        var top = unitsByAttack.Take(int.Parse(command[1]));

                        finalResult.AppendLine(Result(top));

                        break;
                }

                currCommand = Console.ReadLine();
            }

            Console.WriteLine(finalResult.ToString());
        }

        static string Add(IList<Unit> units, SortedSet<Unit> unitsByAttack, string[] commands)
        {
            int index = Find(units, commands[1]);

            if (index < 0)
            {
                return "FAIL: " + commands[1] + " already exists!";
            }

            var newUnit = new Unit { Name = commands[1], Type = commands[2], Attack = commands[3] };

            if (index == units.Count)
            {
                units.Add(newUnit);
            }

            else
            {
                units.Insert(index, newUnit);
            }

            unitsByAttack.Add(newUnit);

            return "SUCCESS: " + commands[1] + " added!";
        }

        static bool Contains(IList<Unit> units, string name)
        {
            foreach (var unit in units)
            {
                if (unit.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        static void Add(IList<Unit> units, Unit unit)
        {

        }

        static bool Remove(IList<Unit> units, string name)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].Name == name)
                {
                    units.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        static string Result(IEnumerable<Unit> selected)
        {
            var formatedUnits = new List<string>();

            foreach (var unit in selected)
            {
                string result = "";

                result += unit.Name;
                result += "[" + unit.Type + "]";
                result += "(" + unit.Attack + ")";

                formatedUnits.Add(result);
            }

            return "RESULT: " + string.Join(", ", formatedUnits);
        }

        public static int Find(IList<Unit> elements, string name)
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

                int bigger = string.Compare(name, pivot.Name, StringComparison.Ordinal);

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
                if (string.Compare(name, elements[0].Name, StringComparison.Ordinal) < 0)
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
                if (string.Compare(name, elements[elements.Count - 1].Name, StringComparison.Ordinal) < 0)
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

}

internal class Unit : IComparable<Unit>
{
    public string Name { get; set; }

    public string Type { get; set; }

    public string Attack { get; set; }

    public int CompareTo(Unit other)
    {
        var result = this.Attack.CompareTo(other.Attack) * -1;
        if (result == 0)
        {
            result = this.Name.CompareTo(other.Name);
        }

        return result;
    }
}


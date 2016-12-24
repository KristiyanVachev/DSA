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
            List<Unit> units = new List<Unit>();
            var finalResult = new StringBuilder();

            while (!currCommand.Contains("end"))
            {
                string[] command = currCommand.Split(' ');

                switch (command[0])
                {
                    case "add":
                        if (!Contains(units, command[1]))
                        {
                            units.Add(new Unit { Name = command[1], Type = command[2], Attack = command[3] });
                            finalResult.AppendLine("SUCCESS: " + command[1] + " added!");
                        }
                        else
                        {
                            finalResult.AppendLine("FAIL: " + command[1] + " already exists!");
                        }
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
                        var top = units.OrderByDescending(x => x.Attack).Take(int.Parse(command[1])).ToList();

                        finalResult.AppendLine(Result(top));

                        break;
                }

                currCommand = Console.ReadLine();
            }

            Console.WriteLine(finalResult.ToString());
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

        static string Result(IList<Unit> selected)
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

        public static void Quicksort(IList<Unit> elements, Unit unit)
        {
            int left = 0;
            int right = elements.Count - 1;

            var pivot = elements[(left + right) / 2];

            while (left < right)
            {
                int n = string.Compare(pivot.Name, unit.Name, StringComparison.Ordinal);

                if (n < 0)
                {
                    
                }
                else if (n > 0)
                {
                    
                }
                else
                {
                    //0
                }
            }

        }

    }

    internal class Unit
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Attack { get; set; }
    }


}

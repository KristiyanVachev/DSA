using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitsOfWork
{
    class Startup
    {
        static void Main()
        {
            string currCommand = Console.ReadLine();

            var unitsByName = new Dictionary<string, Unit>();
            var unitsByAttack = new SortedSet<Unit>();
            var unitsByType = new Dictionary<string, SortedSet<Unit>>();

            var finalResult = new StringBuilder();


            while (!currCommand.Contains("end"))
            {
                string[] command = currCommand.Split(' ');

                switch (command[0])
                {
                    case "add":
                        string addResult = Add(unitsByName, unitsByType, unitsByAttack, command);
                        finalResult.AppendLine(addResult);
                        break;

                    case "remove":
                        if (Remove(unitsByName, unitsByType, unitsByAttack, command[1]))
                        {
                            finalResult.AppendLine("SUCCESS: " + command[1] + " removed!");
                        }
                        else
                        {
                            finalResult.AppendLine("FAIL: " + command[1] + " could not be found!");
                        }
                        break;

                    case "find":

                        if (!unitsByType.ContainsKey(command[1]))
                        {
                            finalResult.AppendLine("RESULT:");
                        }
                        else
                        {
                            var selected = unitsByType[command[1]].Take(10);
                            finalResult.AppendLine(Output(selected));
                        }


                        break;

                    case "power":
                        var top = unitsByAttack.Take(int.Parse(command[1]));

                        finalResult.AppendLine(Output(top));

                        break;
                }

                currCommand = Console.ReadLine();
            }

            Console.WriteLine(finalResult.ToString());
        }

        static string Add(IDictionary<string, Unit> unitsByName, IDictionary<string, SortedSet<Unit>> unitsByType, SortedSet<Unit> unitsByAttack, string[] commands)
        {
            if (unitsByName.ContainsKey(commands[1]))
            {
                return "FAIL: " + commands[1] + " already exists!";
            }

            var newUnit = new Unit { Name = commands[1], Type = commands[2], Attack = commands[3] };

            unitsByName[commands[1]] = newUnit;

            if (!unitsByType.ContainsKey(commands[2]))
            {
                unitsByType[commands[2]] = new SortedSet<Unit>();
            }

            unitsByType[commands[2]].Add(newUnit);

            unitsByAttack.Add(newUnit);

            return "SUCCESS: " + commands[1] + " added!";
        }
       
        static bool Remove(IDictionary<string, Unit> unitsByName, IDictionary<string, SortedSet<Unit>> unitsByType, SortedSet<Unit> unitsByAttack, string name)
        {
            if (!unitsByName.ContainsKey(name))
            {
                return false;
            }

            var unitToRemove = unitsByName[name];

            unitsByName.Remove(name);

            unitsByType[unitToRemove.Type].Remove(unitToRemove);

            unitsByAttack.Remove(unitToRemove);

            return true;

        }

        static string Output(IEnumerable<Unit> selected)
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

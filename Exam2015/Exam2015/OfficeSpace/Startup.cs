using System;
using System.Collections.Generic;
using System.Linq;

namespace OfficeSpace
{
    public class Startup
    {
        static int[] calcTimes = new int[50];

        public static void Main()
        {
            int tasks = int.Parse(Console.ReadLine());
            var times = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            List<int>[] dependencies = new List<int>[tasks];

            for (int i = 0; i < tasks; i++)
            {
                dependencies[i] = Console.ReadLine().Split(' ').Select(x => int.Parse(x) - 1).ToList();
            }

            int max = int.MinValue;
            int taskTime;

            for (int i = 0; i < times.Length; i++)
            {
                taskTime = GetTime(i, times, dependencies, new bool[50]);

                if (taskTime == -1)
                {
                    Console.WriteLine(-1);
                    return;
                }

                if (taskTime > max)
                {
                    max = taskTime;
                }
            }

            Console.WriteLine(max);
        }

        static int GetTime(int index, int[] times, List<int>[] dependencies, bool[] walkedTrough)
        {
            if (walkedTrough[index])
            {
                return -1;
            }

            if (dependencies[index].Count == 1 && dependencies[index][0] == -1)
            {
                return times[index];
            }

            int maxDepTime = 0;
            int currDepTime;

            foreach (var depId in dependencies[index])
            {
                if (calcTimes[depId] != 0)
                {
                    currDepTime = calcTimes[depId];
                }
                else
                {
                    walkedTrough[index] = true;
                    currDepTime = GetTime(depId, times, dependencies, walkedTrough);
                    calcTimes[depId] = currDepTime;
                }

                if (currDepTime == -1)
                {
                    return -1;
                }

                if (currDepTime > maxDepTime)
                {
                    maxDepTime = currDepTime;
                }
            }

            return maxDepTime + times[index];
        }
    }
}

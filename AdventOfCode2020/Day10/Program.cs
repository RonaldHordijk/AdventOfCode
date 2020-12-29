using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            var values = lines.Select(l => Int64.Parse(l)).ToList();

            values.Add(0);
            values.Add(values.Max() + 3);
            values.Sort();

            var jump1 = CountJumps(values, 1);
            var jump3 = CountJumps(values, 3);

            Console.WriteLine($"diff of one {jump1}, diff of three {jump3}, mult = {jump1 * jump3}");

            var grps = SplitOnThrees(values);
            long count = 1;
            foreach(var sub in grps)
            {
                var l = CountPaths(sub);

                count *= l;
            }


            //var count = CountPaths(values);
            Console.WriteLine($"NrPaths = {count}");

        }

        private static List<List<long>> SplitOnThrees(List<long> values)
        {
            var res = new List<List<long>>();

            var work = new List<long>();
            for (int i = 0; i<values.Count -1;i++)
            {
                work.Add(values[i]);
                if (values[i+1] - values[i] == 3)
                {
                    res.Add(work);
                    work = new List<long>();
                }
            }

            work.Add(values[^1]);
            res.Add(work);

            return res;
        }

        private static int CountJumps(List<long> values, int jump)
        {
            int count = 0;

            for (int i = 0; i < values.Count - 1; i++)
            {
                if (values[i + 1] - values[i] == jump)
                    count++;
            }

            return count;
        }

        private static int CountPaths(List<long> values)
        {
            var sol = new List<List<long>>();
            sol.Add(values);
            for(int i = 0; i<sol.Count; i++)
            {
                var work = sol[i];

                for (int j = 1; j < work.Count -1; j++)
                {
                    if (work[j+1]- work[j-1] <= 3)
                    {
                        var newList = new List<long>(work);
                        newList.RemoveAt(j);
                        if (!Contains(sol, newList))
                            sol.Add(newList);
                    }
                }
            }


            return sol.Count;

        }

        private static bool Contains(List<List<long>> sol, List<long> newlist)
        {
            foreach(var list in sol)
            {
                if (list.Count != newlist.Count)
                    continue;

                bool isSame = true;
                for (int i = 0; i< list.Count;i++ )
                {
                    if (list[i] != newlist[i])
                    {
                        isSame = false;
                        break;
                    }
                }

                if (isSame)
                    return true;
            }

            return false;
        }
    }
}

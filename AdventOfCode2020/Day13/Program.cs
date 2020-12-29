using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            int DepTime = Int32.Parse(lines[0]);

            var routes = lines[1].Split(',').Where(s => s != "x").Select(s => Int32.Parse(s)).ToList();

            int minWait = 100000;
            int sol = 0;
            foreach (var route in routes)
            {
                var wait = route * ((int)Math.Floor(1.0 * DepTime / route) + 1) - DepTime;
                if (wait < minWait)
                {
                    minWait = wait;
                    sol = wait * route;
                }
            }
            Console.WriteLine($"Solution is {sol}");

            var routeOffset = lines[1].Split(',')
                .Select((s, i) => (s, i))
                .Where(si => si.s != "x")
                .Select(si => (route: Int64.Parse(si.s), offset: (long)si.i))
                .ToList();

            //routeOffset.Sort((l, r) => (int)(r.route - l.route));

            while (routeOffset.Count > 1)
            {
                routeOffset.Insert(2, Join(routeOffset[0], routeOffset[1]));
                routeOffset.RemoveAt(0);
                routeOffset.RemoveAt(0);
            }

            Console.WriteLine($"First value is {routeOffset[0].route- routeOffset[0].offset}");

            //for (long i = 1; ;i++ )
            //{
            //    long v = routeOffset[0].route * i - routeOffset[0].offset;

            //    bool found = true;
            //    for(int j = 1; j< routeOffset.Count; j++)
            //    {
            //        if ((v + routeOffset[j].offset) % routeOffset[j].route != 0 )
            //        {
            //            found = false;
            //            break;
            //        }
            //    }

            //    if (found)
            //    {
            //        Console.WriteLine($"First value is {v}");
            //        break;
            //    }
            //}
        }

        private static (long route, long offset) Join((long route, long offset) p1, (long route, long offset) p2)
        {
            var result = (route: 0L, offset: 0L);

            for (long i = 1; ; i++)
            {
                long v = p1.route * i - p1.offset;

                if ((v + p2.offset) % p2.route == 0)
                {
                    if (result.offset == 0)
                    {
                        result.offset = v;
                    } else
                    {
                        result.route = v - result.offset;
                        result.offset = result.route - result.offset;
                        return result;
                    }
                }
            }

            return result;
        }
    }
}


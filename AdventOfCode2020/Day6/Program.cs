using System;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            int sum = 0;

            string allOptions = "";
            //foreach (var line in lines)
            //{
            //    allOptions += line;

            //    if (string.IsNullOrWhiteSpace(line))
            //    {
            //        sum += allOptions.Distinct().Count();

            //        allOptions = "";
            //    }
            //}
            //sum += allOptions.Distinct().Count();

            bool first = true;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    sum += allOptions.Count();

                    allOptions = "";
                    first = true;
                } else {
                    if (first)
                    {
                        allOptions = line;
                        first = false;
                    }
                    else
                    {
                        allOptions = string.Join(null, allOptions.Where(c => line.Contains(c)).ToArray());
                    }
                }
            }
            sum += allOptions.Count();

            Console.WriteLine($"Sum is {sum}");
        }
    }
}

using System;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            long totalSum = 0;
            foreach (var line in lines)
            {
                //check 
                long res = Solve(line);
                Console.WriteLine($"Result of the sum is {res}");
                totalSum += res;
            }

            Console.WriteLine($"Total sum is {totalSum}");
        }

        private static long Solve(string line)
        {
            // fix backets
            while (line.Contains('('))
            {
                var openClose = line
                    .Select((c, i) => (c, i))
                    .Where(ci => ci.c == '(' || ci.c == ')')
                    .ToList();

                for(int i = 0; i < openClose.Count - 1; i++)
                {
                    if (openClose[i].c == '(' && openClose[i+1].c == ')')
                    {
                        long v = Solve(line.Substring(openClose[i].i + 1, openClose[i + 1].i - openClose[i].i - 1));
                        line = line.Remove(openClose[i].i, openClose[i + 1].i - openClose[i].i +1);
                        line = line.Insert(openClose[i].i, v.ToString());
                        break;
                    }
                }
            }

            var words = line.Split(" ").ToList();
            //long value = Int64.Parse(words[0]);
            //for (int i = 1; i< words.Count; i+=2)
            //{
            //    long nextValue = Int64.Parse(words[i+1]);
            //    if (words[i] == "+")
            //    {
            //        value += nextValue;
            //    } else
            //    {
            //        value *= nextValue;
            //    }
            //}

            for (int i = words.Count - 2; i > 0; i -= 2)
            {
                if (words[i] == "+")
                {
                    long vs = Int64.Parse(words[i - 1]) + Int64.Parse(words[i + 1]);
                    words[i - 1] = vs.ToString();
                    words.RemoveAt(i);
                    words.RemoveAt(i);
                }
            }

            long value = Int64.Parse(words[0]);
            for (int i = 1; i < words.Count; i += 2)
            {
                long nextValue = Int64.Parse(words[i + 1]);
                value *= nextValue;
            }

            return value;
        }
    }
}

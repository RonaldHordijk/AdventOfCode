using System;
using System.IO;
using System.Net;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            var v1 = NewMethod(lines, 1, 1);
            var v2 = NewMethod(lines, 1, 3);
            var v3 = NewMethod(lines, 1, 5);
            var v4 = NewMethod(lines, 1, 7);
            var v5 = NewMethod(lines, 2, 1);

            long nrTrees = v1 * v2 * v3 * v4 * v5;

            Console.WriteLine($"NrTrees = {nrTrees}");
        }

        private static long NewMethod(string[] lines, int down, int right)
        {
            int nrTrees = 0;
            for (int i = 0; i < (lines.Length/down); i++)
            {
                if (lines[i * down][(i * right) % lines[i * down].Length] == '#')
                    nrTrees++;
            }

            return nrTrees;
        }
    }
}

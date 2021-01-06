using System;
using System.IO;

namespace Day01
{
    static class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            long sum = 0;

            foreach (var line in lines)
            {
                long value = Int64.Parse(line);
                value /= 3;
                value -= 2;

                while (value >= 0)
                {
                    sum += value;
                    value /= 3;
                    value -= 2;
                }
            }

            Console.WriteLine($"sum is {sum}");


        }
    }
}

using System;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            int max = 0;
            bool[] seats = new bool[960];

            foreach(var line in lines)
            {
                var bin1 = string.Join(null, line.Take(7).Select(c => c == 'B' ? '1' : '0').ToArray());
                var bin2 = string.Join(null, line.Skip(7).Select(c => c == 'L' ? '0' : '1').ToArray());

                int row = Convert.ToInt32(bin1, 2);
                int column = Convert.ToInt32(bin2, 2);
                int id = row * 8 + column;

                max = Math.Max(id, max);
                seats[id] = true;
            }

            Console.WriteLine($"Max Id is {max}");

            for (int i = 1; i < 859; i++)
            {
                if (seats[i - 1] && !seats[i] && seats[i + 1])
                {
                    Console.WriteLine($"openseat {i}");
                }
            }
        }
    }
}

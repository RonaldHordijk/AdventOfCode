using System;
using System.IO;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            var values = lines.Select(l => Int64.Parse(l)).ToArray();

            int window = 25;
            long searchValue = 0;

            for (int i = window; i< values.Length; i++)
            {
                bool found = false;
                for (int j = i-window; j < i-1; j++)
                {
                    var rem = values[i] - values[j];
                    for (int k = j + 1; k < i; k++)
                    {
                        if (values[k] == rem)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found) 
                        break;
                }

                if (!found)
                {
                    Console.WriteLine($"First invalid value is {values[i]}");
                    searchValue = values[i];
                    break;
                }
            }

            for (int i = 0; i < values.Length; i++)
            {
                long sum = 0;
                for (int j = i; i < values.Length; j++)
                {
                    sum += values[j];
                    if (sum > searchValue)
                        break;

                    if (sum == searchValue)
                    {
                        var set = values[i..(j+1)];
                        var min = set.Min();
                        var max = set.Max();

                        Console.WriteLine($"Code is {min+max}");

                        break;
                    }
                }
            }
        }
    }
}

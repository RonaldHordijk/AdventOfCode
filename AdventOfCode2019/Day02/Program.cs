using System;
using System.IO;
using System.Linq;

namespace Day02
{
    static class Program
    {
        static void Main(string[] args)
        {

            for (int x = 0; x <= 100; x++)
            {
                for (int y = 0; y <= 100; y++)
                {

                    var values = File.ReadAllLines("data.txt")[0]
                    .Split(',')
                    .Select(s => Int64.Parse(s))
                    .ToArray();

                    values[1] = x;
                    values[2] = y;


                    int pos = 0;
                    while (values[pos] != 99)
                    {
                        var mode = values[pos];
                        var v1 = values[pos + 1];
                        var v2 = values[pos + 2];
                        var loc = values[pos + 3];
                        if (mode == 1)
                        {
                            values[loc] = values[v1] + values[v2];
                        }
                        else
                        {
                            values[loc] = values[v1] * values[v2];
                        }

                        pos += 4;
                    }

                    if (values[0] == 19690720)
                        Console.WriteLine($"Found Value at {x} - {y}");
                }
            }


        }
    }
}

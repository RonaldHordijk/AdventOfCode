using System;
using System.IO;
using System.Linq;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = File.ReadAllLines("data.txt")[0]
                            .Split(',')
                            .Select(s => Int32.Parse(s))
                            .ToArray();


            int pos = 0;
            while (values[pos] != 99)
            {
                var mode = values[pos] % 100;
                bool p1Immediate = (values[pos] / 100) % 2 == 1;
                bool p2Immediate = (values[pos] / 1000) % 2 == 1;
                bool p3Immediate = (values[pos] / 10000) % 2 == 1;

                switch (mode)
                {
                    case 1:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            var p3 = values[pos + 3];
                            values[p3] = p1 + p2;
                            pos += 4;
                            break;
                        }
                    case 2:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            var p3 = values[pos + 3];
                            values[p3] = p1 * p2;
                            pos += 4;
                            break;
                        }
                    case 3:
                        {
                            var p1 = values[pos + 1];
                            values[p1] = 5;
                            pos += 2;
                            break;
                        }
                    case 4:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            Console.WriteLine(p1);
                            pos += 2;
                            break;
                        }
                    case 5:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            pos = (p1 != 0) ? p2 : pos + 3;
                            break;
                        }
                    case 6:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            pos = (p1 == 0) ? p2 : pos + 3;
                            break;
                        }
                    case 7:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            var p3 = values[pos + 3];
                            values[p3] = (p1 < p2) ? 1 : 0;
                            pos += 4;
                            break;
                        }
                    case 8:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            var p3 = values[pos + 3];
                            values[p3] = (p1 == p2) ? 1 : 0;
                            pos += 4;
                            break;
                        }

                }

            }
        }
    }
}

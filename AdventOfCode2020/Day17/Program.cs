using System;
using System.Collections.Generic;
using System.IO;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            int size = lines[0].Length;
            var map = new int[16 + size, 16 + size, 16, 16];
            var map1 = new int[16 + size, 16 + size, 16, 16];

            int iy = 8;
            foreach (var line in lines)
            {
                int ix = 8;
                foreach (var c in line.ToCharArray())
                    map[ix++, iy, 8, 8] = (c == '#') ? 1 : 0;

                iy++;
            }

            var neighbours = new List<(int ox, int oy, int oz, int ow)>{
                ( -1, -1, -1, -1 ),
                ( -1, -1, -1, 0 ),
                ( -1, -1, -1, 1 ),
                ( -1, -1, 0, -1 ),
                ( -1, -1, 0, 0 ),
                ( -1, -1, 0, 1 ),
                ( -1, -1, 1, -1 ),
                ( -1, -1, 1, 0 ),
                ( -1, -1, 1, 1 ),
                ( -1, 0, -1, -1 ),
                ( -1, 0, -1, 0 ),
                ( -1, 0, -1, 1 ),
                ( -1, 0, 0, -1 ),
                ( -1, 0, 0, 0 ),
                ( -1, 0, 0, 1 ),
                ( -1, 0, 1, -1 ),
                ( -1, 0, 1, 0 ),
                ( -1, 0, 1, 1 ),
                ( -1, 1, -1, -1 ),
                ( -1, 1, -1, 0 ),
                ( -1, 1, -1, 1 ),
                ( -1, 1, 0, -1 ),
                ( -1, 1, 0, 0 ),
                ( -1, 1, 0, 1 ),
                ( -1, 1, 1, -1 ),
                ( -1, 1, 1, 0 ),
                ( -1, 1, 1, 1 ),
                ( 0, -1, -1, -1 ),
                ( 0, -1, -1, 0 ),
                ( 0, -1, -1, 1 ),
                ( 0, -1, 0, -1 ),
                ( 0, -1, 0, 0 ),
                ( 0, -1, 0, 1 ),
                ( 0, -1, 1, -1 ),
                ( 0, -1, 1, 0 ),
                ( 0, -1, 1, 1 ),
                ( 0, 0, -1, -1 ),
                ( 0, 0, -1, 0 ),
                ( 0, 0, -1, 1 ),
                ( 0, 0, 0, -1 ),
                ( 0, 0, 0, 1 ),
                ( 0, 0, 1, -1 ),
                ( 0, 0, 1, 0 ),
                ( 0, 0, 1, 1 ),
                ( 0, 1, -1, -1 ),
                ( 0, 1, -1, 0 ),
                ( 0, 1, -1, 1 ),
                ( 0, 1, 0, -1 ),
                ( 0, 1, 0, 0 ),
                ( 0, 1, 0, 1 ),
                ( 0, 1, 1, -1 ),
                ( 0, 1, 1, 0 ),
                ( 0, 1, 1, 1 ),
                ( 1, -1, -1, -1 ),
                ( 1, -1, -1, 0 ),
                ( 1, -1, -1, 1 ),
                ( 1, -1, 0, -1 ),
                ( 1, -1, 0, 0 ),
                ( 1, -1, 0, 1 ),
                ( 1, -1, 1, -1 ),
                ( 1, -1, 1, 0 ),
                ( 1, -1, 1, 1 ),
                ( 1, 0, -1, -1 ),
                ( 1, 0, -1, 0 ),
                ( 1, 0, -1, 1 ),
                ( 1, 0, 0, -1 ),
                ( 1, 0, 0, 0 ),
                ( 1, 0, 0, 1 ),
                ( 1, 0, 1, -1 ),
                ( 1, 0, 1, 0 ),
                ( 1, 0, 1, 1 ),
                ( 1, 1, -1, -1 ),
                ( 1, 1, -1, 0 ),
                ( 1, 1, -1, 1 ),
                ( 1, 1, 0, -1 ),
                ( 1, 1, 0, 0 ),
                ( 1, 1, 0, 1 ),
                ( 1, 1, 1, -1 ),
                ( 1, 1, 1, 0 ),
                ( 1, 1, 1, 1 ),
            };


            for (int step =0; step <6; step++)
            {
                Console.WriteLine($"Result {step} is {CountLife(size, map)}");

                for (int x = 1; x < 16 + size - 1; x++)
                {
                    for (int y = 1; y < 16 + size - 1; y++)
                    {
                        for (int z = 1; z < 16 - 1; z++)
                        {
                            for (int w = 1; w < 16 - 1; w++)
                            {
                                var count = 0;
                                foreach (var n in neighbours)
                                {
                                    count += map[x + n.ox, y + n.oy, z + n.oz, w + n.ow];
                                }

                                if (map[x, y, z, w] == 1 && ((count == 2) || (count == 3)))
                                {
                                    map1[x, y, z, w] = 1;
                                }
                                else if (map[x, y, z, w] == 0 && count == 3)
                                {
                                    map1[x, y, z, w] = 1;
                                }
                                else
                                    map1[x, y, z, w] = 0;
                            }
                        }
                    }
                }

                var t = map1;
                map1 = map;
                map = t;

            }

            Console.WriteLine($"Result is {CountLife(size, map)}");
        }

        private static int CountLife(int size, int[,,,] map)
        {
            var sum1 = 0;

            for (int x = 1; x < 16 + size - 1; x++)
            {
                for (int y = 1; y < 16 + size - 1; y++)
                {
                    for (int z = 1; z < 16 - 1; z++)
                        for (int w = 1; w < 16 - 1; w++)
                            sum1 += map[x, y, z, w];
                }
            }

            return sum1;
        }
    }
}

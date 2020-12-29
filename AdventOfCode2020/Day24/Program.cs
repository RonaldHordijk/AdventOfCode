using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Day24
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            var positions = new List<Point>();

            foreach (var line in lines)
            {
                Point pos = new Point(0, 0);
                int offset = 0;
                while (offset < line.Length)
                {
                    if (line[offset] == 'w')
                    {
                        pos.X -= 2;
                        offset++;
                    }
                    else if (line[offset] == 'e')
                    {
                        pos.X += 2;
                        offset++;
                    }
                    else if (line[offset] == 'n')
                    {
                        pos.Y -= 1;
                        offset++;

                        if (line[offset] == 'w')
                        {
                            pos.X -= 1;
                        }
                        else //e
                        {
                            pos.X += 1;
                        }
                        offset++;
                    }
                    else if (line[offset] == 's')
                    {
                        pos.Y += 1;
                        offset++;

                        if (line[offset] == 'w')
                        {
                            pos.X -= 1;
                        }
                        else //e
                        {
                            pos.X += 1;
                        }
                        offset++;
                    }
                }
                positions.Add(pos);
            }

            bool[] processed = new bool[positions.Count];
            int flipped = 0;

            for (int i = 0; i < positions.Count; i++)
            {
                if (processed[i])
                    continue;

                int nrOccur = 1;
                for (int j = i + 1; j < positions.Count; j++)
                {
                    if (positions[i].X == positions[j].X && positions[i].Y == positions[j].Y)
                    {
                        nrOccur++;
                        processed[j] = true;
                    }
                }

                if (nrOccur % 2 == 1)
                    flipped++;
            }

            Console.WriteLine($"Nr tiles flipped is {flipped}");

            bool[,] map = new bool[500, 500];
            bool[,] map1 = new bool[500, 500];
            for (int i = 0; i < positions.Count; i++)
            {
                int y = positions[i].Y + 250;
                int x = positions[i].Y % 2 == 0 ? positions[i].X / 2 + 250 : (positions[i].X + 1) / 2 + 250;
                map[x, y] = !map[x, y];
            }

            for (int step = 0; step <= 100; step++)
            {
                int cnt = CountFlipped(map);
                Console.WriteLine($"Step {step}  Nr tiles flipped is {cnt}");

                //DisplayMap(map);

                for (int x = 1; x < 500 - 1; x++)
                {
                    for (int y = 1; y < 500 - 1; y++)
                    {
                        int neighbours = 0;
                        if (map[x - 1, y])
                            neighbours++;
                        if (map[x + 1, y])
                            neighbours++;
                        if (map[x, y - 1])
                            neighbours++;
                        if (map[x, y + 1])
                            neighbours++;
                        if (y % 2 == 0)
                        {
                            if (map[x + 1, y - 1])
                                neighbours++;
                            if (map[x + 1, y + 1])
                                neighbours++;
                        }
                        else
                        {
                            if (map[x - 1, y - 1])
                                neighbours++;
                            if (map[x - 1, y + 1])
                                neighbours++;
                        }

                        map1[x, y] = false;
                        if (map[x, y] && neighbours >= 1 && neighbours <= 2)
                            map1[x, y] = true;
                        if (!map[x, y] && neighbours == 2)
                            map1[x, y] = true;
                    }
                }

                var temp = map;
                map = map1;
                map1 = temp;
            }

        }

        private static void DisplayMap(bool[,] map)
        {
            for (int y = 245; y < 255; y++)
            {
                if (y % 2 == 1)
                    Console.Write(" ");
                for (int x = 245; x < 255; x++)
                {
                    Console.Write(map[x, y] ? "X " : ". ");
                }
                Console.WriteLine();
            }
        }

        private static int CountFlipped(bool[,] map)
        {
            int cnt = 0;
            for (int x = 0; x < 500; x++)
            {
                for (int y = 0; y < 500; y++)
                {
                    if (map[x, y])
                        cnt++;
                }
            }

            return cnt;
        }
    }
}

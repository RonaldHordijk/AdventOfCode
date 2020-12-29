using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            int width = lines[0].Length;
            int height = lines.Length;
            int[,] map = new int[height, width];
            

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = lines[i][j] == '.' ? 0 : 1;
                }
            }

            //Display(width, height, map);
            while (true)
            {
                var map2 = NewMap(width, height, map);
                //Display(width, height, map2);

                if (AreSame(width, height, map, map2))
                {
                    var count = CountOcc(width, height, map);
                    Console.WriteLine($"Number of occupied seat is {count}");
                    break;
                }
                map = map2;
            }

        }

        private static int CountOcc(int width, int height, int[,] map)
        {
            int count = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] == 2)
                        count++;
                }
            }

            return count;
        }

        private static bool AreSame(int width, int height, int[,] map, int[,] map2)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] != map2[i, j])
                        return false;
                }
            }

            return true;
        }

        private static void Display(int width, int height, int[,] map)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] == 0)
                        Console.Write('.');
                    if (map[i, j] == 1)
                        Console.Write('O');
                    if (map[i, j] == 2)
                        Console.Write('X');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static int[,] NewMap(int width, int height, int[,] map)
        {
            int[,] map2 = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int nrOcc = 0;

                    for (int x = -1; x<2; x++)
                    {
                        for (int y = -1; y<2 ; y++)
                        {
                            if (x == 0 && y == 0)
                                continue;

                            int step = 0;
                            while (true)
                            {
                                step++;

                                int ni = i + step * x;
                                int nj = j + step * y;

                                if (ni < 0 || ni >= height)
                                    break;

                                if (nj < 0 || nj >= width)
                                    break;
                                if (map[ni, nj] == 0)
                                    continue;

                                if (map[ni, nj] == 2)
                                    nrOcc++;

                                break;
                            }
                        }
                    }

                    map2[i, j] = map[i, j];
                    if (map2[i, j] == 1 && nrOcc == 0)
                    {
                        map2[i, j] = 2;
                    }
                    else if (map2[i, j] == 2 && nrOcc >= 5)
                    {
                        map2[i, j] = 1;
                    }
                }
            }

            return map2;
        }
    }
}

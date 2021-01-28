using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    public class Meteor
    {
        public int x;
        public int y;
        public double angle;
        public double dist;
    }


    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            var map = lines.Select(l => l.Select(c => c == '#' ? 1 : 0).ToArray()).ToArray();

            var bestPos = FindBestPos(map);

            Console.WriteLine($"Best position is at ({bestPos.x}, {bestPos.y}) with {bestPos.nr - 1}");

            GetMeteorInOrder(map, bestPos.x, bestPos.y);
        }

        private static void GetMeteorInOrder(int[][] map, int a, int b)
        {
            var meteors = new List<Meteor>();

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 0)
                        continue;

                    if (x == a && y == b)
                        continue;

                    meteors.Add(new Meteor
                    {
                        x = x,
                        y = y,
                        angle = Math.Atan2(y - b, x - a) * 180.0 / Math.PI,
                        dist = Math.Sqrt((x - a) * (x - a) + (y - b) * (y - b))
                    });
                }
            }

            int cnt = 1;
            double angle = -90 - 1E-5;

            while (meteors.Count > 0 && cnt < 210)
            {
                //printMap(map);

                var next = meteors.Where(m => m.angle > angle).OrderBy(m => m.angle).ThenBy(m => m.dist).First();

                map[next.y][next.x] = 0;
                angle = next.angle + 1e-5;
                if (angle > 180)
                    angle -= 360;

                Console.WriteLine($"{cnt++} at {next.x},{next.y}");

                meteors.Remove(next);
            }
        }

        private static (int x, int y, int nr) FindBestPos(int[][] map)
        {
            (int x, int y, int nr) minPos = (0, 0, 0);
            int minMeteors = 0;

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 0)
                        continue;

                    var copy = CloneMap(map);
                    RemoveHidden(y, x, ref copy);
                    var count = CountMeteors(copy);
                    //printMap(map);
                    //printMap(copy);

                    if (count > minMeteors)
                    {
                        minMeteors = count;
                        minPos = (x, y, count);
                    }
                }
            }

            return minPos;
        }

        private static void printMap(int[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                Console.WriteLine(new string(map[y].Select(c => c == 1 ? '#' : '.').ToArray()));
            }
            Console.WriteLine();
        }

        private static int CountMeteors(int[][] map)
        {
            return map.Sum(l => l.Sum());
        }

        private static void RemoveHidden(int a, int b, ref int[][] map)
        {
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (x == b && y == a)
                        continue;

                    bool remove = false;
                    var wx = x;
                    var wy = y;

                    while (wx >= 0 && wy >= 0 && wx < map[0].Length && wy < map.Length)
                    {
                        if (remove)
                            map[wy][wx] = 0;
                        else
                            remove = map[wy][wx] == 1;

                        wx = wx + (x - b);
                        wy = wy + (y - a);
                    }
                }
            }
        }

        private static int[][] CloneMap(int[][] map)
        {
            return map.Select(l => (int[])l.Clone()).ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Day20
{
    class Map
    {
        public int Id { get; set; }
        public List<string> Lines { get; set; } = new List<string>();
        private int[,] _edges = new int[8, 4];

        public int EdgeValue(int order, int side) => _edges[order, side];

        private static (int x, int y) Orient(int order, int x, int y) => order switch
        {
            0 => (x, y),
            1 => (x, 9 - y),
            2 => (9 - x, 9 - y),
            3 => (9 - x, 9 - y),
            4 => (y, x),
            5 => (y, 9 - x),
            6 => (9 - y, x),
            7 => (9 - y, 9 - x),
            _ => (x, y),
        };

        private string GetSideText(int order, int side)
        {
            return side switch
            {
                0 => new string(Enumerable.Range(0, 10)
                                       .Select(i => Orient(order, 0, i))
                                       .Select(p => Lines[p.x][p.y])
                                       .ToArray()),
                1 => new string(Enumerable.Range(0, 10)
                                        .Select(i => Orient(order, i, 0))
                                        .Select(p => Lines[p.x][p.y])
                                        .ToArray()),
                2 => new string(Enumerable.Range(0, 10)
                                    .Select(i => Orient(order, 9, i))
                                    .Select(p => Lines[p.x][p.y])
                                    .ToArray()),
                3 => new string(Enumerable.Range(0, 10)
                                    .Select(i => Orient(order, i, 9))
                                    .Select(p => Lines[p.x][p.y])
                                    .ToArray()),
                _ => "",
            };
        }

        private int StringToInt(string s) => Convert.ToInt32(string.Join(null, s.Select(c => c == '#' ? '1' : '0').ToArray()), 2);

        public void Init()
        {
            foreach (var order in Enumerable.Range(0, 8))
            {
                _edges[order, 0] = StringToInt(GetSideText(order, 0));
                _edges[order, 1] = StringToInt(GetSideText(order, 1));
                _edges[order, 2] = StringToInt(GetSideText(order, 2));
                _edges[order, 3] = StringToInt(GetSideText(order, 3));
            }
        }

        public List<string> InnerMap(int order)
        {
            //return Enumerable.Range(1, 8).Select(r =>
            //        new string(Enumerable.Range(1, 8)
            //                        .Select(i => Orient(order, r, i))
            //                        .Select(p => Lines[p.x][p.y])
            //                        .ToArray())).ToList();

            var result = new List<string>();

            for (int i = 1; i < 9; i++)
            {
                string s = "";
                for (int j = 1; j < 9; j++)
                {
                    (int x, int y) = Orient(order, j, i);
                    s += Lines[x][y];
                }
                result.Add(s);
            }

            return result;
        }
    }

    class Program
    {
        private static (int x, int y) Orient2(int order, int x, int y, int max) => order switch
        {
            0 => (x, y),
            1 => (x, max - y),
            2 => (max - x, max - y),
            3 => (max - x, max - y),
            4 => (y, x),
            5 => (y, max - x),
            6 => (max - y, x),
            7 => (max - y, max - x),
            _ => (x, y),
        };

        static void Main(string[] args)
        {
            int[] sol =
            {
3229, 2111, 3559, 2683, 2767, 1051, 1787, 1321, 2381, 2273, 1229, 1049,
2671, 2819, 3407, 2377, 1607, 3637, 1471, 3391, 1039, 1657, 2957, 2089,
3701, 2383, 2861, 3319, 2503, 2593, 2029, 1439, 3583, 3727, 3037, 1973,
3911, 1811, 3733, 3677, 3571, 1453, 2801, 3083, 1327, 3331, 2027, 2357,
1447, 1637, 2477, 3313, 3923, 3203, 1847, 2287, 3517, 3581, 3167, 3307,
1231, 3217, 2903, 2689, 3299, 1759, 1733, 1913, 3821, 1489, 3907, 2557,
1033, 1747, 3251, 1777, 1153, 2267, 2633, 3623, 1181, 1123, 2887, 2473,
1663, 2411, 3989, 3049, 2143, 2677, 2053, 3967, 1069, 3779, 1511, 2179,
3541, 1823, 3023, 3767, 3797, 2749, 1217, 1619, 2953, 1709, 1567, 3833,
2099, 2803, 2333, 3089, 2939, 1801, 2909, 3631, 1013, 2039, 1319, 3931,
1061, 2269, 3919, 3433, 1999, 1949, 2239, 3943, 2693, 1933, 2311, 3769,
2129, 2441, 2659, 1583, 1523, 1873, 2347, 3889, 2221, 1399, 3697, 2081};


            var lines = File.ReadAllLines("data.txt");

            var Maps = new List<Map>();

            foreach (var line in lines)
            {
                if (line.StartsWith("Tile"))
                {
                    Maps.Add(new Map
                    {
                        Id = Int32.Parse(line.Substring(5, 4))
                    });
                    continue;
                }

                if (string.IsNullOrEmpty(line))
                    continue;

                Maps.Last().Lines.Add(line);
            }

            foreach (var map in Maps)
            {
                map.Init();
            }

            var positions = new int[150, 2];
            for (int i = 0; i <= Maps.Count; i++)
            {
                positions[i, 0] = 0;
                positions[i, 1] = -1;
            }

            bool[] inUse = new bool[150];

            int currentIndex = 0;
            int nrRows = 12;

            positions[0, 0] = 0;
            positions[0, 1] = -1;

            while (true)
            {
                if (currentIndex == Maps.Count)
                    break;

                if (currentIndex == 0 && positions[0, 0] >= Maps.Count)
                    break;

                positions[currentIndex, 1]++;
                if (positions[currentIndex, 1] >= 8)
                {
                    positions[currentIndex, 0]++;
                    positions[currentIndex, 1] = 0;
                }

                if (positions[currentIndex, 0] >= Maps.Count)
                {
                    currentIndex--;
                    inUse[positions[currentIndex, 0]] = false;
                    continue;
                }

                if (Maps[positions[currentIndex, 0]].Id != sol[currentIndex])
                    continue;

                if (inUse[positions[currentIndex, 0]])
                {
                    positions[currentIndex, 1] = 7;
                    continue;
                }

                //fits;
                bool firstRow = (currentIndex < nrRows);
                bool firstColom = (currentIndex % nrRows) == 0;
                bool fits = (firstRow || (Maps[positions[currentIndex, 0]].EdgeValue(positions[currentIndex, 1], 1) == Maps[positions[currentIndex - nrRows, 0]].EdgeValue(positions[currentIndex - nrRows, 1], 3)))
                           && (firstColom || (Maps[positions[currentIndex, 0]].EdgeValue(positions[currentIndex, 1], 0) == Maps[positions[currentIndex - 1, 0]].EdgeValue(positions[currentIndex - 1, 1], 2)));

                if (fits)
                {
                    inUse[positions[currentIndex, 0]] = true;
                    currentIndex++;
                    positions[currentIndex, 0] = 0;
                    positions[currentIndex, 1] = -1;
                }
            }


            for (int i = 0; i < nrRows; i++)
            {
                for (int j = 0; j < nrRows; j++)
                {
                    Console.Write($"{Maps[positions[i * nrRows + j, 0]].Id}  ");
                }
                Console.WriteLine();
            }

            var picture = new List<string>();

            for (int i = 0; i < nrRows; i++)
            {
                for (int r = 0; r < 8; r++)
                    picture.Add("");

                for (int j = 0; j < nrRows; j++)
                {
                    var im = Maps[positions[i * nrRows + j, 0]].InnerMap(positions[i * nrRows + j, 1]);
                    for (int r = 0; r < 8; r++)
                        picture[picture.Count - 8 + r] += im[r];
                }
            }

            for (int i = 0; i < picture.Count; i++)
                Console.WriteLine(picture[i]);

            int monsterMaxX = 20;
            int monsterMaxY = 3;
            int[,] monster =
            {
                {18,0},
                {0, 1},
                {5, 1},
                {6, 1},
                {11, 1},
                {12, 1},
                {17, 1},
                {18, 1},
                {19, 1},
                {1, 2},
                {4, 2},
                {7, 2},
                {10, 2},
                {13, 2},
                {16, 2},
            };

            for (int order = 1; order < 2; order++)
            {
                int monsterCount = 0;

                for (int x = 0; x < picture.Count - monsterMaxX; x++)
                {
                    for (int y = 0; y < picture.Count - monsterMaxY; y++)
                    {
                        bool found = true;
                        for (int m = 0; m < 15; m++)
                        {
                            (int ox, int oy) = Orient2(order, x + monster[m, 0], y + monster[m, 1], picture.Count - 1);

                            if (picture[ox][oy] == '.')
                            {
                                found = false;
                                break;
                            }
                        }
                        if (found)
                        {
                            monsterCount++;
                            for (int m = 0; m < 15; m++)
                            {
                                (int ox, int oy) = Orient2(order, x + monster[m, 0], y + monster[m, 1], picture.Count - 1);

                                var s= picture[ox].Remove(oy, 1);
                                picture[ox] = s.Insert(oy, "o");
                            }
                        }
                    }
                }
                Console.WriteLine($"order {order}, monsters = {monsterCount}");
            }

            for (int i = 0; i < picture.Count; i++)
                Console.WriteLine(picture[i]);

            var cnt = picture.Sum(l => l.Sum(c => c == '#' ? 1 : 0));
            Console.WriteLine($"Nr # {cnt}");

        }
    }
}

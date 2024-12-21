//966130
var lines = File.ReadAllLines("data.txt");

int xcount = lines[0].Length;
int ycount = lines.Length;
int[,] map = new int[xcount, ycount];
long[,] solve = new long[xcount, ycount];

int sx = 0;
int sy = 0;
int ex = 0;
int ey = 0;

for (int y = 0; y < ycount; y++)
{
    var line = lines[y];
    for (int x = 0; x < line.Length; x++)
    {
        map[x, y] = 0;
        if (line[x] == '#')
            map[x, y] = 1;

        if (line[x] == 'S')
        {
            sx = x;
            sy = y;
        }
        if (line[x] == 'E')
        {
            ex = x;
            ey = y;
        }
    }
}

SortedDictionary<long, int> Paths = [];

var pp = GetPath();
for (int loc = 0; loc < pp.Count - 1; loc++)
{
    int startx = pp[loc].x;
    int starty = pp[loc].y;

    for (int nl = loc + 1; nl < pp.Count; nl++)
    {
        int x = pp[nl].x;
        int y = pp[nl].y;

        int cl = Math.Abs(startx - x) + Math.Abs(starty - y);
        if (cl > 20)
            continue;

        int newlength = loc + cl + (pp.Count - nl);
        if (newlength < 0)
            continue;

        if (Paths.TryGetValue(newlength, out int count))
        {
            Paths[newlength] = count + 1;
        }
        else
        {
            Paths[newlength] = 1;
        }
    }
}

long path = ShortestPath();
Console.WriteLine($"Path {path}");


int over100 = 0;
foreach ((long length, int count) in Paths)
{
    Console.WriteLine($"{path - length} {count}");

    if ((pp.Count - length) >= 100)
        over100 += count;
}
Console.WriteLine($"over 100 {over100}");
Console.WriteLine();





//for (int y = 1; y < ycount - 1; y++)
//{
//    for (int x = 1; x < ycount - 1; x++)
//    {
//        if (map[x, y] == 1)
//        {
//            map[x, y] = 0;
//            long p1 = ShortestPath();
//            if (paths.TryGetValue(p1, out int count))
//            {
//                paths[p1] = count + 1;
//            }
//            else
//            {
//                paths[p1] = 1;
//            }

//            map[x, y] = 1;
//        }
//    }
//}

var cheats2 = GetCheatPaths(1, 2);

for (int y = 1; y < ycount - 1; y++)
{
    for (int x = 1; x < ycount - 1; x++)
    {
        if (map[x, y] == 0)
        {
            var cheats = GetCheatPaths(x, y);
            foreach ((int cx, int cy, int length) in cheats)
            {
                if (cx < x)
                    continue;

                if (cx == x && cy < y)
                    continue;

                long p1 = ShortestPath(x, y, cx, cy, length);

                if (Paths.TryGetValue(p1, out int count))
                {
                    Paths[p1] = count + 1;
                }
                else
                {
                    Paths[p1] = 1;
                }
            }
        }
    }
}


//int over100 = 0;
//foreach ((long length, int count) in Paths)
//{
//    Console.WriteLine($"{path - length} {count}");

//    if ((path - length) >= 100)
//        over100 += count;
//}
//Console.WriteLine($"over 100 {over100}");

void CheckCheatPaths(int loc)
{
    int startx = pp[loc].x;
    int starty = pp[loc].y;

    for (int nl = loc + 99; nl < pp.Count; nl++)
    {
        int x = pp[nl].x;
        int y = pp[nl].y;

        int cl = Math.Abs(startx - x) + Math.Abs(starty - y);
        if (cl > 20)
            continue;

        int newlength = pp.Count - (nl - loc) + cl;
        if (newlength < 0)
            return;

        if (Paths.TryGetValue(newlength, out int count))
        {
            Paths[newlength] = count + 1;
        }
        else
        {
            Paths[newlength] = 1;
        }
    }
}



List<(int x, int y, int length)> GetCheatPaths(int sx, int sy)
{
    long[,] solve = new long[xcount, ycount];

    List<(int x, int y, long score)> paths = [(sx, sy, 0)];
    solve[sx, sy] = 1;

    // find best solution
    while (paths.Count > 0)
    {
        paths.Sort((l, r) => (int)(l.score - r.score));

        (int x, int y, long sscore) = paths[0];

        if (x < xcount - 1 && map[x + 1, y] == 1)
        {
            long score = sscore + 1;
            if (solve[x + 1, y] == 0 || (score < solve[x + 1, y]))
            {
                solve[x + 1, y] = score;

                paths.Add((x + 1, y, score));
            }
        }
        if (x > 0 && map[x - 1, y] == 1)
        {
            long score = sscore + 1;
            if (solve[x - 1, y] == 0 || (score < solve[x - 1, y]))
            {
                solve[x - 1, y] = score;

                paths.Add((x - 1, y, score));
            }
        }
        if (y < ycount - 1 && map[x, y + 1] == 1)
        {
            long score = sscore + 1;
            if (solve[x, y + 1] == 0 || (score < solve[x, y + 1]))
            {
                solve[x, y + 1] = score;

                paths.Add((x, y + 1, score));
            }
        }
        if (y > 0 && map[x, y - 1] == 1)
        {
            long score = sscore + 1;
            if (solve[x, y - 1] == 0 || (score < solve[x, y - 1]))
            {
                solve[x, y - 1] = score;
                paths.Add((x, y - 1, score));
            }
        }
        paths.RemoveAt(0);
        paths = paths.Where(path => path.score < 20).ToList();
    }

    //ShowSolve(solve);
    solve[sx, sy] = 0;

    List<(int x, int y, int length)> result = [];

    for (int y = 1; y < ycount - 1; y++)
    {
        for (int x = 1; x < ycount - 1; x++)
        {
            if (map[x, y] == 0)
            {
                int minlength = int.MaxValue;
                if (solve[x + 1, y] > 0)
                    minlength = Math.Min(minlength, (int)solve[x + 1, y]);
                if (solve[x - 1, y] > 0)
                    minlength = Math.Min(minlength, (int)solve[x - 1, y]);
                if (solve[x, y + 1] > 0)
                    minlength = Math.Min(minlength, (int)solve[x, y + 1]);
                if (solve[x, y - 1] > 0)
                    minlength = Math.Min(minlength, (int)solve[x, y - 1]);

                if (minlength < int.MaxValue)
                    result.Add((x, y, minlength + 1));
            }
        }
    }

    //ShowPaths(result);

    return result;
}

List<(int x, int y)> GetPath()
{
    long[,] solve = new long[xcount, ycount];

    List<(int x, int y, long score)> paths = [(sx, sy, 1)];
    solve[sx, sy] = 1;

    // find best solution
    while (paths.Count > 0)
    {
        paths.Sort((l, r) => (int)(l.score - r.score));

        (int x, int y, long sscore) = paths[0];

        if (x < xcount - 1 && map[x + 1, y] == 0)
        {
            long score = sscore + 1;
            if (solve[x + 1, y] == 0 || (score < solve[x + 1, y]))
            {
                solve[x + 1, y] = score;

                paths.Add((x + 1, y, score));
            }
        }
        if (x > 0 && map[x - 1, y] == 0)
        {
            long score = sscore + 1;
            if (solve[x - 1, y] == 0 || (score < solve[x - 1, y]))
            {
                solve[x - 1, y] = score;

                paths.Add((x - 1, y, score));
            }
        }
        if (y < ycount - 1 && map[x, y + 1] == 0)
        {
            long score = sscore + 1;
            if (solve[x, y + 1] == 0 || (score < solve[x, y + 1]))
            {
                solve[x, y + 1] = score;

                paths.Add((x, y + 1, score));
            }
        }
        if (y > 0 && map[x, y - 1] == 0)
        {
            long score = sscore + 1;
            if (solve[x, y - 1] == 0 || (score < solve[x, y - 1]))
            {
                solve[x, y - 1] = score;
                paths.Add((x, y - 1, score));
            }
        }
        paths.RemoveAt(0);
    }

    List<(int x, int y)> res = new List<(int x, int y)>();
    for (int i = 0; i < (int)solve[ex, ey]; i++)
        res.Add((0, 0));

    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < xcount; x++)
        {
            if (solve[x, y] > 0)
            {
                res[(int)solve[x, y] - 1] = (x, y);
            }
        }
    }

    return res;
}

long ShortestPath(int cx1 = 0, int cy1 = 0, int cx2 = 0, int cy2 = 0, int length = 0)
{
    long[,] solve = new long[xcount, ycount];

    List<(int x, int y, long score)> paths = [(sx, sy, 1)];
    solve[sx, sy] = 1;

    // find best solution
    while (paths.Count > 0)
    {
        paths.Sort((l, r) => (int)(l.score - r.score));

        (int x, int y, long sscore) = paths[0];

        if (x == cx1 && y == cy1)
        {
            long score = sscore + length;
            if (solve[cx2, cy2] == 0 || (score < solve[cx2, cy2]))
            {
                solve[cx2, cy2] = score;

                paths.Add((cx2, cy2, score));
            }
        }
        if (x == cx2 && y == cy2)
        {
            long score = sscore + length;
            if (solve[cx1, cy1] == 0 || (score < solve[cx1, cy1]))
            {
                solve[cx1, cy1] = score;

                paths.Add((cx1, cy1, score));
            }
        }

        if (x < xcount - 1 && map[x + 1, y] == 0)
        {
            long score = sscore + 1;
            if (solve[x + 1, y] == 0 || (score < solve[x + 1, y]))
            {
                solve[x + 1, y] = score;

                paths.Add((x + 1, y, score));
            }
        }
        if (x > 0 && map[x - 1, y] == 0)
        {
            long score = sscore + 1;
            if (solve[x - 1, y] == 0 || (score < solve[x - 1, y]))
            {
                solve[x - 1, y] = score;

                paths.Add((x - 1, y, score));
            }
        }
        if (y < ycount - 1 && map[x, y + 1] == 0)
        {
            long score = sscore + 1;
            if (solve[x, y + 1] == 0 || (score < solve[x, y + 1]))
            {
                solve[x, y + 1] = score;

                paths.Add((x, y + 1, score));
            }
        }
        if (y > 0 && map[x, y - 1] == 0)
        {
            long score = sscore + 1;
            if (solve[x, y - 1] == 0 || (score < solve[x, y - 1]))
            {
                solve[x, y - 1] = score;
                paths.Add((x, y - 1, score));
            }
        }
        paths.RemoveAt(0);
    }

    if (solve[ex, ey] - 1 == 14)
    {
        ShowMap2(solve, cx1, cy1, cx2, cy2, length);
    }

    return solve[ex, ey] - 1;
    //Console.WriteLine($"total {solve[ex, ey]}");
}

void ShowMap2(long[,] solve, int cx1, int cy1, int cx2, int cy2, int length)
{
    Console.WriteLine($"{cx1}, {cy1} - {cx2}, {cy2} {length} ");

    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < xcount; x++)
        {
            if (map[x, y] == 0)
            {
                if (x == cx1 && y == cy1)
                    Console.Write($"s");
                else if (x == cx2 && y == cy2)
                    Console.Write($"e");
                else if (x == sx && y == sy)
                    Console.Write($"S");
                else if (x == ex && y == ey)
                    Console.Write($"E");
                else
                    Console.Write($".");
            }
            if (map[x, y] == 1)
                Console.Write("#");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

void ShowMap(int cx1, int cy1, int cx2, int cy2)
{
    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < xcount; x++)
        {
            if (map[x, y] == 0)
            {
                if (x == cx1 && y == cy1)
                    Console.Write($"s");
                else if (x == cx2 && y == cy2)
                    Console.Write($"e");
                else if (x == sx && y == sy)
                    Console.Write($"S");
                else if (x == ex && y == ey)
                    Console.Write($"E");
                else
                    Console.Write($".");
            }
            if (map[x, y] == 1)
                Console.Write("#");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

void ShowSolve(long[,] solve)
{
    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < xcount; x++)
        {
            if (map[x, y] == 0)
            {
                Console.Write($"...");
            }
            if (map[x, y] == 1)
            {
                if (solve[x, y] == 0)
                {
                    Console.Write("###");
                }
                else
                {
                    Console.Write($"{solve[x, y],3}");
                }
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

void ShowPaths(List<(int x, int y, int length)> result)
{
    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < xcount; x++)
        {
            if (map[x, y] == 0)
            {
                var r = result.Where(xx => xx.x == x && xx.y == y).Select(xx => xx.length).ToList();
                if (r.Count == 1)
                {
                    Console.Write($"{r[0],3}");
                }
                else
                {
                    Console.Write($"...");
                }
            }
            if (map[x, y] == 1)
            {
                Console.Write("###");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

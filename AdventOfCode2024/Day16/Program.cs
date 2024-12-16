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

List<(int x, int y, int dx, int dy, List<int> path, long score)> Paths = [(sx, sy, 1, 0, null, 1)];
solve[sx, sy] = 1;

// find best solution
while (Paths.Count > 0)
{
    Paths.Sort((l, r) => (int)(l.score - r.score));

    (int x, int y, int dx, int dy, List<int> p, long sscore) = Paths[0];

    if (map[x + 1, y] == 0)
    {
        long score = sscore;

        score = score + 1;
        if (dx != 1 || dy != 0)
            score += 1000;

        if (solve[x + 1, y] == 0 || (score < solve[x + 1, y]))
        {
            solve[x + 1, y] = score;

            Paths.Add((x + 1, y, 1, 0, null, score));
        }
    }
    if (map[x - 1, y] == 0)
    {
        long score = sscore;

        score = score + 1;
        if (dx != -1 || dy != 0)
            score += 1000;

        if (solve[x - 1, y] == 0 || (score < solve[x - 1, y]))
        {
            solve[x - 1, y] = score;

            Paths.Add((x - 1, y, -1, 0, null, score));
        }
    }
    if (map[x, y + 1] == 0)
    {
        long score = sscore;

        score = score + 1;
        if (dx != 0 || dy != 1)
            score += 1000;

        if (solve[x, y + 1] == 0 || (score < solve[x, y + 1]))
        {
            solve[x, y + 1] = score;


            Paths.Add((x, y + 1, 0, 1, null, score));
        }
    }
    if (map[x, y - 1] == 0)
    {
        long score = sscore;

        score = score + 1;
        if (dx != 0 || dy != -1)
            score += 1000;

        if (solve[x, y - 1] == 0 || (score < solve[x, y - 1]))
        {
            solve[x, y - 1] = score;
            Paths.Add((x, y - 1, 0, -1, null, score));
        }
    }
    Paths.RemoveAt(0);
}

Console.WriteLine($"total {solve[ex, ey]}");
long bestpath = solve[ex, ey];

Paths = [(sx, sy, 1, 0, [], 1)];
List<int> solution = [sx + 1000 * sy];

while (Paths.Count > 0)
{
    Paths.Sort((l, r) => (int)(l.score - r.score));

    (int x, int y, int dx, int dy, List<int> p, long sscore) = Paths[0];

    if (x == ex && y == ey && sscore == bestpath)
    {
        solution.AddRange(p);
    }

    if (map[x + 1, y] == 0 && !(dx == -1 && dy == 0))
    {
        long score = sscore;

        score = score + 1;
        if (dx != 1 || dy != 0)
            score += 1000;

        List<int> np = new(p);
        np.Add(x + 1 + y * 1000);

        if (score <= bestpath && score <= solve[x + 1, y] + 1001)
            Paths.Add((x + 1, y, 1, 0, np, score));
    }
    if (map[x - 1, y] == 0 && !(dx == 1 && dy == 0))
    {
        long score = sscore;

        score = score + 1;
        if (dx != -1 || dy != 0)
            score += 1000;

        List<int> np = new(p);
        np.Add(x - 1 + y * 1000);

        if (score <= bestpath && score <= solve[x - 1, y] + 1001)
            Paths.Add((x - 1, y, -1, 0, np, score));
    }
    if (map[x, y + 1] == 0)
    {
        long score = sscore;

        score = score + 1;
        if (dx != 0 || dy != 1)
            score += 1000;

        List<int> np = new(p);
        np.Add(x + (y + 1) * 1000);

        if (score <= bestpath && score <= solve[x, y + 1] + 1001)
            Paths.Add((x, y + 1, 0, 1, np, score));
    }
    if (map[x, y - 1] == 0)
    {
        long score = sscore;

        score = score + 1;
        if (dx != 0 || dy != -1)
            score += 1000;

        List<int> np = new(p);
        np.Add(x + (y - 1) * 1000);

        if (score <= bestpath && score <= solve[x, y - 1] + 1001)
            Paths.Add((x, y - 1, 0, -1, np, score));
    }

    //for (int yy = 0; yy < ycount; yy++)
    //{
    //    for (int xx = 0; xx < xcount; xx++)
    //    {
    //        if (map[xx, yy] == 0)
    //        {
    //            if (pp[xx, yy] is not null)
    //                Console.Write($"{pp[xx, yy].Count,3}");
    //            else
    //                Console.Write(" . ");
    //        }
    //        if (map[xx, yy] == 1)
    //            Console.Write("###");
    //    }
    //    Console.WriteLine();
    //}
    //Console.WriteLine();

    Paths.RemoveAt(0);
}



Console.WriteLine($"total {solve[ex, ey]}");
int pps = solution.Distinct().Count();

Console.WriteLine($"length {pps}");


//Console.WriteLine();
//for (int yy = 0; yy < ycount; yy++)
//{
//    for (int x = 0; x < xcount; x++)
//    {
//        if (map[x, yy] == 0)
//            Console.Write('.');
//        if (map[x, yy] == 1)
//            Console.Write('#');
//    }
//    Console.WriteLine();
//}
//Console.WriteLine();

//for (int y = 0; y < ycount; y++)
//{
//    for (int x = 0; x < xcount; x++)
//    {
//        if (solve[x, y] != 0)
//            Console.Write($"{solve[x, y],6}");
//        if (map[x, y] == 1)
//            Console.Write("      ");
//    }
//    Console.WriteLine();
//}
//Console.WriteLine();

for (int y = 0; y < ycount; y++)
{
    for (int x = 0; x < xcount; x++)
    {
        if (map[x, y] == 0)
            if (solution.Contains(x + y * 1000))
                Console.Write($"0");
            else
                Console.Write($".");
        if (map[x, y] == 1)
            Console.Write("#");
    }
    Console.WriteLine();
}
Console.WriteLine();

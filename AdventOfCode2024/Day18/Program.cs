var lines = File.ReadAllLines("data.txt");

int xcount = 71;
int ycount = 71;
int[,] map = new int[xcount, ycount];

int sx = 0;
int sy = 0;
int ex = xcount - 1;
int ey = ycount - 1;

var memitems = lines.Select(l => {
    var ws = l.Split(",");
    return (x: Convert.ToInt32(ws[0]), y: Convert.ToInt32(ws[1]));
}).ToList();

for (int i = 0; i < memitems.Count; i++)
{
    map[memitems[i].x, memitems[i].y] = 1;

    if (!CanSolve())
    {
        Console.WriteLine($"{i}: {memitems[i].x},{memitems[i].y}");
        break;
    }
}

bool CanSolve()
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


    return (solve[ex, ey] > 0);
    //Console.WriteLine($"total {solve[ex, ey]}");
}

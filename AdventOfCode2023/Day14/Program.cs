var lines = File.ReadAllLines("data.txt");

int nrSand = 0;
bool[,] map = new bool[1002, 1002];
int maxy = 0;

foreach (var line in lines)
{
    var coords = line.Split(" -> ");

    for (int i = 1; i < coords.Length; i++)
    {
        var p = coords[i - 1].Split(',').ToList().Select(Int32.Parse).ToList();
        int sx = p[0];
        int sy = p[1];

        p = coords[i].Split(',').ToList().Select(Int32.Parse).ToList();
        int ex = p[0];
        int ey = p[1];

        int dirx = Math.Sign(ex - sx);
        int diry = Math.Sign(ey - sy);

        int nrsteps = Math.Max(Math.Abs(ex - sx), Math.Abs(ey - sy));

        for (int s = 0; s <= nrsteps; s++)
        {
            map[sx + dirx * s, sy + diry * s] = true;
        }

        maxy = Math.Max(maxy, Math.Max(ey, sy));
    }
}

Console.WriteLine(maxy);

for (int i = 0; i < 1000; i++)
{
    map[i, maxy + 2] = true;
}

Display(500, 10);

while (true)
{
    int sandX = 500;
    int sandY = 0;

    while (true)
    {
        if (map[500, 0])
            break;

        if (map[sandX, sandY + 1] == false)
        {
            // down
            sandY++;
        }
        else if (map[sandX - 1, sandY + 1] == false)
        {
            // down and left
            sandX--;
            sandY++;
        }
        else if (map[sandX + 1, sandY + 1] == false)
        {
            // down and rightt
            sandX++;
            sandY++;
        }
        else
        {
            // at rest 
            map[sandX, sandY] = true;
            break;
        }

        if (sandY >= 1000)
            break;
    }

    if (map[500, 0])
        break;

    if (sandY >= 1000)
        break;

    //Display(500, 10);
    nrSand++;
}


void Display(int sx, int sy)
{
    for (int y = sy - 10; y < sy + 10; y++)
    {
        for (int x = sx - 10; x < sx + 10; x++)
        {
            if (map[x, y])
                Console.Write("X");
            else
                Console.Write(".");
        }
        Console.WriteLine();
    }

    Console.WriteLine();
}

Display(500, 10);

Console.WriteLine($"total {nrSand}");

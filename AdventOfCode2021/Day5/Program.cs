var lines = File.ReadAllLines("data.txt");

var map = new int[1000, 1000];

foreach (var line in lines)
{
    var coor = line.Split(" -> ");
    int x1 = Int32.Parse(coor[0].Split(",")[0]);
    int y1 = Int32.Parse(coor[0].Split(",")[1]);
    int x2 = Int32.Parse(coor[1].Split(",")[0]);
    int y2 = Int32.Parse(coor[1].Split(",")[1]);

    if (x1 == x2)
    {
        for (int y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
            map[x1, y]++;
    }
    else if (y1 == y2)
    {
        for (int x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
            map[x, y1]++;
    }
    else
    {
        int size = Math.Abs(x1 - x2);
        int dirx = Math.Sign(x2 - x1);
        int diry = Math.Sign(y2 - y1);
        foreach (var step in Enumerable.Range(0, size + 1))
            map[x1 + dirx * step, y1 + diry * step]++;
    }
}

int sum = 0;
for (int x = 0; x < 1000; x++)
{
    for (int y = 0; y < 1000; y++)
    {
        if (map[x, y] > 1)
            sum++;
    }
}

Console.WriteLine($"Fields with 2+ = {sum}");

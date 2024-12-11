
var lines = File.ReadAllLines("data.txt");

int[,] map = new int[lines[0].Length, lines.Length];
List<int> paths = [];

int xcount = lines[0].Length;
int ycount = lines.Length;

int yy = 0;
foreach (var line in lines)
{
    for (int x = 0; x < line.Length; x++)
    {
        map[x, yy] = line[x] - '0';
    }
    yy++;
}

int sumtrails = 0;
int sumtrails2 = 0;
for (int y = 0; y < ycount; y++)
{
    for (int x = 0; x < xcount; x++)
    {
        if (map[x, y] == 0)
        {
            sumtrails += CountTrails(x, y);
            sumtrails2 += CountTrails2(x, y);
            //Console.WriteLine($" {x}, {y}  = {CountTrails(x, y)}");
        }
    }
}

Console.WriteLine($"total {sumtrails}");
Console.WriteLine($"total2 {sumtrails2}");



int CountTrails(int x, int y, int val = 0)
{
    if (val == 0)
        paths = [];

    if (val == 9)
    {
        if (!paths.Contains(x + y * 1000))
            paths.Add(x + y * 1000);
    }

    var res = 0;

    if (x > 0 && map[x - 1, y] == val + 1)
    {
        res += CountTrails(x - 1, y, val + 1);
    }

    if (x < (xcount - 1) && map[x + 1, y] == val + 1)
    {
        res += CountTrails(x + 1, y, val + 1);
    }

    if (y > 0 && map[x, y - 1] == val + 1)
    {
        res += CountTrails(x, y - 1, val + 1);
    }

    if (y < (ycount - 1) && map[x, y + 1] == val + 1)
    {
        res += CountTrails(x, y + 1, val + 1);
    }

    return paths.Count;
}

int CountTrails2(int x, int y, int val = 0)
{
    if (val == 9)
        return 1;

    var res = 0;

    if (x > 0 && map[x - 1, y] == val + 1)
    {
        res += CountTrails2(x - 1, y, val + 1);
    }

    if (x < (xcount - 1) && map[x + 1, y] == val + 1)
    {
        res += CountTrails2(x + 1, y, val + 1);
    }

    if (y > 0 && map[x, y - 1] == val + 1)
    {
        res += CountTrails2(x, y - 1, val + 1);
    }

    if (y < (ycount - 1) && map[x, y + 1] == val + 1)
    {
        res += CountTrails2(x, y + 1, val + 1);
    }

    return res;
}

using Day18;

var lines = File.ReadAllLines("data.txt");

List<Point3> points = new();

foreach (var line in lines)
{
    var coords = line.Split(',');

    points.Add(new()
    {
        X = int.Parse(coords[0]),
        Y = int.Parse(coords[1]),
        Z = int.Parse(coords[2]),
    });
}

foreach (var p in points)
{
    p.Sides = 6;
    if (points.Any(sp => sp.X == p.X && sp.Y == p.Y && sp.Z == p.Z + 1))
        p.Sides--;

    if (points.Any(sp => sp.X == p.X && sp.Y == p.Y && sp.Z == p.Z - 1))
        p.Sides--;

    if (points.Any(sp => sp.X == p.X && sp.Y == p.Y - 1 && sp.Z == p.Z))
        p.Sides--;

    if (points.Any(sp => sp.X == p.X && sp.Y == p.Y + 1 && sp.Z == p.Z))
        p.Sides--;

    if (points.Any(sp => sp.X == p.X - 1 && sp.Y == p.Y && sp.Z == p.Z))
        p.Sides--;

    if (points.Any(sp => sp.X == p.X + 1 && sp.Y == p.Y && sp.Z == p.Z))
        p.Sides--;
}

Console.WriteLine($"{points.Sum(p => p.Sides)}");

int maxx = points.Max(sp => sp.X);
int maxy = points.Max(sp => sp.Y);
int maxz = points.Max(p => p.Z);

int[,,] map = new int[maxx + 3, maxy + 3, maxz + 3];

foreach (var p in points)
{
    map[p.X + 1, p.Y + 1, p.Z + 1] = 1;
}

int count = 0;

for (int x = 0; x < maxx + 2; x++)
    for (int y = 0; y < maxy + 2; y++)
        for (int z = 0; z < maxz + 2; z++)
        {
            if (map[x, y, z] == 1)
            {
                if (x > 0 && map[x - 1, y, z] == 0)
                {
                    count++;
                    //Console.WriteLine($"{x} {y} {z} x-");
                }
                if (x < maxx + 2 && map[x + 1, y, z] == 0)
                {
                    count++;
                    //Console.WriteLine($"{x} {y} {z} x+");
                }
                if (y > 0 && map[x, y - 1, z] == 0)
                    count++;
                if (y < maxy + 2 && map[x, y + 1, z] == 0)
                    count++;
                if (z > 0 && map[x, y, z - 1] == 0)
                    count++;
                if (z < maxz + 2 && map[x, y, z + 1] == 0)
                    count++;
            }
        }

Console.WriteLine($"count {count}");

int added = 1;
map[0, 0, 0] = 2;

while (added > 0)
{
    added = 0;

    for (int x = 0; x < maxx + 3; x++)
        for (int y = 0; y < maxy + 3; y++)
            for (int z = 0; z < maxz + 3; z++)
            {
                if (map[x, y, z] == 0)
                {
                    if (x > 0 && map[x - 1, y, z] == 2)
                    {
                        map[x, y, z] = 2;
                        added++;
                    }
                    else if (x < maxx + 2 && map[x + 1, y, z] == 2)
                    {
                        map[x, y, z] = 2;
                        added++;
                    }
                    else if (y > 0 && map[x, y - 1, z] == 2)
                    {
                        map[x, y, z] = 2;
                        added++;
                    }
                    else if (y < maxy + 2 && map[x, y + 1, z] == 2)
                    {
                        map[x, y, z] = 2;
                        added++;
                    }
                    else if (z > 0 && map[x, y, z - 1] == 2)
                    {
                        map[x, y, z] = 2;
                        added++;
                    }
                    else if (z < maxz + 2 && map[x, y, z + 1] == 2)
                    {
                        map[x, y, z] = 2;
                        added++;
                    }
                }
            }

    Console.WriteLine($"added {added}");
}

count = 0;
for (int x = 0; x < maxx + 2; x++)
    for (int y = 0; y < maxy + 2; y++)
        for (int z = 0; z < maxz + 2; z++)
        {
            if (map[x, y, z] == 1)
            {
                if (x > 0 && map[x - 1, y, z] == 2)
                {
                    count++;
                    //Console.WriteLine($"{x} {y} {z} x-");
                }
                if (x < maxx + 2 && map[x + 1, y, z] == 2)
                {
                    count++;
                    //Console.WriteLine($"{x} {y} {z} x+");
                }
                if (y > 0 && map[x, y - 1, z] == 2)
                    count++;
                if (y < maxy + 2 && map[x, y + 1, z] == 2)
                    count++;
                if (z > 0 && map[x, y, z - 1] == 2)
                    count++;
                if (z < maxz + 2 && map[x, y, z + 1] == 2)
                    count++;
            }
        }

Console.WriteLine($"count {count}");

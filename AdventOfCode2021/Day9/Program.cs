var lines = File.ReadAllLines("data.txt");

var heightmap = lines.Select(l => l.ToCharArray().Select(c => (int)Char.GetNumericValue(c)).ToArray()).ToArray();

int maxx = heightmap.Length;
int maxy = heightmap[0].Length;

var basin = new int[maxx, maxy];
var dest = new int[maxx, maxy];

bool action = true;
while (action)
{
    action = false;

    for (int x = 0; x < maxx; x++)
    {
        for (int y = 0; y < maxy; y++)
        {
            if (heightmap[x][y] == 9)
                continue;

            if (dest[x, y] > 0)
                continue;

            var lowerDir = new List<int>();

            if (x > 0 && heightmap[x][y] > heightmap[x - 1][y])
            {
                lowerDir.Add(100 * (x - 1) + y);
            }

            if (x < maxx - 1 && heightmap[x][y] > heightmap[x + 1][y])
            {
                lowerDir.Add(100 * (x + 1) + y);
            }

            if (y > 0 && heightmap[x][y] > heightmap[x][y - 1])
            {
                lowerDir.Add(100 * x + y - 1);
            }

            if (y < maxy - 1 && heightmap[x][y] > heightmap[x][y + 1])
            {
                lowerDir.Add(100 * x + y + 1);
            }

            if (lowerDir.Count == 0)
            {
                basin[x, y]++;
                dest[x, y] = x * 100 + y;
                action = true;
                continue;
            }

            if (lowerDir.Count > 0)
            {
                var d = dest[lowerDir[0] / 100, lowerDir[0] % 100];

                if (lowerDir.Count != lowerDir.Count(x => d == dest[x / 100, x % 100]))
                    continue;

                if (dest[lowerDir[0] / 100, lowerDir[0] % 100] > 0)
                {
                    dest[x, y] = dest[lowerDir[0] / 100, lowerDir[0] % 100];
                    basin[dest[x, y] / 100, dest[x, y] % 100]++;
                    action = true;
                }
            }
        }
    }


    //for (int x = 0; x < maxx; x++)
    //{
    //    for (int y = 0; y < maxy; y++)
    //    {
    //        if (basin[x, y] == 0 && dest[x, y] > 0)
    //            Console.Write("  .");
    //        else
    //            Console.Write($"{basin[x, y].ToString().PadLeft(3)}");
    //    }
    //    Console.WriteLine();
    //}

    //Console.WriteLine();
}

for (int x = 0; x < maxx; x++)
{
    for (int y = 0; y < maxy; y++)
    {
        if (basin[x, y] == 0 && dest[x, y] > 0)
            Console.Write("  .");
        else
            Console.Write($"{basin[x, y].ToString().PadLeft(3)}");
    }
    Console.WriteLine();
}

var basinSize = new List<int>();

for (int x = 0; x < maxx; x++)
{
    for (int y = 0; y < maxy; y++)
    {
        if (basin[x, y] > 0)
            basinSize.Add(basin[x, y]);
    }
}
basinSize.Sort();
basinSize.Reverse();

Console.WriteLine($"Basin mult = {basinSize[0] * basinSize[1] * basinSize[2]}");


int sum = 0;
for (int x = 0; x < maxx; x++)
{
    for (int y = 0; y < maxy; y++)
    {
        if (x > 0 && heightmap[x][y] >= heightmap[x - 1][y])
            continue;

        if (x < maxx - 1 && heightmap[x][y] >= heightmap[x + 1][y])
            continue;

        if (y > 0 && heightmap[x][y] >= heightmap[x][y - 1])
            continue;

        if (y < maxy - 1 && heightmap[x][y] >= heightmap[x][y + 1])
            continue;

        //Console.WriteLine($"{x} {y} {heightmap[x][y]}");
        sum += heightmap[x][y] + 1;
    }
}

Console.WriteLine(sum);

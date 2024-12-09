var lines = File.ReadAllLines("data.txt");

int xcount = lines[0].Length;
int ycount = lines.Length;


Dictionary<char, List<int>> stations = [];

foreach ((string line, int y) in lines.Select((l, y) => (l, y)))
{
    foreach ((char c, int x) in line.Select((c, x) => (c, x)))
    {
        if (c == '.')
            continue;

        if (stations.ContainsKey(c))
        {
            stations[c].Add(y * 1000 + x);
        }
        else
        {
            stations[c] = [y * 1000 + x];
        }
    }
}

foreach (var c in stations.Keys)
{
    Console.WriteLine(c);
    Console.WriteLine(string.Join("", stations[c].Select(x => $"({x % 1000},{x / 1000})")));
}

List<int> antinodes = [];
foreach (var c in stations.Keys)
{
    var poslist = stations[c];
    for (int i = 0; i < poslist.Count - 1; i++)
    {
        for (int j = i + 1; j < poslist.Count; j++)
        {
            int x1 = poslist[i] % 1000;
            int y1 = poslist[i] / 1000;
            int x2 = poslist[j] % 1000;
            int y2 = poslist[j] / 1000;

            if (!antinodes.Contains(y1 * 1000 + x1))
            {
                antinodes.Add(y1 * 1000 + x1);
            }

            if (!antinodes.Contains(y2 * 1000 + x2))
            {
                antinodes.Add(y2 * 1000 + x2);
            }

            for (int mult = 1; ; mult++)
            {
                int ax = x1 + mult * (x1 - x2);
                int ay = y1 + mult * (y1 - y2);

                if (ax < 0 || ax >= xcount
                     || ay < 0 || ay >= ycount)
                {
                    break;
                }

                int a = ay * 1000 + ax;
                if (!antinodes.Contains(a))
                {
                    antinodes.Add(a);
                }
            }

            for (int mult = 1; ; mult++)
            {
                int ax = x2 + mult * (x2 - x1);
                int ay = y2 + mult * (y2 - y1);

                if (ax < 0 || ax >= xcount
                     || ay < 0 || ay >= ycount)
                {
                    break;
                }

                int a = ay * 1000 + ax;
                if (!antinodes.Contains(a))
                {
                    antinodes.Add(a);
                }
            }
        }
    }
}

for (int y = 0; y < ycount; y++)
{
    for (int x = 0; x < xcount; x++)
    {
        if (antinodes.Contains(y * 1000 + x))
            Console.Write('X');
        else
            Console.Write('.');
    }
    Console.WriteLine();
}
Console.WriteLine();

Console.WriteLine($"count  {antinodes.Count}");

var lines = File.ReadAllLines("data.txt");

List<List<int>> locks = [];
List<List<int>> keys = [];

bool iskey = false;
List<int> current = [-1, -1, -1, -1, -1];

foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        if (iskey)
            keys.Add(current);
        else
            locks.Add(current);

        current = [-1, -1, -1, -1, -1];

        continue;
    }

    if (line.StartsWith("#####"))
    {
        iskey = true;
    }

    if (line.StartsWith("....."))
    {
        iskey = false;
    }

    for (int i = 0; i < 5; i++)
    {
        if (line[i] == '#')
            current[i]++;
    }

}

if (iskey)
    keys.Add(current);
else
    locks.Add(current);

foreach (var l in locks)
    Console.WriteLine(string.Join(',', l.Select(i => i.ToString())));

Console.WriteLine();
foreach (var k in keys)
    Console.WriteLine(string.Join(',', k.Select(i => i.ToString())));

int count = 0;
foreach (var l in locks)
{
    foreach (var k in keys)
    {
        bool fit = true;

        for (int i = 0; i < 5; i++)
        {
            if (l[i] + k[i] > 5)
                fit = false;
        }

        if (fit)
            count++;
    }
}

Console.WriteLine();
Console.WriteLine($"Nr Fit {count}");

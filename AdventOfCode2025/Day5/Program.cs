var lines = File.ReadAllLines("data.txt");


List<(long, long)> rules = [];

foreach (var line in lines)
{
    if (line.Contains('-'))
    {
        var words = line.Split('-');
        rules.Add((Convert.ToInt64(words[0]), Convert.ToInt64(words[1])));
    }
}

int cnt = 0;

foreach (var line in lines)
{
    if (!line.Contains('-') && line.Length > 0)
    {
        long val = Convert.ToInt64(line);
        bool spoiled = true;
        foreach (var rule in rules)
        {
            if (val >= rule.Item1 && val <= rule.Item2)
            {
                spoiled = false;
                break;
            }
        }

        if (!spoiled)
        {
            cnt++;
        }
    }
}


Console.WriteLine($"sumorder {cnt}");

bool found = true;
while (found)
{
    found = false;
    for (int i = 0; i < rules.Count; i++)
    {
        for (int j = 0; j < rules.Count; j++)
        {
            if (i == j)
                continue;

            if ((rules[j].Item1 >= rules[i].Item1 && rules[j].Item1 <= rules[i].Item2)
                || (rules[j].Item2 >= rules[i].Item1 && rules[j].Item2 <= rules[i].Item2))

            {
                rules[i] = (Math.Min(rules[i].Item1, rules[j].Item1),
                            Math.Max(rules[i].Item2, rules[j].Item2));
                rules.RemoveAt(j);
                found = true;
                break;
            }
        }
    }
}

cnt = 0;
foreach (var line in lines)
{
    if (!line.Contains('-') && line.Length > 0)
    {
        long val = Convert.ToInt64(line);
        bool spoiled = true;
        foreach (var rule in rules)
        {
            if (val >= rule.Item1 && val <= rule.Item2)
            {
                spoiled = false;
                break;
            }
        }

        if (!spoiled)
        {
            cnt++;
        }
    }
}


Console.WriteLine($"sumorder {cnt}");


Console.WriteLine($"rule {rules.Sum(r => r.Item2 - r.Item1 + 1)}");

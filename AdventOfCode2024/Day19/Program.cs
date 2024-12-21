
var lines = File.ReadAllLines("data.txt");

List<string> options = lines[0].Split(", ").ToList();
List<string> patterns = lines.Skip(2).ToList();

Dictionary<string, long> buffer = new() { { "", 1L } };

int makeables = 0;
long nrmake = 0;
foreach (var pattern in patterns)
{
    if (CanMake(pattern))
    {
        makeables++;
        long count = NrMake(pattern);
        Console.WriteLine($"{patterns.IndexOf(pattern)} {count}");
        nrmake += count;
    }
}

bool CanMake(string pattern)
{
    if (options.Contains(pattern))
        return true;

    foreach (string option in options)
    {
        if (pattern.StartsWith(option))
        {
            if (CanMake(pattern[(option.Length..)]))
                return true;
        }
    }

    return false;
}



long NrMake(string pattern)
{
    if (buffer.TryGetValue(pattern, out long value))
    {
        return value;
    }

    long count = 0;
    foreach (string option in options)
    {
        if (pattern.StartsWith(option))
        {
            count += NrMake(pattern[(option.Length..)]);
        }
    }

    buffer.Add(pattern, count);

    return count;
}

Console.WriteLine(makeables);
Console.WriteLine(nrmake);

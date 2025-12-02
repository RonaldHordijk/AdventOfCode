var lines = File.ReadAllLines("data.txt");


long sum = 0;
long sum2 = 0;

foreach (var line in lines)
{
    var seq = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
    foreach (var s in seq)
    {
        var words = s.Split('-', StringSplitOptions.RemoveEmptyEntries);
        long first = long.Parse(words[0]);
        long last = long.Parse(words[1]);

        for (long l = first; l <= last; l++)
        {
            if (isRepeat(l))
                sum += l;
            if (isRepeat2(l))
                sum2 += l;
        }
    }
}

bool isRepeat(long value)
{
    string s = value.ToString();
    if (s.Length % 2 == 1)
        return false;

    return (s[..(s.Length / 2)] == s[(s.Length / 2)..]);
}

bool isRepeat2(long value)
{
    string s = value.ToString();

    for (int step = 1; step < s.Length; step++)
    {
        if (s.Length % step != 0)
            continue;

        bool allSame = true;

        string start = s[..step];
        for (int i = step; i < s.Length; i += step)
        {
            allSame = allSame && (s[i..(i + step)] == start);
        }

        if (allSame)
            return true;

    }

    return false;
}

Console.WriteLine($"total {sum}");
Console.WriteLine($"total2 {sum2}");

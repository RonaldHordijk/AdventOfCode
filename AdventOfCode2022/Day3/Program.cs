var lines = File.ReadAllLines("data.txt");

var sum = 0;

foreach (var line in lines)
{
    var dub = GetDouble(line.Substring(0, line.Length / 2), line.Substring(line.Length / 2));
    sum += GetValue(dub);
}
Console.WriteLine($"Total Score {sum}");

sum = 0;
for (int i = 0; i < lines.Length; i += 3)
{
    var dub = GetCommon(lines[i], lines[i + 1], lines[i + 2]);
    sum += GetValue(dub);
}
Console.WriteLine($"Total Score badges {sum}");

int GetValue(char dub)
{
    if (Char.IsUpper(dub))
    {
        return dub - 'A' + 27;
    }
    else
    {
        return dub - 'a' + 1;
    }
}

char GetDouble(string v1, string v2)
{
    return v1.ToCharArray().First(c => v2.Contains(c));
}

char GetCommon(string v1, string v2, string v3)
{
    return v1.ToCharArray().First(c => v2.Contains(c) && v3.Contains(c));
}

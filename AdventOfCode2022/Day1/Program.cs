var lines = File.ReadAllLines("data.txt");

Int64 som = 0;
Int64 maxSom = 0;
List<Int64> sums = new List<Int64>();

foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        sums.Add(som);
        som = 0;
    }
    else
    {
        som += Int64.Parse(line);
    }
}

sums.Sort();
sums.Reverse();
var total = sums.Take(3).Sum();

Console.WriteLine($"Max {sums[0]}");
Console.WriteLine($"Max {total}");


var lines = File.ReadAllLines("dataTest.txt");


long sum = 0;
long sum2 = 0;

foreach (var line in lines)
{
    var values = line.Select(c => (int)(c - '0')).ToList();
    int first = values.Take(values.Count - 1).Max();
    int firstIndex = values.IndexOf(first);
    int second = values.Skip(firstIndex + 1).Max();
    sum += first * 10 + second;

    long v = GetMax(values, 12);
    Console.WriteLine($"value {v}");
    sum2 += v;
}

long GetMax(List<int> values, int count)
{
    if (count == 0)
    {
        return 0;
    }

    if (values.Count == count)
    {
        return (long)(Math.Pow(10, count - 1) * values[0]) + GetMax(values.Skip(1).ToList(), count - 1);
    }

    int first = values.Take(values.Count - count + 1).Max();
    int firstIndex = values.IndexOf(first);
    return (long)(Math.Pow(10, count - 1) * first) + GetMax(values.Skip(firstIndex + 1).ToList(), count - 1);

}

Console.WriteLine($"sum {sum}");
Console.WriteLine($"sum2 {sum2}");


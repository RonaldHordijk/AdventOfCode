
var lines = File.ReadAllLines("data.txt");

long sum = 0;
long sumFirst = 0;

foreach (var line in lines)
{
    var values = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => Int64.Parse(s)).ToList();
    List<long> last = [values[^1]];
    List<long> first = [values[0]];

    while (values.Any(v => v != 0))
    {
        values = GetSum(values);
        last.Add(values[^1]);
        first.Add(values[0]);
    }

    long fv = 0;
    for (int i = 0; i < first.Count; i++)
    {
        fv = fv + first[i] * (i % 2 == 0 ? 1 : -1);
    }


    //Console.WriteLine(fv);
    sumFirst += fv;
    sum += last.Sum();
}

Console.WriteLine(sum);
Console.WriteLine(sumFirst);

List<long> GetSum(List<long> values)
{
    List<long> result = [];
    for (int i = 1; i < values.Count; i++)
    {
        result.Add(values[i] - values[i - 1]);
    }

    return result;
}

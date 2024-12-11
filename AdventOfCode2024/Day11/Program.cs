var lines = File.ReadAllLines("data.txt");

var words = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
var values = words.Select(w => Convert.ToInt64(w)).ToList();

var input = DoNrRuns(values, 25);

Console.WriteLine(input.Count);

List<Dictionary<long, long>> buffer = [];
for (int i = 0; i < 76; i++)
{
    buffer.Add([]);
}

long sum3 = 0;
foreach (var value in values)
{
    sum3 += GetSum(value, 75);
}
Console.WriteLine($"sums {sum3}");

long GetSum(long value, int depth)
{
    if (depth == 0)
        return 1;

    if (buffer[depth].TryGetValue(value, out long found))
    {
        return found;
    }

    long res = 0;
    if (value == 0)
    {
        res = GetSum(1, depth - 1);
    }
    else
    {
        string s = value.ToString();
        if (s.Length % 2 == 0)
        {
            res = GetSum(Convert.ToInt64(s[..(s.Length / 2)]), depth - 1)
                  + GetSum(Convert.ToInt64(s[(s.Length / 2)..]), depth - 1);
        }
        else
        {
            res = GetSum(2024 * value, depth - 1);
        }
    }

    buffer[depth].Add(value, res);
    return res;
}



Dictionary<long, long> buffer50 = [];
Dictionary<long, long> buffer25 = [];
long sum = 0;
int count = 0;
foreach (var value in input)
{
    count++;
    if (count % 1000 == 0)
        Console.WriteLine(count);

    if (buffer50.TryGetValue(value, out long found50))
    {
        sum += found50;
    }
    else
    {
        long sum2 = 0;
        var input2 = DoNrRuns([value], 25);
        foreach (var value2 in input2)
        {
            if (buffer25.TryGetValue(value2, out long found25))
            {
                sum2 += found25;
            }
            else
            {
                long count25 = (long)DoNrRuns([value2], 25).Count;
                buffer25.Add(value2, count25);
                sum2 += count25;
            }
        }

        buffer50.Add(value, sum2);
        sum += sum2;
    }
}

Console.WriteLine(sum);

static List<long> DoNrRuns(List<long> values, int count)
{
    var input = values;
    for (int i = 0; i < count; i++)
    {
        List<long> output = [];

        foreach (long v in input)
        {
            if (v == 0)
            {
                output.Add(1);
            }
            else
            {
                string s = v.ToString();
                if (s.Length % 2 == 0)
                {
                    output.Add(Convert.ToInt64(s[..(s.Length / 2)]));
                    output.Add(Convert.ToInt64(s[(s.Length / 2)..]));
                }
                else
                {
                    output.Add(v * 2024);
                }
            }
        }
        input = output;
    }
    return input;
}

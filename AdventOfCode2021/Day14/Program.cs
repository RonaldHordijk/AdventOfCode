using System.Linq;
using System.Text;

var lines = File.ReadAllLines("data.txt");


string work = string.Empty;
Dictionary<string, string> expander = new();

foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
        continue;

    if (!line.Contains("->"))
    {
        work = line;
        continue;
    }

    var w = line.Split(" -> ");
    expander.Add(w[0], w[0][0] + w[1]);
}

string work2 = work;
for (int step = 0; step < 10; step++)
{
    var sb = new StringBuilder();
    for (int i = 0; i < work.Length - 1; i++)
    {
        string s = work.Substring(i, 2);
        if (expander.ContainsKey(s))
        {
            sb.Append(expander[s]);
        }
        else
        {
            sb.Append(work[i]);
        }
    }
    sb.Append(work[^1]);


    work = sb.ToString();
    //Console.WriteLine(work);
}

Dictionary<string, Dictionary<Char, Int64>> start = new();
Dictionary<string, Dictionary<Char, Int64>> counter = new();
Dictionary<string, Dictionary<Char, Int64>> counterNext = new();
// initial version
foreach (var kv in expander)
{
    start.Add(kv.Key, new Dictionary<Char, Int64> { { kv.Value[1], 1 } });
}
counter = start;

// do steps
for (int i = 0; i < 39; i++)
{
    foreach (var kv in expander)
    {
        var c = start[kv.Key];

        string s1 = kv.Key[0].ToString() + kv.Value[1];
        Dictionary<Char, Int64> v1 = null;
        if (counter.ContainsKey(s1))
            v1 = counter[s1];

        string s2 = kv.Value[1] + kv.Key[1].ToString();
        Dictionary<Char, Int64> v2 = null;
        if (counter.ContainsKey(s2))
            v2 = counter[s2];

        var allChar = c.Keys.ToList();
        allChar.AddRange(v1.Keys.ToList());
        allChar.AddRange(v2.Keys.ToList());
        allChar = allChar.Distinct().ToList();

        Dictionary<Char, Int64> sum = new();
        foreach (char ch in allChar)
        {
            Int64 sc = 0;
            if (c.ContainsKey(ch))
                sc = c[ch];
            if (v1 is not null && v1.ContainsKey(ch))
                sc += v1[ch];
            if (v2 is not null && v2.ContainsKey(ch))
                sc += v2[ch];
            sum.Add(ch, sc);
        }

        counterNext.Add(kv.Key, sum);
    }

    counter = counterNext;
    counterNext = new();
}

Dictionary<Char, Int64> sumchar = new();

foreach (var ch in work2)
{
    if (sumchar.ContainsKey(ch))
    {
        sumchar[ch]++;
    }
    else
    {
        sumchar.Add(ch, 1);
    }
}


for (int i = 0; i < work2.Length - 1; i++)
{
    string s = work2.Substring(i, 2);
    if (counter.ContainsKey(s))
    {
        var items = counter[s];
        foreach (var kv in items)
        {
            if (sumchar.ContainsKey(kv.Key))
            {
                sumchar[kv.Key] += kv.Value;
            }
            else
            {
                sumchar.Add(kv.Key, kv.Value);
            }
        }
    }
}

Int64 ss = 0;
foreach (var kv in sumchar)
{
    Console.WriteLine($"{kv.Key} {kv.Value}");
    ss += kv.Value;
}
Console.WriteLine(ss);



Console.WriteLine(work.Length);
var counts = work.GroupBy(c => c).Select(c => c.Count()).OrderBy(x => x).ToList();
Console.WriteLine(counts.Last() - counts.First());

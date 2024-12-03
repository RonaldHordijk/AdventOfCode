using System.Text.RegularExpressions;

var lines = File.ReadAllLines("data.txt");

Regex r = new Regex(@"mul\((\d+),(\d+)\)", RegexOptions.IgnoreCase);

long sum = 0;

foreach (var line in lines)
{
    Match m = r.Match(line);

    while (m.Success)
    {
        long v1 = Convert.ToInt64(m.Groups[1].Value);
        long v2 = Convert.ToInt64(m.Groups[2].Value);
        sum += v1 * v2;
        m = m.NextMatch();
    }
}

Console.WriteLine($"sum {sum}");

long sum2 = 0;

string work = String.Join("", lines);
while (work.Length > 0)
{
    int end = work.IndexOf("don't()");
    if (end < 0)
    {
        end = work.Length;
    }
    else
    {
        end += 6;
    }

    string enabled = work.Substring(0, end);
    work = work[end..];
    Match m = r.Match(enabled);

    while (m.Success)
    {
        long v1 = Convert.ToInt64(m.Groups[1].Value);
        long v2 = Convert.ToInt64(m.Groups[2].Value);
        sum2 += v1 * v2;
        m = m.NextMatch();
    }

    // disabeld
    end = work.IndexOf("do()");
    if (end < 0)
    {
        end = work.Length;
    }
    else
    {
        end += 4;
    }
    work = work[end..];
}


Console.WriteLine($"sum2 {sum2}");

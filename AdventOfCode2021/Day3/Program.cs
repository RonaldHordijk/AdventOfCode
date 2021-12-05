var lines = File.ReadAllLines("data.txt");

var ones = new List<int>();

for (int i = 0; i < lines[0].Length; i++)
    ones.Add(0);

foreach (var l in lines)
{
    for (int i = 0; i < l.Length; i++)
    {
        if (l[i] == '1')
            ones[i] += 1;
    }
}

string s1 = string.Empty;
string s2 = string.Empty;

for (int i = 0; i < lines[0].Length; i++)
{
    if (2 * ones[i] > lines.Length)
    {
        s1 += "1";
        s2 += "0";
    }
    else
    {
        s1 += "0";
        s2 += "1";
    }
}

var v1 = Convert.ToInt32(s1, 2);
var v2 = Convert.ToInt32(s2, 2);

Console.WriteLine($" values are {v1}, {v2}. product is {v1 * v2}");


var copy = new List<string>();
copy.AddRange(lines);

for (int location = 0; location < lines[0].Length; location++)
{
    var c = copy.Count(s => s[location] == '1');
    if (2 * c >= copy.Count)
    {
        copy = copy.Where(s => s[location] == '1').ToList();
    }
    else
    {
        copy = copy.Where(s => s[location] == '0').ToList();
    }
}

v1 = Convert.ToInt32(copy[0], 2);

copy = new List<string>();
copy.AddRange(lines);

for (int location = 0; location < lines[0].Length; location++)
{
    var c = copy.Count(s => s[location] == '1');
    if (2 * c >= copy.Count && (c != copy.Count))
    {
        copy = copy.Where(s => s[location] == '0').ToList();
    }
    else
    {
        copy = copy.Where(s => s[location] == '1').ToList();
    }

    if (copy.Count == 1)
        break;
}

v2 = Convert.ToInt32(copy[0], 2);

Console.WriteLine($" values are {v1}, {v2}. product is {v1 * v2}");

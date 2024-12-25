var lines = File.ReadAllLines("data.txt");

SortedDictionary<string, int> values = [];
List<Function> functions = [];

foreach (var line in lines)
{
    if (line.Contains(':'))
    {
        var words = line.Split(':');

        values.Add(words[0], Convert.ToInt32(words[1]));
    }
    else if (line.Contains("->"))
    {
        var words = line.Split(' ');

        functions.Add(new Function(words[0], words[2], words[1], words[4]));
    }
}

Init(0, 0);

Run(values, functions);


for (int i = 0; i < 46; i++)
{
    var list = GetDependants($"z{(i),2:00}");
    list.Sort();
    Console.WriteLine($"{i} {list.Distinct().Count()}");
    //Console.WriteLine(string.Join(" ", list.Distinct()));
}

List<string> GetDependants(string start)
{
    List<string> result = [start];

    foreach (var func in functions)
    {
        if (func.Result == start)
        {
            result.AddRange(GetDependants(func.Val1));
            result.AddRange(GetDependants(func.Val2));
        }
    }

    return result;
}

WriteSum(values);
WriteTotal(values);


void switchfunc(string v1, string v2)
{
    foreach (var function in functions)
    {
        if (function.Result == v1)
        {
            function.Result = v2;
        }
        else if (function.Result == v2)
        {
            function.Result = v1;
        }
    }
}

switchfunc("z05", "jst");
switchfunc("mcm", "gdf");
switchfunc("z15", "dnt");
switchfunc("z30", "gwc");
// dnt,gdf,gwc,jst,mcm,z05,z15,z30

var r = new Random();
for (int i = 0; i < 100; i++)
{
    long x = r.NextInt64(1000000000000);
    long y = r.NextInt64(1000000000000);
    long z = x + y;

    Init(x, y);
    Run(values, functions);
    if (!Check(z, 44))
        Console.WriteLine($"{x}, {y}");
}

void WriteSum(SortedDictionary<string, int> values)
{
    for (int i = 0; i < 46; i++)
    {
        if (values.TryGetValue($"x{(45 - i),2:00}", out int v))
        {
            Console.Write(v);
        }
        else
        {
            Console.Write(" ");
        }
    }
    Console.WriteLine();
    for (int i = 0; i < 46; i++)
    {
        if (values.TryGetValue($"y{(45 - i),2:00}", out int v))
        {
            Console.Write(v);
        }
        else
        {
            Console.Write(" ");
        }
    }
    Console.WriteLine();
    for (int i = 0; i < 46; i++)
    {
        if (values.TryGetValue($"z{(45 - i),2:00}", out int v))
        {
            Console.Write(v);
        }
        else
        {
            Console.Write(" ");
        }
    }
    Console.WriteLine();
}


static void WriteTotal(SortedDictionary<string, int> values)
{
    long res = 0;
    for (int i = 0; i < 100; i++)
    {
        if (values.TryGetValue($"z{i,2:00}", out int v))
        {
            res += v * (long)Math.Pow(2, i);
        }
        else
        {
            break;
        }
    }

    Console.WriteLine($"total {res}");
}

static void Run(SortedDictionary<string, int> values, List<Function> functions)
{
    while (true)
    {
        bool donestep = false;

        foreach (var func in functions)
        {
            if (values.TryGetValue(func.Val1, out int v1)
                 && values.TryGetValue(func.Val2, out int v2)
                 && !values.ContainsKey(func.Result))
            {
                donestep = true;

                int result = 0;
                if (func.Op == "AND")
                {
                    result = v1 & v2;
                }
                if (func.Op == "OR")
                {
                    result = v1 | v2;
                }
                if (func.Op == "XOR")
                {
                    result = v1 ^ v2;
                }

                values.Add(func.Result, result);
            }
        }

        //foreach (var kv in values)
        //{
        //    Console.WriteLine($"{kv.Key} {kv.Value}");
        //}

        if (!donestep)
            break;
    }
}

void Init(long x, long y)
{
    var keys = values.Keys.ToList();
    foreach (var key in keys)
    {
        if (key.StartsWith('x'))
            values[key] = 0;
        else if (key.StartsWith('y'))
            values[key] = 0;
        else
            values.Remove(key);
    }

    for (int i = 0; i < 45; i++)
    {
        if (x % 2 == 1)
        {
            values[$"x{i,2:00}"] = 1;
        }

        x = x / 2;

        if (y % 2 == 1)
        {
            values[$"y{i,2:00}"] = 1;
        }

        y = y / 2;
    }
}

bool Check(long z, int decimals)
{
    for (int i = 0; i < decimals; i++)
    {
        if (values[$"z{i,2:00}"] != z % 2)
            return false;

        z = z / 2;
    }

    return true;
}

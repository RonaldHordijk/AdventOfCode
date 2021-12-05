var lines = File.ReadAllLines("data.txt");

var values = lines.Select(l => Int64.Parse(l)).ToList();

var incCount = 0;

for (int i = 1; i < values.Count; i++)
{
    if (values[i] > values[i - 1])
        incCount++;
}

Console.WriteLine($"Nr Increases {incCount}");

var window3 = new List<Int64>();
for (int i = 2; i < values.Count; i++)
{
    window3.Add(values[i] + values[i - 1] + values[i - 2]);
}

incCount = 0;

for (int i = 1; i < window3.Count; i++)
{
    if (window3[i] > window3[i - 1])
        incCount++;
}

Console.WriteLine($"Nr Increases 3 {incCount}");

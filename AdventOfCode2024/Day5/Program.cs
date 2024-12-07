var lines = File.ReadAllLines("data.txt");


List<(int, int)> rules = [];
List<List<int>> sets = [];

foreach (var line in lines)
{
    if (line.Contains('|'))
    {
        var words = line.Split('|');
        rules.Add((Convert.ToInt32(words[0]), Convert.ToInt32(words[1])));
    }
}

foreach (var line in lines)
{
    if (!line.Contains('|') && line.Length > 0)
    {
        var words = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
        sets.Add(words.Select(w => Convert.ToInt32(w)).ToList());
    }
}

int sumCentersOrder = 0;

foreach (var set in sets)
{
    if (InOrder(set))
    {
        Console.WriteLine($"{set[set.Count / 2]}");
        sumCentersOrder += set[set.Count / 2];
    }
}
Console.WriteLine($"sumorder {sumCentersOrder}");

int sumCenters = 0;

foreach (var set in sets)
{
    PutInOrder(set);

    Console.WriteLine($"{set[set.Count / 2]}");
    sumCenters += set[set.Count / 2];

}

Console.WriteLine($"sum {sumCenters}");
Console.WriteLine($"sum wrongorder {sumCenters - sumCentersOrder}");

void PutInOrder(List<int> set)
{
    bool exchange = true;
    while (exchange)
    {
        exchange = false;

        foreach (var (p1, p2) in rules)
        {
            int index1 = set.IndexOf(p1);
            int index2 = set.IndexOf(p2);

            if (index1 >= 0 && index2 >= 0)
            {
                if (index1 > index2)
                {
                    exchange = true;
                    (set[index1], set[index2]) = (set[index2], set[index1]);
                }
            }
        }
    }
}

bool InOrder(List<int> set)
{

    foreach (var (p1, p2) in rules)
    {
        int index1 = set.IndexOf(p1);
        int index2 = set.IndexOf(p2);

        if (index1 >= 0 && index2 >= 0)
        {
            if (index1 > index2)
            {
                return false;
            }
        }
    }
    return true;
}

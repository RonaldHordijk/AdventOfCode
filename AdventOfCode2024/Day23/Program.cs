var lines = File.ReadAllLines("data.txt");

Dictionary<string, List<string>> map = [];

foreach (var line in lines)
{
    var words = line.Split('-');
    string small = words[0];
    string big = words[1];

    if (string.Compare(small, big) > 0)
        (small, big) = (big, small);

    if (map.TryGetValue(small, out var list))
    {
        list.Add(big);
    }
    else
    {
        map.Add(small, [big]);
    }
}

int countT = 0;

foreach (var kv in map)
{
    kv.Value.Sort();

    for (int i = 0; i < kv.Value.Count; i++)
    {
        string second = kv.Value[i];
        if (!map.TryGetValue(second, out var secondList))
            continue;

        for (int j = i + 1; j < kv.Value.Count; j++)
        {
            if (secondList.Contains(kv.Value[j]))
            {
                //Console.WriteLine($"{kv.Key},{second},{kv.Value[j]}");

                if (kv.Key.StartsWith("t")
                    || second.StartsWith("t")
                    || kv.Value[j].StartsWith("t"))
                {
                    countT++;
                }
            }
        }
    }
}


Console.WriteLine($"Nr T {countT}");

int maxgroupListSize = 0;
List<string> maxgroupList = [];


foreach (var kv in map)
{
    MakeGroupList([kv.Key]);
}

maxgroupList.Sort();
Console.WriteLine($"MaxGroup  {maxgroupListSize} {string.Join(',', maxgroupList)} ");


void MakeGroupList(List<string> grouplist)
{
    List<string> joined = GetJoinedList(grouplist);

    if (joined.Count == 0)
    {
        if (grouplist.Count > maxgroupListSize)
        {
            maxgroupListSize = grouplist.Count;
            maxgroupList = new(grouplist);
        }

        return;
    }

    foreach (string j in joined)
    {
        grouplist.Add(j);

        MakeGroupList(grouplist);

        grouplist.Remove(j);
    }
}

List<string> GetJoinedList(List<string> grouplist)
{
    if (!map.TryGetValue(grouplist[0], out var frstList))
        return [];

    List<string> main = new(frstList);

    foreach (string g in grouplist.Skip(1))
    {
        if (!map.TryGetValue(g, out var secondList))
            return [];

        main = main.Where(s => secondList.Contains(s)).ToList();
    }

    return main;
}

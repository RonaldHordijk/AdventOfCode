var lines = File.ReadAllLines("data.txt");

static bool IsSafe(List<long> list)
{
    int dir = Math.Sign(list[0] - list[1]);

    bool safe = true;
    for (int i = 1; i < list.Count; i++)
    {
        if (Math.Sign(list[i - 1] - list[i]) != dir
            || (Math.Abs(list[i - 1] - list[i]) > 3)
            || (list[i - 1] == list[i]))
        {
            safe = false;
            break;
        }
    }

    return safe;
}


int nrsafe = 0;

foreach (var line in lines)
{
    var words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    var list = words.Select(w => Convert.ToInt64(w)).ToList();

    if (IsSafe(list))
    {
        nrsafe++;
    }
    else
    {
        //dampen
        for (int i = 0; i < list.Count; i++)
        {
            var listCopy = list.Select(x => x).ToList();
            listCopy.RemoveAt(i);
            if (IsSafe(listCopy))
            {
                nrsafe++;
                break;
            }
        }
    }
}

Console.WriteLine($"nr safe {nrsafe}");

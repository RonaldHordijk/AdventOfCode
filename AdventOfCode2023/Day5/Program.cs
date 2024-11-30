
var lines = File.ReadAllLines("dataTest.txt");

var seeds = lines[0].Split(':')[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => Int64.Parse(s)).ToList();
Console.WriteLine(string.Join(" ", seeds.Select(s => s.ToString())));

List<(long to, long from, long size)> map = new();

foreach (var line in lines.Skip(2))
{
    if (string.IsNullOrWhiteSpace(line))
    {
        //map
        MoveSeeds(map);
        map.Clear();
        continue;
    }

    if (!Char.IsDigit(line[0]))
        continue;

    var values = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => Int64.Parse(s)).ToArray();
    map.Add((values[0], values[1], values[2]));
}

MoveSeeds(map);

Console.WriteLine($"Min is {seeds.Min()}");

// ranges

List<(long start, long size)> seedranges = [];

for (int s = 0; s < seeds.Count; s += 2)
{
    seedranges.Add((seeds[s], seeds[s + 1]));
}

map.Clear();
foreach (var line in lines.Skip(2))
{
    if (string.IsNullOrWhiteSpace(line))
    {
        //map
        seedranges = MoveSeedRanges(map, seedranges);
        map.Clear();
        continue;
    }

    if (!Char.IsDigit(line[0]))
        continue;

    var values = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => Int64.Parse(s)).ToArray();
    map.Add((values[0], values[1], values[2]));
}

seedranges = MoveSeedRanges(map, seedranges);

Console.WriteLine($"RandedMin is {seedranges.Min(sr => sr.start)}");

List<(long start, long size)> MoveSeedRanges(List<(long to, long from, long size)> map, List<(long start, long size)> seedranges)
{
    List<(long start, long size)> splittedRanges = [];

    for (int sr = 0; sr < seedranges.Count; sr++)
    {
        var (start, srsized) = seedranges[sr];
        long end = start + srsized;

        foreach (var (to, from, size) in map)
        {
            if ((to > start) && (to < end))
            {
                splittedRanges.Add((start, start - to));
                start = to;
            }

            long tend = to + size;
            if ((tend > start) && (tend < end))
            {
                splittedRanges.Add((start, start - tend));
                start = tend;
            }

            splittedRanges.Add((start, end - start));
        }
    }

    List<(long start, long size)> result = [];

    for (int sr = 0; sr < splittedRanges.Count; sr++)
    {
        var (start, srsized) = splittedRanges[sr];
        long end = start + srsized;

        foreach (var (to, from, size) in map)
        {
            if (start >= from
               && start < from + size)
            {
                result.Add((to + start - from, srsized));
                break;
            }
        }
    }

    return result;
}

void MoveSeeds(List<(long to, long from, long size)> map)
{
    for (int s = 0; s < seeds.Count; s++)
    {
        long seed = seeds[s];

        foreach (var (to, from, size) in map)
        {
            if (seed >= from
                && seed < from + size)
            {
                seeds[s] = to + seed - from;
                break;
            }
        }
    }

    Console.WriteLine(string.Join(" ", seeds.Select(s => s.ToString())));
}

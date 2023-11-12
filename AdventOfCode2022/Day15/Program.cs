using Day15;
using System.Text.RegularExpressions;

var lines = File.ReadAllLines("data.txt");

List<Sensor> sensors = new();

//Sensor at x=20, y=1: closest beacon is at x=15, y=3
Regex regex = new Regex(@".*x=([+-]?\d+).*y=([+-]?\d+).*x=([+-]?\d+).*y=([+-]?\d+)");

foreach (var line in lines)
{
    Match match = regex.Match(line);

    // Step 3: test for Success.
    if (match.Success)
    {
        sensors.Add(new Sensor
        {
            SX = int.Parse(match.Groups[1].Value),
            SY = int.Parse(match.Groups[2].Value),
            BX = int.Parse(match.Groups[3].Value),
            BY = int.Parse(match.Groups[4].Value),
        });
    }
}

var regions = GetRegions(10);
int sum = 0;

foreach (var r in regions)
    sum += (r.e - r.s);

Console.WriteLine($"sum in {sum}");

for (int r = 0; r < 4000000; r++)
{
    var rr = GetRegions(r);
    if (rr.Count > 1)
    {
        Console.WriteLine(r);
        foreach (var region in rr)
            Console.WriteLine($"s = {region.s}, e ={region.e}");
        Console.WriteLine($"v = {(long)4000000 * ((long)rr[0].e + 1) + (long)r}");
        Console.WriteLine($"v = {4000000 * r + (rr[0].e + 1)}");
    }
}

List<(int s, int e)> GetRegions(int row)
{

    List<(int s, int e)> regions = new();

    foreach (var sensor in sensors)
    {
        (int s, int e) = sensor.GetRow(row);

        if (e >= s)
            regions.Add((s, e));
        //        Console.WriteLine($"s = {s}, e ={e}");
    }

    for (int i = 0; i < regions.Count; i++)
    {
        //Console.WriteLine("i");
        //foreach (var r in regions)
        //    Console.WriteLine($"s = {r.s}, e ={r.e}");

        for (int j = i + 1; j < regions.Count; j++)
        {
            if (regions[i].e < regions[j].s || regions[j].e < regions[i].s)
                continue;

            // join
            regions[i] = (Math.Min(regions[i].s, regions[j].s), Math.Max(regions[i].e, regions[j].e));
            regions.RemoveAt(j);
            j = i;

            //Console.WriteLine();
            //foreach (var r in regions)
            //    Console.WriteLine($"s = {r.s}, e ={r.e}");
        }
    }
    return regions;
}

var lines = File.ReadAllLines("data.txt");

var fishes = lines[0]
    .Split(",")
    .Select(x => Int32.Parse(x))
    .ToList();

long[] timebuffer = new long[300];
for (int d = 0; d < 299; d++)
{
    timebuffer[d] = 1;
    if ((d - 7) > 0)
        timebuffer[d] += timebuffer[d - 7];

    if ((d - 9) > 0)
        timebuffer[d] += timebuffer[d - 9];
}


Int64 count = fishes.Count;
foreach (int fish in fishes)
{
    count += timebuffer[256 - fish];
}
Console.WriteLine($"Day 80, fishes {count}");

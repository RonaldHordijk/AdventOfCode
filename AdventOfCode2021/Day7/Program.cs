var lines = File.ReadAllLines("data.txt");

var pos = lines[0]
    .Split(",")
    .Select(x => Int32.Parse(x))
    .ToList();

var avg = pos.Average();

Int64 minp = 0;
Int64 minf = 100000000;
for (int p = 0; p <= (int)Math.Ceiling(avg) + 100; p++)
{
    Int64 fuel = pos.Sum(pp =>
    {
        Int64 d = Math.Abs(pp - p);
        return (d * (d + 1)) / 2;
    });

    Console.WriteLine($"Positon {p} Fuel {fuel}");

    if (fuel < minf)
    {
        minf = fuel;
        minp = p;
    }
}


Console.WriteLine($"\nPositon {minp} Fuel {minf}");

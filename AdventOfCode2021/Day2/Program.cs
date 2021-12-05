var lines = File.ReadAllLines("data.txt");

Int64 forward = 0;
Int64 depth = 0;

foreach (var l in lines)
{
    var t = l.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    if (t[0] == "forward")
        forward += Int64.Parse(t[1]);

    if (t[0] == "down")
        depth += Int64.Parse(t[1]);

    if (t[0] == "up")
        depth -= Int64.Parse(t[1]);
}

Console.WriteLine($"({forward}, {depth}) = {forward * depth}");



forward = 0;
depth = 0;
Int64 aim = 0;

foreach (var l in lines)
{
    var t = l.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    if (t[0] == "forward")
    {
        forward += Int64.Parse(t[1]);
        depth += Int64.Parse(t[1]) * aim;
    }

    if (t[0] == "down")
        aim += Int64.Parse(t[1]);

    if (t[0] == "up")
        aim -= Int64.Parse(t[1]);
}

Console.WriteLine($"({forward}, {depth}) = {forward * depth}");

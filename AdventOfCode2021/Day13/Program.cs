var lines = File.ReadAllLines("data.txt");

List<(int x, int y)> coords = new();
List<(bool x, int pos)> folds = new();


foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
        continue;

    if (line[0] == 'f')
    {
        bool x = line.Contains("x");
        var wf = line.Split('=');
        folds.Add((x, Int32.Parse(wf[1])));
        continue;
    }

    var w = line.Split(',');
    coords.Add((Int32.Parse(w[0]), Int32.Parse(w[1])));
}


foreach (var fold in folds)
{
    var c = coords.GroupBy(xy => xy).Count();
    Console.WriteLine(c);


    for (int i = 0; i < coords.Count; i++)
    {
        var coord = coords[i];

        if (fold.x)
        {
            if (coord.x > fold.pos)
                coord.x = 2 * fold.pos - coord.x;
        }
        else
        {
            if (coord.y > fold.pos)
                coord.y = 2 * fold.pos - coord.y;
        }

        coords[i] = coord;
    }
}

var count = coords.GroupBy(xy => xy).Count();
Console.WriteLine(count);

int maxx = coords.Max(xy => xy.x) + 1;
int maxy = coords.Max(xy => xy.y) + 1;

for (int i = 0; i < maxy; i++)
{
    for (int j = 0; j < maxx; j++)
    {
        if (coords.Any(xy => (xy.x == j) && (xy.y == i)))
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }
    Console.WriteLine();
}

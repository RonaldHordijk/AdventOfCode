var lines = File.ReadAllLines("data.txt");

// build graph
var caves = new Dictionary<string, Cave>();
foreach (var line in lines)
{
    var locs = line.Split("-");

    Cave? cave1;
    if (caves.ContainsKey(locs[0]))
    {
        cave1 = caves[locs[0]];
    }
    else
    {
        cave1 = new Cave(locs[0]);
        caves.Add(locs[0], cave1);
    }

    Cave? cave2;
    if (caves.ContainsKey(locs[1]))
    {
        cave2 = caves[locs[1]];
    }
    else
    {
        cave2 = new Cave(locs[1]);
        caves.Add(locs[1], cave2);
    }

    cave1.Connections.Add(cave2);
    cave2.Connections.Add(cave1);
}

List<Cave?> path = new();
caves["start"].Passed = true;
path.Add(caves["start"]);
int count = 0;

while (path.Count > 0)
{
    path.Add(path.Last()?.GetNextConnection(null));

    if (path.Last()?.Name == "end")
    {
        //Console.WriteLine(string.Join(',', path.ConvertAll(c => c?.Name)));
        count++;
    }
    //Console.WriteLine(string.Join(',', path.ConvertAll(c => c?.Name)));

    while (!IsValidPath(path) || (path.Last()?.Name == "end"))
    {
        var last = path.Last();
        path.RemoveAt(path.Count - 1);

        if (last is null)
        {
            last = path.Last();
            path.RemoveAt(path.Count - 1);
        }

        //Console.WriteLine(string.Join(',', path.ConvertAll(c => c?.Name)));

        if (path.Count == 0)
            break;

        path.Add(path.Last()?.GetNextConnection(last));

        if (path.Last()?.Name == "end")
        {
            //Console.WriteLine(string.Join(',', path.ConvertAll(c => c?.Name)));
            count++;
        }

        //Console.WriteLine(string.Join(',', path.ConvertAll(c => c?.Name)));
    }
}

bool IsValidPath(List<Cave?> path)
{
    if (path is null)
        return false;

    if (path.Count == 0)
        return false;

    var last = path.Last();
    if (last is null)
        return false;

    if (last.Small && path.Count(c => c == last) > 2)
        return false;

    if (last.Small && path.Count(c => c == last) == 2)
    {
        if (last.Name == "start")
            return false;

        return path.Where(c => c.Small).GroupBy(c => c.Name).Count(l => l.Count() == 2) <= 1;
    }

    return true;

}

Console.WriteLine(count);


public class Cave
{
    public string Name { get; set; }
    public bool Small { get; set; }

    public bool Passed { get; set; }

    public List<Cave> Connections { get; set; } = new();

    public Cave? GetNextConnection(Cave? cave)
    {
        if (cave is null)
            return Connections[0];

        if (cave == Connections.Last())
            return null;

        return Connections[Connections.IndexOf(cave) + 1];
    }

    public Cave(string name)
    {
        Name = name;
        Small = Char.IsLower(name[0]);
    }
}

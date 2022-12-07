using Day7;

var lines = File.ReadAllLines("data.txt");



string currentDir = "/";
List<Dir> dirs = new() { new() { Path = currentDir } };

foreach (var line in lines.Skip(1))
{
    if (line == "$ ls")
        continue;

    if (line.StartsWith("$ cd"))
    {
        if (line == "$ cd ..")
        {
            var index = currentDir[..^1].LastIndexOf("/");
            currentDir = currentDir[..index] + "/";
        }
        else
        {
            currentDir += line[5..] + "/";

            dirs.Add(new() { Path = currentDir });
        }
        continue;
    }

    if (line.StartsWith("dir"))
        continue; // dir result

    // values
    var size = long.Parse(line.Split(" ")[0]);

    foreach (var dir in dirs.Where(d => currentDir.StartsWith(d.Path)))
        dir.Size += size;
}


foreach (var dir in dirs)
{
    Console.WriteLine($"{dir.Path} -  {dir.Size}");
}

long sum = 0;
foreach (var dir in dirs)
{
    if (dir.Size <= 100000)
        sum += dir.Size;
}

Console.WriteLine($"Total {sum}");

// needed 
long neededSpace = 30000000 - (70000000 - dirs[0].Size);

var x = dirs.Where(d => d.Size > neededSpace).OrderBy(d => d.Size).ToList();

Console.WriteLine($"smallest {x[0].Path} {x[0].Size}");

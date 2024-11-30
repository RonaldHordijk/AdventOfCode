var lines = File.ReadAllLines("data.txt");

string path = lines[0];

Dictionary<string, (string L, string R)> graph = [];

foreach (string line in lines.Skip(2))
{
    //AAA = (BBB, CCC)
    graph.Add(line[0..3], (line[7..10], line[12..15]));
}

long steps = 0;
//string pos = "AAA";

//while (pos != "ZZZ")
//{
//    bool goLeft = path[steps % path.Length] == 'L';
//    pos = goLeft ? graph[pos].L : graph[pos].R;
//    steps++;
//}

//Console.WriteLine($"Nr steps {steps}");

steps = 0;
var positions = graph.Where(kv => kv.Key.EndsWith("A")).Select(kv => kv.Key).ToList();

while (true)
{
    bool goLeft = path[(int)(steps % path.Length)] == 'L';
    positions = positions.Select(p => goLeft ? graph[p].L : graph[p].R).ToList();
    steps++;

    if (positions.All(p => p.EndsWith("Z")))
        break;

    if (steps % 10000 == 0)
    {
        Console.WriteLine(steps);
    }
}

Console.WriteLine($"Nr steps {steps}");

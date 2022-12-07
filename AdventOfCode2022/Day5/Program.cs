using System.Text.RegularExpressions;

var lines = File.ReadAllLines("data.txt");

var sum = 0;


//   [D]
//   [N][C]
//[Z][M][P]
// 1   2   3

int nrStacks = 0;
int lineEnd = 0;

for (int i = 0; i < lines.Length; i++)
{
    if (lines[i][1] == '1')
    {
        lineEnd = i;
        nrStacks = lines[i].Count(c => c != ' ');

        break;
    }
}

var stacks = new List<Stack<char>>();
for (int i = 0; i < nrStacks; i++)
{
    stacks.Add(new Stack<char>());
}

for (int i = lineEnd - 1; i >= 0; i--)
{
    var line = lines[i];
    for (int lc = 1; lc < line.Length; lc += 4)
    {
        if (line[lc] != ' ')
            stacks[(lc - 1) / 4].Push(line[lc]);
    }
}

foreach (var line in lines.Skip(lineEnd + 2))
{
    //move 1 from 2 to 1
    Match m = Regex.Match(line, @"move (\d*) from (\d*) to (\d*)");
    if (m.Success)
    {
        var count = int.Parse(m.Groups[1].Value);
        var from = int.Parse(m.Groups[2].Value) - 1;
        var to = int.Parse(m.Groups[3].Value) - 1;

        var buffer = new Stack<char>();

        for (int j = 0; j < count; j++)
        {
            buffer.Push(stacks[from].Pop());
        }
        for (int j = 0; j < count; j++)
        {
            stacks[to].Push(buffer.Pop());
        }

    }
}

Console.Write($"Result: ");
for (int i = 0; i < nrStacks; i++)
    Console.Write($"{stacks[i].Pop()}");

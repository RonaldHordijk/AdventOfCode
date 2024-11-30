using Day21;

var lines = File.ReadAllLines("data.txt");

var monkeyValues = new Dictionary<string, long>();
List<Monkey> monkeys = new();

foreach (var line in lines)
{
    if (line.Length == 17)
    {
        monkeys.Add(new()
        {
            Name = line[..4],
            Val1 = line[6..10],
            Val2 = line[13..17],
            Action = line[11]
        });
    }
    else
    {
        monkeyValues.Add(line[..4], int.Parse(line[6..]));
    }
}

monkeyValues.Remove("humn");

var root = monkeys.Find(m => m.Name == "root");
monkeys.Remove(root);

bool removed = true;
while (removed)
{
    removed = false;
    Console.WriteLine(monkeys.Count);
    for (int i = monkeys.Count - 1; i >= 0; i--)
    {
        if (monkeyValues.ContainsKey(monkeys[i].Val1)
            && monkeyValues.ContainsKey(monkeys[i].Val2))
        {
            switch (monkeys[i].Action)
            {
                case '+':
                    monkeyValues.Add(monkeys[i].Name, monkeyValues[monkeys[i].Val1] + monkeyValues[monkeys[i].Val2]);
                    break;
                case '-':
                    monkeyValues.Add(monkeys[i].Name, monkeyValues[monkeys[i].Val1] - monkeyValues[monkeys[i].Val2]);
                    break;
                case '*':
                    monkeyValues.Add(monkeys[i].Name, monkeyValues[monkeys[i].Val1] * monkeyValues[monkeys[i].Val2]);
                    break;
                case '/':
                    monkeyValues.Add(monkeys[i].Name, monkeyValues[monkeys[i].Val1] / monkeyValues[monkeys[i].Val2]);
                    break;
                default:
                    Console.WriteLine($"Uknown {monkeys[i].Action}");
                    break;
            }

            monkeys.RemoveAt(i);
            removed = true;
        }
    }
}

long start = 3558714868000;
long step = 1000;

for (long i = start; i < start + 10000; i += 1)
{
    monkeyValues["humn"] = i;

    foreach (var m in monkeys)
    {
        m.solved = false;
        monkeyValues.Remove(m.Name);
    }

    Solve();

    if (monkeyValues[root.Val1] < monkeyValues[root.Val2])
        Console.WriteLine($"{i} {monkeyValues[root.Val1]} < {monkeyValues[root.Val2]}");
    else
        Console.WriteLine($"{i} {monkeyValues[root.Val1]} > {monkeyValues[root.Val2]}");

    if (monkeyValues[root.Val1] == monkeyValues[root.Val2])
        break;
}

Console.WriteLine($"{monkeyValues[root.Val1]} {monkeyValues[root.Val2]}");


void Solve()
{

    while (monkeys.Count(m => !m.solved) > 0)
    {
        for (int i = 0; i < monkeys.Count; i++)
        {
            if (!monkeys[i].solved
                && monkeyValues.ContainsKey(monkeys[i].Val1)
                && monkeyValues.ContainsKey(monkeys[i].Val2))
            {
                switch (monkeys[i].Action)
                {
                    case '+':
                        monkeyValues.Add(monkeys[i].Name, monkeyValues[monkeys[i].Val1] + monkeyValues[monkeys[i].Val2]);
                        break;
                    case '-':
                        monkeyValues.Add(monkeys[i].Name, monkeyValues[monkeys[i].Val1] - monkeyValues[monkeys[i].Val2]);
                        break;
                    case '*':
                        monkeyValues.Add(monkeys[i].Name, monkeyValues[monkeys[i].Val1] * monkeyValues[monkeys[i].Val2]);
                        break;
                    case '/':
                        monkeyValues.Add(monkeys[i].Name, monkeyValues[monkeys[i].Val1] / monkeyValues[monkeys[i].Val2]);
                        break;
                    default:
                        Console.WriteLine($"Uknown {monkeys[i].Action}");
                        break;
                }

                monkeys[i].solved = true;
            }
        }
    }
}

//var rootvalue = monkeyValues["root"];
//Console.WriteLine($"root {rootvalue}");

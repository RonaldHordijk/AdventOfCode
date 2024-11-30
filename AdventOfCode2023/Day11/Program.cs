using System.Numerics;

var lines = File.ReadAllLines("data.txt");

var monkeys = new List<Monkey>();
var inspect = new List<long>();

BigInteger modder = 1;

for (int i = 0; i <= lines.Length / 7; i++)
{
    var newMonkey = new Monkey
    {
        Items = lines[i * 7 + 1][18..].Split(",").Select(BigInteger.Parse).ToList(),
        Diviser = int.Parse(lines[i * 7 + 3][21..]),
        Multiply = lines[i * 7 + 2].Contains("*"),
        Add = lines[i * 7 + 2].Contains("+"),
        MultiplySelf = lines[i * 7 + 2].EndsWith("old"),
        Value = lines[i * 7 + 2].EndsWith("old") ? 0 : int.Parse(lines[i * 7 + 2][25..]),
        MonkeyTrue = int.Parse(lines[i * 7 + 4][29..]),
        MonkeyFalse = int.Parse(lines[i * 7 + 5][30..]),
    };
    monkeys.Add(newMonkey);
    inspect.Add(0);

    modder *= newMonkey.Diviser;
}

for (int i = 1; i <= 10000; i++)
{
    DoStep(monkeys);
    //Show(monkeys);
    if (i % 1000 == 0)
        ShowInspect();
    if (i == 1)
        ShowInspect();
    if (i == 20)
        ShowInspect();
}

ShowInspect();

void ShowInspect()
{
    for (int i = 0; i < inspect.Count; i++)
    {
        Console.WriteLine($"monkey {i} {inspect[i]}");
    }
    Console.WriteLine();
}

inspect.Sort();
inspect.Reverse();
Console.WriteLine(inspect[0] * inspect[1]);


void Show(List<Monkey> monkeys)
{
    for (int i = 0; i < monkeys.Count; i++)
    {
        Console.WriteLine($"monkey {i} {string.Join(",", monkeys[i].Items.Select(i => i.ToString()).ToArray())}");
    }
    Console.WriteLine();
}

void DoStep(List<Monkey> monkeys)
{
    for (int i = 0; i < monkeys.Count; i++)
    {
        var monkey = monkeys[i];

        inspect[i] = inspect[i] + monkeys[i].Items.Count();

        foreach (var item in monkey.Items)
        {
            BigInteger v = item;

            if (monkey.MultiplySelf)
                v *= v;
            else if (monkey.Multiply)
                v *= monkey.Value;
            else
                v += monkey.Value;

            //v /= 3;
            v %= modder;

            if (v % monkey.Diviser == 0)
            {
                monkeys[monkey.MonkeyTrue].Items.Add(v);
            }
            else
            {
                monkeys[monkey.MonkeyFalse].Items.Add(v);
            }
        }
        monkey.Items.Clear();
    }
}

var lines = File.ReadAllLines("data.txt");

var sum = 0;

List<int> cardCount = [];

foreach (var line in lines)
{
    cardCount.Add(1);
}

int lineNr = 0;
foreach (var line in lines)
{
    var card = line.Split(":");
    var sets = card[1].Split("|");
    var winner = sets[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    var values = sets[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

    int count = 0;

    foreach (var w in winner)
    {
        if (values.Contains(w))
            count++;
    }

    if (count > 0)
        sum += 1 << (count - 1);

    for (int i = 0; i < count; i++)
    {
        cardCount[lineNr + i + 1] += cardCount[lineNr];
    }

    lineNr++;
}

Console.WriteLine($"Points {sum}");
Console.WriteLine($"NrCard {cardCount.Sum()}");

var lines = File.ReadAllLines("data.txt");

var sum = 0;
long sumPower = 0;

foreach (var line in lines)
{
    var v = line.Split(":");
    int game = int.Parse(v[0].Split(" ")[1]);

    bool impossible = false;
    int maxRed = 1;
    int maxGreen = 1;
    int maxBlue = 1;


    var rounds = v[1].Split(";");
    foreach (var round in rounds)
    {
        var values = round.Split(", ");
        foreach (var value in values)
        {
            var colval = value.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int val = int.Parse(colval[0]);
            string col = colval[1];

            if (col == "red" && val > 12)
                impossible = true;
            if (col == "blue" && val > 14)
                impossible = true;
            if (col == "green" && val > 13)
                impossible = true;

            if (col == "red" && val > maxRed)
                maxRed = val;
            if (col == "blue" && val > maxBlue)
                maxBlue = val;
            if (col == "green" && val > maxGreen)
                maxGreen = val;
        }
    }

    if (!impossible)
        sum += game;

    sumPower += maxGreen * maxRed * maxBlue;

}

Console.WriteLine($"Total Score {sum}");
Console.WriteLine($"Total Score {sumPower}");

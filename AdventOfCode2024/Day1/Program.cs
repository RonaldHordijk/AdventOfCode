var lines = File.ReadAllLines("data.txt");

Int64 som = 0;

//foreach (var line in lines)
//{
//    var digits = line.Where(c => Char.IsDigit(c)).ToArray();

//    som += 10 * (digits[0] - '0') + digits[^1] - '0';
//}

Console.WriteLine($"som {som}");

som = 0;

List<string> numbers = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];


foreach (var line in lines)
{
    var workline = line;

    int index = FindFirst(workline);
    if (index >= 0)
    {
        workline = workline.Replace(numbers[index], (index + 1).ToString());
        //index = FindFirst(workline);
    }

    var digits = workline.Where(c => Char.IsDigit(c)).ToArray();

    som += 10 * (digits[0] - '0');

    workline = line;
    index = FindLast(workline);
    if (index >= 0)
    {
        workline = workline.Replace(numbers[index], (index + 1).ToString());
        //index = FindFirst(workline);
    }

    digits = workline.Where(c => Char.IsDigit(c)).ToArray();

    som += digits[^1] - '0';

    //Console.WriteLine($"{line} {workline} {digits[0]}{digits[^1]} {som}");
}

Console.WriteLine($"som {som}");

int FindFirst(string workline)
{
    int first = -1;
    int minIndex = int.MaxValue;
    for (int i = 0; i < numbers.Count; i++)
    {
        var index = workline.IndexOf(numbers[i]);
        if (index < 0)
            continue;

        if (index < minIndex)
        {
            first = i;
            minIndex = index;
        }

    }

    return first;
}

int FindLast(string workline)
{
    int first = -1;
    int maxIndex = int.MinValue;
    for (int i = 0; i < numbers.Count; i++)
    {
        var index = workline.LastIndexOf(numbers[i]);
        if (index < 0)
            continue;

        if (index > maxIndex)
        {
            first = i;
            maxIndex = index;
        }

    }

    return first;
}

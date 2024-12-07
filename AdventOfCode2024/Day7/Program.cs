var lines = File.ReadAllLines("data.txt");

long sumsolve = 0;

foreach (var line in lines)
{
    var words = line.Split(':');
    long goal = Convert.ToInt64(words[0]);

    List<long> values = words[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

    int nrOptions = (int)Math.Pow(3, values.Count - 1);

    for (int i = 0; i < nrOptions; i++)
    {
        if (CanSolve(goal, values, i))
        {
            sumsolve += goal;
            break;
        }
    }
}

bool CanSolve(long goal, List<long> values, int combination)
{
    long sum = values[0];
    for (int j = 1; j < values.Count; j++)
    {
        int option = combination % 3;

        if (option == 0)
        {
            sum += values[j];
        }
        else if (option == 1)
        {
            sum *= values[j];
        }
        else
        {
            sum = long.Parse($"{sum}{values[j]}");
        }

        combination /= 3;
    }

    return goal == sum;
}

Console.WriteLine($"total {sumsolve}");

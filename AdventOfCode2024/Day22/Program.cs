var lines = File.ReadAllLines("data.txt");

var startValues = lines.Select(s => Convert.ToInt64(s)).ToList();
Dictionary<int, int> TotalBananas = [];

long sum = 0;

foreach (long value in startValues)
{
    long x = value;
    List<int> prices = [];

    for (int i = 0; i < 2000; i++)
    {
        x = NextNumber(x);
        prices.Add((int)(x % 10));
    }
    //Console.WriteLine($"{value} {x}");
    sum += x;

    processPrices(prices);
}

Console.WriteLine($"total {sum}");
Console.WriteLine($"max {TotalBananas.Max(kv => kv.Value)}");



void processPrices(List<int> prices)
{
    List<int> delta = [];

    for (int i = 1; i < 2000; i++)
    {
        delta.Add(prices[i] - prices[i - 1]);
    }

    List<int> sequences = [];

    for (int i = 4; i < 1999; i++)
    {
        int value = prices[i];
        int sequence = (delta[i - 4] + 9) * 1000000
                     + (delta[i - 3] + 9) * 10000
                     + (delta[i - 2] + 9) * 100
                     + (delta[i - 1] + 9) * 1;

        //-2,1,-1,3 = 7100812

        if (sequences.Contains(sequence))
            continue; // only first occurence

        sequences.Add(sequence);

        if (TotalBananas.TryGetValue(sequence, out int current))
        {
            TotalBananas[sequence] = current + value;
        }
        else
        {
            TotalBananas.Add(sequence, value);
        }
    }
}

long NextNumber(long secret)
{
    long x = secret * 64;
    //mix
    secret = secret ^ x;
    //prune
    secret = secret % 16777216;

    long y = (long)Math.Floor(secret / 32.0);
    //mix
    secret = secret ^ y;
    //prune
    secret = secret % 16777216;

    long z = secret * 2048;
    //mix
    secret = secret ^ z;
    //prune
    secret = secret % 16777216;

    return secret;
}

var lines = File.ReadAllLines("data.txt");

var sum = 0;

foreach (var line in lines)
{
    var values = line.Split(" ");
    //sum += GetScore(values[0][0] - 'A', values[1][0] - 'X');
    sum += GetScoreB(values[0][0] - 'A', values[1][0] - 'X');
}

Console.WriteLine($"Total Score {sum}");


int GetScore(int v1, int v2)
{
    int score = v2 + 1;

    if (v1 == v2)
    {
        //draw 
        return score + 3;
    }
    else if ((v1 == 0 && v2 == 1) || (v1 == 1 && v2 == 2) || (v1 == 2 && v2 == 0))
    {
        // won
        return score + 6;
    }
    else
    {
        // lost
        return score + 0;
    }
}

int GetScoreB(int v1, int v2)
{
    int score = 3 * v2;

    if (v2 == 1)
    {
        //draw 
        return score + v1 + 1;
    }
    else if (v2 == 2)
    {
        // won

        return score + (v1 + 1) % 3 + 1;
    }
    else
    {
        // lost
        return score + (v1 - 1 + 3) % 3 + 1;
    }
}

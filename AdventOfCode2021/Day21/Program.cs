int posp1 = 9;
int posp2 = 6;

//int posp1 = 3;
//int posp2 = 7;

//int scorep1 = 0;
//int scorep2 = 0;

//int die = 0;
//int nrdieroll = 0;
//while (true)
//{
//    // p1 
//    posp1 = (posp1 + GetDieRoll()) % 10;
//    scorep1 += posp1 + 1;
//    if (scorep1 >= 1000)
//        break;

//    Console.WriteLine($"p1 {posp1 + 1} {scorep1}");

//    // p2 
//    posp2 = (posp2 + GetDieRoll()) % 10;
//    scorep2 += posp2 + 1;
//    if (scorep2 >= 1000)
//        break;

//    Console.WriteLine($"p2 {posp2 + 1} {scorep2}");
//}
//Console.WriteLine($"nrdieroll {nrdieroll}");
//Console.WriteLine($"score p1 {scorep1}");
//Console.WriteLine($"score p2 {scorep2}");
//Console.WriteLine($"score p1 {scorep2 * nrdieroll}");
//Console.WriteLine($"score p2 {scorep1 * nrdieroll}");

//int GetDieRoll()
//{
//    nrdieroll += 3;

//    int sum = 3 + die + (die + 1) % 100 + (die + 2) % 100;
//    die = (die + 3) % 100;

//    return sum;
//}

var p1board = new long[10, 21];
p1board[posp1, 0] = 1;
long p1wins = 0;

var p2board = new long[10, 21];
p2board[posp2, 0] = 1;
long p2wins = 0;

List<(int value, long count)> dirac = new List<(int value, long count)> { (3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8, 3), (9, 1) };

for (int i = 0; i < 10; i++)
//while (true)
{
    long possibilities = Count(p2board);

    var p1next = new long[10, 21];
    for (int pos = 0; pos < 10; pos++)
    {
        for (int score = 0; score < 21; score++)
        {
            if (p1board[pos, score] == 0)
                continue;

            foreach (var (dicev, dicec) in dirac)
            {
                var newpos = (pos + dicev) % 10;
                var newscore = score + newpos + 1;
                if (newscore >= 21)
                {
                    p1wins += p1board[pos, score] * dicec * possibilities;
                }
                else
                {
                    p1next[newpos, newscore] += p1board[pos, score] * dicec;
                }
            }
        }
    }
    p1board = p1next;

    possibilities = Count(p1board);

    var p2next = new long[10, 21];
    for (int pos = 0; pos < 10; pos++)
    {
        for (int score = 0; score < 21; score++)
        {
            if (p2board[pos, score] == 0)
                continue;

            foreach (var (dicev, dicec) in dirac)
            {
                var newpos = (pos + dicev) % 10;
                var newscore = score + newpos + 1;
                if (newscore >= 21)
                {
                    p2wins += p2board[pos, score] * dicec * possibilities;
                }
                else
                {
                    p2next[newpos, newscore] += p2board[pos, score] * dicec;
                }
            }
        }
    }
    p2board = p2next;
}

long Count(long[,] board)
{
    long sum = 0;
    for (int pos = 0; pos < 10; pos++)
    {
        for (int score = 0; score < 21; score++)
        {
            sum += board[pos, score];
        }
    }

    return sum;
}

Console.WriteLine(p1wins);
Console.WriteLine(p2wins);

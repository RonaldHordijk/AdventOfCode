var lines = File.ReadAllLines("data.txt");

List<(int x, int y)> loop = new() { (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0) };

Dictionary<long, bool> TailPos = new();

foreach (var line in lines)
{
    char dir = line[0];
    int nrSteps = int.Parse(line[2..]);

    for (int i = 0; i < nrSteps; i++)
    {
        loop[0] = MoveHead(dir, loop[0]);

        for (int l = 1; l < loop.Count; l++)
            loop[l] = MoveTail(loop[l - 1], loop[l]);

        StoreTail(loop.Last());
    }

    //Dump();
}

void Dump()
{
    for (int x = -10; x < 10; x++)
    {
        for (int y = -10; y < 10; y++)
        {
            var res = ".";

            for (int l = 0; l < loop.Count; l++)
            {
                if ((loop[l].x == y) && (loop[l].y == x))
                {
                    res = l.ToString();
                    break;
                }
            }

            Console.Write(res);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

Console.WriteLine($"nr tailPos {TailPos.Count}");

void StoreTail((int x, int y) tail)
{
    long pos = tail.x * 10000 + tail.y;
    if (!TailPos.ContainsKey(pos))
        TailPos[pos] = true;
}

(int x, int y) MoveTail((int x, int y) head, (int x, int y) tail)
{
    if (Math.Abs(head.x - tail.x) <= 1 && Math.Abs(head.y - tail.y) <= 1)
        return tail;

    if (head.x > tail.x)
        tail.x++;
    else if (head.x < tail.x)
        tail.x--;

    if (head.y > tail.y)
        tail.y++;
    else if (head.y < tail.y)
        tail.y--;

    return tail;
}

(int x, int y) MoveHead(char dir, (int x, int y) head)
{
    switch (dir)
    {
        case 'R': head.x++; break;
        case 'L': head.x--; break;
        case 'U': head.y++; break;
        case 'D': head.y--; break;
    };

    return head;
}

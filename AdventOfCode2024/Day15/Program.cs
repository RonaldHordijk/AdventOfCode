
var lines = File.ReadAllLines("data.txt");

int xcount = lines[0].Length;
int ycount = lines.Count(l => l.Length > 0 && l[0] == '#');
int[,] map = new int[xcount, ycount];
int[,] map2 = new int[2 * xcount, ycount];

int rx = 0;
int ry = 0;
int r2x = 0;
int r2y = 0;

for (int y = 0; y < ycount; y++)
{
    var line = lines[y];
    for (int x = 0; x < line.Length; x++)
    {
        map[x, y] = 0;
        if (line[x] == '#')
            map[x, y] = 2;
        if (line[x] == 'O')
            map[x, y] = 1;

        map2[2 * x, y] = 0;
        map2[2 * x + 1, y] = 0;
        if (line[x] == '#')
        {
            map2[2 * x, y] = 2;
            map2[2 * x + 1, y] = 2;
        }
        if (line[x] == 'O')
        {
            map2[2 * x, y] = 10;
            map2[2 * x + 1, y] = 11;
        }

        if (line[x] == '@')
        {
            rx = x;
            ry = y;
            r2x = 2 * x;
            r2y = y;
        }
    }
}

string path = "";

for (int y = ycount + 1; y < lines.Length; y++)
{
    path += lines[y];
}

DumpMap2();
foreach (var c in path)
{
    TryMove2(c);
}

void TryMove(char c)
{
    if (c == '>')
    {
        for (int x = rx + 1; x < xcount; x++)
        {
            if (map[x, ry] == 0)
            {
                for (int xx = rx + 2; xx <= x; xx++)
                    map[xx, ry] = 1;

                rx += 1;
                map[rx, ry] = 0;
                break;
            }
            if (map[x, ry] == 2)
                break;
        }
    }

    if (c == '<')
    {
        for (int x = rx - 1; x > 0; x--)
        {
            if (map[x, ry] == 0)
            {
                for (int xx = x; xx < rx; xx++)
                    map[xx, ry] = 1;

                rx -= 1;
                map[rx, ry] = 0;
                break;
            }
            if (map[x, ry] == 2)
                break;
        }
    }

    if (c == 'v')
    {
        for (int y = ry + 1; y < xcount; y++)
        {
            if (map[rx, y] == 0)
            {
                for (int yy = ry + 2; yy <= y; yy++)
                    map[rx, yy] = 1;

                ry += 1;
                map[rx, ry] = 0;
                break;
            }
            if (map[rx, y] == 2)
                break;
        }
    }

    if (c == '^')
    {
        for (int y = ry - 1; y > 0; y--)
        {
            if (map[rx, y] == 0)
            {
                for (int yy = y; yy < ry; yy++)
                    map[rx, yy] = 1;

                ry -= 1;
                map[rx, ry] = 0;
                break;
            }
            if (map[rx, y] == 2)
                break;
        }
    }

    //Console.WriteLine(c);
    //DumpMap();
}

void TryMove2(char c)
{
    if (c == '>')
    {
        for (int x = r2x + 1; x < 2 * xcount; x++)
        {
            if (map2[x, r2y] == 0)
            {
                for (int xx = x; xx > r2x; xx--)
                    map2[xx, r2y] = map2[xx - 1, r2y];

                r2x += 1;
                map2[r2x, r2y] = 0;
                break;
            }
            if (map2[x, r2y] == 2)
                break;
        }
    }

    if (c == '<')
    {
        for (int x = r2x - 1; x > 0; x--)
        {
            if (map2[x, r2y] == 0)
            {
                for (int xx = x; xx < r2x; xx++)
                    map2[xx, r2y] = map2[xx + 1, r2y];

                r2x -= 1;
                map2[r2x, r2y] = 0;
                break;
            }
            if (map2[x, r2y] == 2)
                break;
        }
    }

    if (c == 'v')
    {
        if (map2[r2x, r2y + 1] == 0)
        {
            r2y += 1;
        }
        else if (map2[r2x, r2y + 1] != 2)
        {
            (bool success, var blocks) = GetBlockingDown(r2x, r2y + 1);

            if (success)
            {
                UnDouble(blocks);
                blocks.Sort((l, r) => l.y - r.y);

                for (int i = blocks.Count - 1; i >= 0; i--)
                {
                    map2[blocks[i].x, blocks[i].y + 1] = map2[blocks[i].x, blocks[i].y];
                    map2[blocks[i].x, blocks[i].y] = 0;
                }

                r2y += 1;
            }
        }
    }

    if (c == '^')
    {
        if (map2[r2x, r2y - 1] == 0)
        {
            r2y -= 1;
        }
        else if (map2[r2x, r2y - 1] != 2)
        {
            (bool success, var blocks) = GetBlockingUp(r2x, r2y - 1);

            if (success)
            {
                UnDouble(blocks);
                blocks.Sort((l, r) => r.y - l.y);

                for (int i = blocks.Count - 1; i >= 0; i--)
                {
                    map2[blocks[i].x, blocks[i].y - 1] = map2[blocks[i].x, blocks[i].y];
                    map2[blocks[i].x, blocks[i].y] = 0;
                }

                r2y -= 1;
            }
        }
    }

    //Console.WriteLine(c);
    //DumpMap2();
}

void UnDouble(List<(int x, int y)> blocks)
{
    for (int i = blocks.Count - 1; i > 0; i--)
    {
        for (int j = 0; j < i; j++)
        {
            if (blocks[i].x == blocks[j].x && blocks[i].y == blocks[j].y)
            {
                blocks.RemoveAt(i);
                break;
            }
        }
    }
}

(bool sucess, List<(int x, int y)>) GetBlockingDown(int x, int y)
{
    if (map2[x, y] == 10)
    {
        if (map2[x, y + 1] == 2 || map2[x + 1, y + 1] == 2)
            return (false, null);

        if (map2[x, y + 1] == 0 && map2[x + 1, y + 1] == 0)
            return (true, [(x, y), (x + 1, y)]);

        List<(int x, int y)> list = [(x, y), (x + 1, y)];

        if (map2[x, y + 1] >= 10)
        {
            (bool success, var list2) = GetBlockingDown(x, y + 1);
            if (!success)
                return (false, null);

            list.AddRange(list2);
        }

        if (map2[x + 1, y + 1] == 10)
        {
            (bool success, var list2) = GetBlockingDown(x + 1, y + 1);
            if (!success)
                return (false, null);

            list.AddRange(list2);
        }

        return (true, list);
    }
    else
    {
        if (map2[x, y + 1] == 2 || map2[x - 1, y + 1] == 2)
            return (false, null);

        if (map2[x, y + 1] == 0 && map2[x - 1, y + 1] == 0)
            return (true, [(x, y), (x - 1, y)]);

        List<(int x, int y)> list = [(x, y), (x - 1, y)];

        if (map2[x, y + 1] >= 10)
        {
            (bool success, var list2) = GetBlockingDown(x, y + 1);
            if (!success)
                return (false, null);

            list.AddRange(list2);
        }

        if (map2[x - 1, y + 1] == 11)
        {
            (bool success, var list2) = GetBlockingDown(x - 1, y + 1);
            if (!success)
                return (false, null);

            list.AddRange(list2);
        }

        return (true, list);
    }
}

(bool sucess, List<(int x, int y)>) GetBlockingUp(int x, int y)
{
    if (map2[x, y] == 10)
    {
        if (map2[x, y - 1] == 2 || map2[x + 1, y - 1] == 2)
            return (false, null);

        if (map2[x, y - 1] == 0 && map2[x + 1, y - 1] == 0)
            return (true, [(x, y), (x + 1, y)]);

        List<(int x, int y)> list = [(x, y), (x + 1, y)];

        if (map2[x, y - 1] >= 10)
        {
            (bool success, var list2) = GetBlockingUp(x, y - 1);
            if (!success)
                return (false, null);

            list.AddRange(list2);
        }

        if (map2[x + 1, y - 1] == 10)
        {
            (bool success, var list2) = GetBlockingUp(x + 1, y - 1);
            if (!success)
                return (false, null);

            list.AddRange(list2);
        }

        return (true, list);
    }
    else
    {
        if (map2[x, y - 1] == 2 || map2[x - 1, y - 1] == 2)
            return (false, null);

        if (map2[x, y - 1] == 0 && map2[x - 1, y - 1] == 0)
            return (true, [(x, y), (x - 1, y)]);

        List<(int x, int y)> list = [(x, y), (x - 1, y)];

        if (map2[x, y - 1] >= 10)
        {
            (bool success, var list2) = GetBlockingUp(x, y - 1);
            if (!success)
                return (false, null);

            list.AddRange(list2);
        }

        if (map2[x - 1, y - 1] == 11)
        {
            (bool success, var list2) = GetBlockingUp(x - 1, y - 1);
            if (!success)
                return (false, null);

            list.AddRange(list2);
        }

        return (true, list);
    }
}


//DumpMap();
//Count();

DumpMap2();
CountMap2();


void DumpMap()
{
    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < xcount; x++)
        {
            if (x == rx && y == ry)
                Console.Write('@');
            else if (map[x, y] == 0)
                Console.Write('.');
            if (map[x, y] == 1)
                Console.Write('0');
            if (map[x, y] == 2)
                Console.Write('#');
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

void DumpMap2()
{
    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < 2 * xcount; x++)
        {
            if (x == r2x && y == r2y)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('@');
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (map2[x, y] == 0)
                Console.Write('.');
            if (map2[x, y] == 10)
                Console.Write('[');
            if (map2[x, y] == 11)
                Console.Write(']');
            if (map2[x, y] == 2)
                Console.Write('#');
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}


void Count()
{
    long sum = 0;
    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < xcount; x++)
        {
            if (map[x, y] == 1)
                sum += 100 * y + x;
        }
    }
    Console.WriteLine($"total {sum}");
}

void CountMap2()
{
    long sum = 0;
    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < 2 * xcount; x++)
        {
            if (map2[x, y] == 10)
                sum += 100 * y + x;
        }
    }
    Console.WriteLine($"total m2 {sum}");
}

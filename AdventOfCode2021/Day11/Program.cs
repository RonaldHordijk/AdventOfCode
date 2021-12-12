var lines = File.ReadAllLines("data.txt");

int maxx = lines.Length;
int maxy = lines[0].Length;

var map = new int[maxx, maxy];
for (int i = 0; i < maxx; i++)
{
    for (int j = 0; j < maxy; j++)
    {
        map[i, j] = (int)Char.GetNumericValue(lines[i][j]);
    }
}

int flashcount = 0;
for (int step = 0; step < 20000; step++)
{
    Console.WriteLine($"{step} - {flashcount}");

    var mapNext = new int[maxx, maxy];
    // increase
    for (int i = 0; i < maxx; i++)
    {
        for (int j = 0; j < maxy; j++)
        {
            mapNext[i, j] = map[i, j] + 1;
        }
    }

    map = mapNext;
    // copy 
    for (int i = 0; i < maxx; i++)
    {
        for (int j = 0; j < maxy; j++)
        {
            mapNext[i, j] = map[i, j];
        }
    }

    bool action = true;
    while (action)
    {
        action = false;

        // flash
        for (int i = 0; i < maxx; i++)
        {
            for (int j = 0; j < maxy; j++)
            {
                if (mapNext[i, j] > 9 && mapNext[i, j] < 90)
                {
                    for (int ii = Math.Max(0, i - 1); ii < Math.Min(maxx, i + 2); ii++)
                    {
                        for (int jj = Math.Max(0, j - 1); jj < Math.Min(maxy, j + 2); jj++)
                        {
                            mapNext[ii, jj]++;
                        }
                    }
                    mapNext[i, j] = 90;
                    action = true;

                    //Console.WriteLine("flash");
                    //for (int x = 0; x < maxx; x++)
                    //{
                    //    for (int y = 0; y < maxy; y++)
                    //    {
                    //        Console.Write(mapNext[x, y].ToString().PadLeft(3));
                    //    }
                    //    Console.WriteLine();
                    //}
                }
            }

        }
    }

    // zero
    for (int i = 0; i < maxx; i++)
    {
        for (int j = 0; j < maxy; j++)
        {
            if (mapNext[i, j] > 9)
            {
                mapNext[i, j] = 0;
                flashcount++;
            }
        }
    }

    map = mapNext;

    //for (int i = 0; i < maxx; i++)
    //{
    //    for (int j = 0; j < maxy; j++)
    //    {
    //        Console.Write(map[i, j]);
    //    }
    //    Console.WriteLine();
    //}

    int sum = 0;
    for (int i = 0; i < maxx; i++)
    {
        for (int j = 0; j < maxy; j++)
        {
            sum += map[i, j];
        }
    }
    Console.WriteLine(sum);
    if (sum == 0)
        break;
}

//Console.WriteLine($"{100} - {flashcount}");

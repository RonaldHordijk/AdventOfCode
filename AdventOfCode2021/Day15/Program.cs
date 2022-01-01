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


var bigMap = new int[maxx * 5, maxy * 5];
for (int x = 0; x < 5; x++)
{
    for (int y = 0; y < 5; y++)
    {
        for (int i = 0; i < maxx; i++)
        {
            for (int j = 0; j < maxy; j++)
            {
                bigMap[maxx * x + i, maxy * y + j] = map[i, j] + x + y;
                if (bigMap[maxx * x + i, maxy * y + j] > 9)
                    bigMap[maxx * x + i, maxy * y + j] -= 9;
            }
        }
    }
}



var sumMap = new int[maxx * 5, maxy * 5];
for (int d = 1; d < (maxx * 5 + maxy * 5 - 1); d++)
{
    // next
    for (int i = 0; i < maxx * 5; i++)
    {
        for (int j = 0; j < maxy * 5; j++)
        {
            if (i + j == d)
            {
                int v1 = 1000000;
                if (i > 0)
                    v1 = sumMap[i - 1, j];

                int v2 = 1000000;
                if (j > 0)
                    v2 = sumMap[i, j - 1];

                sumMap[i, j] = bigMap[i, j] + Math.Min(v1, v2);
            }
        }
    }

    // can update prev
    bool found = true;
    while (d > 1 && found)
    {
        found = false;
        d--;
        for (int i = 0; i < maxx * 5; i++)
        {
            for (int j = 0; j < maxy * 5; j++)
            {
                if (i + j == d)
                {
                    int v1 = 1000000;
                    if (i < maxx * 5 - 1)
                        v1 = sumMap[i + 1, j];

                    int v2 = 1000000;
                    if (j < maxy * 5 - 1)
                        v2 = sumMap[i, j + 1];

                    var min = bigMap[i, j] + Math.Min(v1, v2);
                    if (min < sumMap[i, j])
                    {
                        sumMap[i, j] = min;
                        found = true;
                    }
                }
            }
        }
        if (!found)
            d++;
    }
}


//for (int d = 1; d < (maxx + maxy - 1); d++)
//{
//    for (int i = 0; i < maxx; i++)
//    {
//        for (int j = 0; j < maxy; j++)
//        {
//            if (i + j == d)
//            {
//                int v1 = 1000000;
//                if (i > 0)
//                    v1 = sumMap[i - 1, j];

//                int v2 = 1000000;
//                if (j > 0)
//                    v2 = sumMap[i, j - 1];

//                sumMap[i, j] = map[i, j] + Math.Min(v1, v2);
//            }
//        }
//    }
//}


//for (int i = 0; i < maxx * 5; i++)
//{
//    for (int j = 0; j < maxy * 5; j++)
//    {
//        Console.Write(bigMap[i, j]);
//    }
//    Console.WriteLine();
//}


Console.WriteLine(sumMap[maxx - 1, maxy - 1]);
Console.WriteLine(sumMap[maxx * 5 - 1, maxy * 5 - 1]);

//var path = new List<int>();
//int x = 0;
//int y = 0;
//int sum = 0;

//int minSum = 10000000;

//while (true)
//{
//    //Console.WriteLine(string.Join(",", path.Select(v => v.ToString())));

//    if (path.Count == maxx + maxy - 2)
//    {
//        if (sum < minSum)
//        {
//            minSum = sum;
//            Console.WriteLine(minSum);
//        }
//    }

//    if (path.Count < maxx + maxy - 2)
//    {
//        addstep();
//    }

//    if (sum >= minSum)
//    {
//        if (undostep() == false)
//            break;
//    }
//}

//bool undostep()
//{
//    while (path.Last() == 1 || (path.Sum() == maxy - 1))
//    {
//        sum -= map[x, y];
//        if (path.Last() == 1)
//            y--;
//        else
//            x--;

//        path.RemoveAt(path.Count - 1);

//        if (path.Count == 0)
//            return false;
//    }

//    // undo
//    sum -= map[x, y];
//    x--;

//    path[path.Count - 1] = 1;

//    y++;
//    sum += map[x, y];

//    return true;
//}

//void addstep()
//{
//    if (x < maxx - 1)
//    {
//        x++;
//        path.Add(0);
//    }
//    else
//    {
//        path.Add(1);
//        y++;
//    }

//    sum += map[x, y];
//}


var lines = File.ReadAllLines("data.txt");

char[,] map = new char[lines[0].Length, lines.Length];
int[,] use = new int[lines[0].Length, lines.Length];
int[,] values = new int[lines[0].Length, lines.Length];

int row = 0;
foreach (var line in lines)
{
    for (int x = 0; x < line.Length; x++)
        map[x, row] = line[x];

    row++;
}

// mark for use
for (int y = 0; y < lines.Length; y++)
    for (int x = 0; x < lines[0].Length; x++)
    {
        if (!IsSymbol(map[x, y]))
            continue;

        //checkneighbours
        for (int i = y - 1; i < y + 2; i++)
            for (int j = x - 1; j < x + 2; j++)
            {
                if (i < 0 || i >= lines.Length)
                    continue;
                if (j < 0 || j >= lines[0].Length)
                    continue;

                if (char.IsDigit(map[j, i]))
                    MarkUse(j, i);
            }
    }

//for (int y = 0; y < lines.Length; y++)
//{
//    for (int x = 0; x < lines[0].Length; x++)
//    {

//        if (use[x, y] == 1)
//            Console.Write(map[x, y]);
//        else
//            Console.Write(" ");

//    }
//    Console.WriteLine();
//}

// values
for (int y = 0; y < lines.Length; y++)
{
    int loc = 0;
    for (int x = 0; x < lines[0].Length; x++)
    {
        if (use[x, y] == 1)
        {
            loc = loc * 10 + map[x, y] - '0';
        }
        else
        {
            if (loc > 0)
            {
                SetValue(x - 1, y, loc);
                loc = 0;

            }
        }
    }

    if (loc > 0)
    {
        SetValue(lines[0].Length - 1, y, loc);
    }
}

long sum = 0;
// Sum
for (int y = 0; y < lines.Length; y++)
{
    int loc = 0;
    for (int x = 0; x < lines[0].Length; x++)
    {

        if (use[x, y] == 1)
        {
            loc = loc * 10 + map[x, y] - '0';
        }
        else
        {
            if (loc > 0)
            {
                sum += loc;
                loc = 0;

            }
        }
    }

    if (loc > 0)
    {
        sum += loc;
    }
}

Console.WriteLine($"res = {sum}");

long sumgears = 0;
// gears
for (int y = 0; y < lines.Length; y++)
    for (int x = 0; x < lines[0].Length; x++)
    {
        if (map[x, y] != '*')
            continue;

        List<int> v = new List<int>();

        //checkneighbours
        for (int i = y - 1; i < y + 2; i++)
            for (int j = x - 1; j < x + 2; j++)
            {
                if (i < 0 || i >= lines.Length)
                    continue;
                if (j < 0 || j >= lines[0].Length)
                    continue;

                if (char.IsDigit(map[j, i]))
                    v.Add(values[j, i]);
            }

        v = v.Distinct().ToList();
        if (v.Count == 2)
            sumgears += v[0] * v[1];
    }

Console.WriteLine($"gears = {sumgears}");

void MarkUse(int x, int y)
{
    use[x, y] = 1;
    for (int i = x - 1; i >= 0; i--)
    {
        if (char.IsDigit(map[i, y]))
        {
            use[i, y] = 1;
        }
        else
        {
            break;
        }
    }

    for (int i = x + 1; i < lines[0].Length; i++)
    {
        if (char.IsDigit(map[i, y]))
        {
            use[i, y] = 1;
        }
        else
        {
            break;
        }
    }
}

bool IsSymbol(char c)
{
    if (c == '.')
        return false;

    return !Char.IsDigit(c);
}

void SetValue(int x, int y, int val)
{
    while (x > 0 && Char.IsDigit(map[x, y]))
    {
        values[x, y] = val;
        x--;
    }
}

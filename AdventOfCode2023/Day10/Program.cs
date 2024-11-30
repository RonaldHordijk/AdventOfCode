var lines = File.ReadAllLines("data.txt");

int startx = 0;
int starty = 0;

for (int i = 0; i < lines.Length; i++)
{
    if (lines[i].Contains('S'))
    {
        starty = i;
        startx = lines[i].IndexOf('S');
        break;
    }
}

int[,] map = new int[lines.Length, lines[0].Length];
map[startx, starty] = 1;

int steps = 1;

int posx = startx;
int posy = starty;

//int nextx = posx + 1;
//int nexty = posy;

int nextx = posx;
int nexty = posy + 1;

while (lines[nexty][nextx] != 'S')
{
    if (lines[nexty][nextx] == '|')
    {
        if (nexty > posy)
        {
            posy = nexty;
            nexty = nexty + 1;
        }
        else
        {
            posy = nexty;
            nexty = nexty - 1;
        }
    }
    else if (lines[nexty][nextx] == '-')
    {
        if (nextx > posx)
        {
            posx = nextx;
            nextx = nextx + 1;
        }
        else
        {
            posx = nextx;
            nextx = nextx - 1;
        }
    }
    else if (lines[nexty][nextx] == 'L')
    {
        if (nexty > posy)
        {
            posy = nexty;
            nextx = nextx + 1;
        }
        else
        {
            posx = nextx;
            nexty = nexty - 1;
        }
    }
    else if (lines[nexty][nextx] == '7')
    {
        if (nexty < posy)
        {
            posy = nexty;
            nextx = nextx - 1;
        }
        else
        {
            posx = nextx;
            nexty = nexty + 1;
        }
    }
    else if (lines[nexty][nextx] == 'J')
    {
        if (nexty > posy)
        {
            posy = nexty;
            nextx = nextx - 1;
        }
        else
        {
            posx = nextx;
            nexty = nexty - 1;
        }
    }
    else if (lines[nexty][nextx] == 'F')
    {
        if (nexty < posy)
        {
            posy = nexty;
            nextx = nextx + 1;
        }
        else
        {
            posx = nextx;
            nexty = nexty + 1;
        }
    }

    steps++;
    map[nexty, nextx] = 1;
    map[posy, posx] = 1;
}


Console.WriteLine($"{steps} {steps / 2} ");

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        Console.Write(map[y, x] == 1 ? "." : " ");
    }
    Console.WriteLine();
}

//for (int y = 0; y < lines.Length; y++)
//{
//    for (int x = 0; x < lines[0].Length; x++)
//    {
//        if (map[y, x] == 0)
//        {
//            var sb = new StringBuilder(lines[y]);
//            sb[x] = '.';
//            lines[y] = sb.ToString();
//        }
//    }
//}

//for (int y = 0; y < lines.Length; y++)
//{
//    Console.WriteLine(lines[y]);
//}

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        if (map[y, x] == 0)
        {
            int inside = 0;

            for (int i = x + 1; i < lines[0].Length; i++)
            {
                if (map[y, i] == 1 && lines[y][i] != '-')
                    inside++;
            }

            if (inside % 2 == 1)
                map[y, x] = 2;
        }
    }
}

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        if (map[y, x] == 1)
        {
            Console.Write(".");
        }
        else if (map[y, x] == 2)
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(" ");
        }

    }
    Console.WriteLine();
}

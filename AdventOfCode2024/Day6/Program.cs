
var lines = File.ReadAllLines("data.txt");

int[,] map = new int[lines[0].Length, lines.Length];

int xcount = lines[0].Length;
int ycount = lines.Length;

int startposx = 0;
int startposy = 0;
int posx = 0;
int posy = 0;
int dirx = 0;
int diry = -1;


int yy = 0;
foreach (var line in lines)
{
    for (int x = 0; x < line.Length; x++)
    {
        if (line[x] == '#')
            map[x, yy] = 1;

        if (line[x] == '^')
        {
            startposx = x;
            startposy = yy;
        }
    }
    yy++;
}

posx = startposx;
posy = startposy;


while (DoStep())
{ }

int count = 0;
for (int y = 0; y < ycount; y++)
{
    for (int x = 0; x < xcount; x++)
    {
        if (map[x, y] == 2)
            count++;
    }
}

Console.WriteLine($"steps {count}");

//ClearMap();
////map[9, 5] = 1;
//DumpMap();
//IsLoop();
//DumpMap();

int NrLoopPos = 0;

for (int y = 0; y < ycount; y++)
{
    for (int x = 0; x < xcount; x++)
    {
        if (x == startposx && y == startposy)
            continue;

        if (map[x, y] != 1)
        {
            map[x, y] = 1;

            if (IsLoop())
            {
                NrLoopPos++;
            }

            map[x, y] = 0;
        }
    }
}

Console.WriteLine($"loops {NrLoopPos}");


bool DoStep()
{
    map[posx, posy] = 2;

    posx += dirx;
    posy += diry;

    if (posx < 0 || posy < 0)
        return false;

    if (posx >= xcount || posy >= ycount)
        return false;

    while (map[posx, posy] == 1)
    {
        posx -= dirx;
        posy -= diry;

        // rotate
        if (dirx == 0 && diry == -1)
        {
            dirx = 1;
            diry = 0;
        }
        else if (dirx == 1 && diry == 0)
        {
            dirx = 0;
            diry = 1;
        }
        else if (dirx == 0 && diry == 1)
        {
            dirx = -1;
            diry = 0;
        }
        else if (dirx == -1 && diry == 0)
        {
            dirx = 0;
            diry = -1;
        }

        posx += dirx;
        posy += diry;
    }

    return true;
}

void DumpMap()
{
    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < xcount; x++)
        {
            if (map[x, y] == 0)
                Console.Write('.');
            if (map[x, y] == 1)
                Console.Write('#');
            if (map[x, y] == 2)
                Console.Write('X');
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

void ClearMap()
{
    for (int y = 0; y < ycount; y++)
    {
        for (int x = 0; x < xcount; x++)
        {
            if (map[x, y] == 2)
                map[x, y] = 0;
        }
    }
}



bool IsLoop()
{
    List<long> steps = [];
    ClearMap();

    posx = startposx;
    posy = startposy;
    dirx = 0;
    diry = -1;

    while (DoStep())
    {
        long code = posx * 100000 + posy * 100 + (dirx + 1) * 10 + diry + 1;

        if (steps.Contains(code))
            return true;

        steps.Add(code);
    }

    return false;
}

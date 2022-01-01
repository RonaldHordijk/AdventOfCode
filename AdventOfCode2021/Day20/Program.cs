var lines = File.ReadAllLines("data.txt");

var code = lines[0];

int maxx = lines[2].Length;
int maxy = lines.Length - 2;

int bound = 56;

int bmaxx = maxx + 2 * bound;
int bmaxy = maxy + 2 * bound;

var map = new char[bmaxx, bmaxy];

// init
for (int i = 0; i < bmaxx; i++)
{
    for (int j = 0; j < bmaxy; j++)
    {
        map[i, j] = '.';
    }
}

//load
for (int i = 0; i < maxx; i++)
{
    for (int j = 0; j < maxy; j++)
    {
        map[i + bound, j + bound] = lines[j + 2][i];
    }
}

//ShowMap();


//ShowMap();

for (int d = 1; d < 51; d++)
{
    DoStep(d, (d % 2 == 1) ? '#' : '.');
    Console.WriteLine(CountCells());
}

ShowMap();


void DoStep(int depth, char init)
{
    var newmap = new char[bmaxx, bmaxy];

    for (int i = 0; i < bmaxx; i++)
    {
        for (int j = 0; j < bmaxy; j++)
        {
            newmap[i, j] = init;
        }
    }

    for (int i = -depth; i < maxx + depth; i++)
    {
        for (int j = -depth; j < maxy + depth; j++)
        {
            newmap[j + bound, i + bound] = GetValue(i + bound, j + bound);
        }
    }

    for (int i = 0; i < bmaxx; i++)
    {
        for (int j = 0; j < bmaxy; j++)
        {
            map[i, j] = newmap[i, j];
        }
    }
}

char GetValue(int x, int y)
{
    string s = String.Empty;
    for (int i = -1; i < 2; i++)
    {
        for (int j = -1; j < 2; j++)
        {
            s += map[y + j, x + i] == '.' ? 0 : 1;
        }
    }

    return code[Convert.ToInt32(s, 2)];
}

void ShowMap()
{
    Console.WriteLine();

    for (int i = 0; i < bmaxx; i++)
    {
        for (int j = 0; j < bmaxy; j++)
        {
            Console.Write(map[j, i]);
        }
        Console.WriteLine();
    }
}


int CountCells()
{
    int sum = 0;
    for (int i = 0; i < bmaxx; i++)
    {
        for (int j = 0; j < bmaxy; j++)
        {
            if (map[i, j] == '#')
                sum++;
        }
    }

    return sum;
}

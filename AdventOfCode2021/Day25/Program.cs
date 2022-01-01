var lines = File.ReadAllLines("data.txt");

int my = lines[0].Length;
int mx = lines.Length;

var map = new char[mx, my];

for (int i = 0; i < mx; i++)
{
    for (int j = 0; j < my; j++)
    {
        map[i, j] = lines[i][j];
    }
}

//Print();


int step = 0;
while (true)
{
    step++;

    bool moved = false;

    // move east
    var newmap = new char[mx, my];
    for (int i = 0; i < mx; i++)
    {
        for (int j = 0; j < my; j++)
        {
            if (map[i, j] == '>')
            {
                if (map[i, (j + 1) % my] == '.')
                {
                    newmap[i, j] = '.';
                    newmap[i, (j + 1) % my] = '>';
                    moved = true;
                }
                else
                {
                    newmap[i, j] = '>';
                }
            }
            else if (map[i, j] == '.')
            {
                if (newmap[i, j] != '>')
                    newmap[i, j] = '.';
            }
            else
            {
                newmap[i, j] = map[i, j];
            }
        }
    }
    map = newmap;
    // down
    newmap = new char[mx, my];
    for (int i = 0; i < mx; i++)
    {
        for (int j = 0; j < my; j++)
        {
            if (map[i, j] == 'v')
            {
                if (map[(i + 1) % mx, j] == '.')
                {
                    newmap[i, j] = '.';
                    newmap[(i + 1) % mx, j] = 'v';
                    moved = true;
                }
                else
                {
                    newmap[i, j] = 'v';
                }
            }
            else if (map[i, j] == '.')
            {
                if (newmap[i, j] != 'v')
                    newmap[i, j] = '.';
            }
            else
            {
                newmap[i, j] = map[i, j];
            }
        }
    }
    map = newmap;

    //Print();

    if (!moved)
    {
        Console.WriteLine(step);
        break;
    }

}

void Print()
{
    for (int i = 0; i < mx; i++)
    {
        for (int j = 0; j < my; j++)
        {
            Console.Write(map[i, j]);
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

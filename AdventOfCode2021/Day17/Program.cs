//target area: x = 20..30, y = -10..-5
//int xmin = 20;
//int xmax = 30;
//int ymin = -10;
//int ymax = -5;

//target area: x = 265..287, y = -103..-58
int xmin = 265;
int xmax = 287;
int ymin = -103;
int ymax = -58;

int maxtop = -100000;
int count = 0;

for (int i = 1; i < xmax + 1; i++)
{
    for (int j = ymin; j < 1000; j++)
    {
        var (res, top) = shoot(i, j);
        if (res)
        {
            Console.WriteLine($"{i} {j}, {top}");
            maxtop = Math.Max(maxtop, top);
            count++;
        }
    }
}

Console.WriteLine(maxtop);
Console.WriteLine(count);

(bool, int) shoot(int dx, int dy)
{
    int x = 0;
    int y = 0;

    int top = 0;

    while (true)
    {
        x += dx;
        y += dy;

        if (y > top)
            top = y;

        if (dx < 0)
            dx++;
        else if (dx > 0)
            dx--;

        dy--;

        if (x > xmax)
            return (false, 0);

        if (y < ymin)
            return (false, 0);

        if (x <= xmax && x >= xmin && y <= ymax && y >= ymin)
            return (true, top);

    }
}

using System.Text.RegularExpressions;

var lines = File.ReadAllLines("data.txt");

//Button A: X + 94, Y + 34
//Button B: X + 22, Y + 67
//Prize: X = 8400, Y = 5400

long sum = 0;
for (int i = 0; i < lines.Length; i += 4)
{
    var m = Regex.Match(lines[i], @".*?(\d+).*?(\d+)");
    long ax = Convert.ToInt32(m.Groups[1].Value);
    long ay = Convert.ToInt32(m.Groups[2].Value);

    m = Regex.Match(lines[i + 1], @".*?(\d+).*?(\d+)");
    long bx = Convert.ToInt32(m.Groups[1].Value);
    long by = Convert.ToInt32(m.Groups[2].Value);

    m = Regex.Match(lines[i + 2], @".*?(\d+).*?(\d+)");
    long px = Convert.ToInt64(m.Groups[1].Value) + 10000000000000;
    long py = Convert.ToInt64(m.Groups[2].Value) + 10000000000000;

    //long cost = Cost(ax, ay, bx, by, px, py);
    long cost = Cost2(ax, ay, bx, by, px, py);
    //Cost3(ax, ay, bx, by, px, py);
    if (cost != long.MaxValue)
    {
        Console.WriteLine(cost);

        sum += cost;
    }
}

long Cost2(long ax, long ay, long bx, long by, long px, long py)
{
    long B = (ay * px - ax * py) / (ay * bx - ax * by);
    long A = (px - B * bx) / ax;

    long npx = ax * A + bx * B;
    long npy = ay * A + by * B;

    A = (by * px - bx * py) / (by * ax - bx * ay);
    //B = (px - A * ax) / bx;

    npx = ax * A + bx * B;
    npy = ay * A + by * B;

    if (npx != px || npy != py)
        return 0;

    Console.WriteLine($"{A} {B} {B + 3 * A}");

    return B + 3 * A;
}

Console.WriteLine(sum);

long Cost(long ax, long ay, long bx, long by, long px, long py)
{
    long locx = 0;
    long locy = 0;

    long stepb = 0;
    long mincost = 0;
    while (locx < px && locy < py)
    {
        locx = stepb * bx;
        locy = stepb * by;

        if ((px - locx) % ax == 0
            && (py - locy) % ay == 0
            && Math.Abs((px - locx) / ax - (py - locy) / ay) < 0.01)
        {
            long cost = stepb + 3 * (long)((px - locx) / ax);
            if (mincost == 0)
            {
                mincost = cost;
            }
            else
            {
                mincost = Math.Min(mincost, cost);
            }

        }

        stepb++;
    }
    return mincost;
}
long Cost3(long ax, long ay, long bx, long by, long px, long py)
{
    long locx = 0;
    long locy = 0;

    long stepa = 0;
    long mincost = long.MaxValue;
    while (locx < px && locy < py && 3 * stepa < mincost)
    {
        locx = stepa * ax;
        locy = stepa * ay;

        if ((px - locx) % bx == 0
            && (py - locy) % by == 0
            && (long)((px - locx) / bx) == (long)((py - locy) / by))
        {
            long cost = 3 * stepa + (long)((px - locx) / bx);
            Console.WriteLine($"{stepa} {(long)((px - locx) / bx)} {cost}");
            mincost = Math.Min(mincost, cost);
        }

        stepa++;
    }
    return mincost;
}

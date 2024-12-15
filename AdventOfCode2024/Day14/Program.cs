using System.Text.RegularExpressions;

var lines = File.ReadAllLines("data.txt");

List<Robot> robots = [];

foreach (var line in lines)
{
    //p=0,4 v=3,-3
    var m = Regex.Match(line, @"p=(\d+),(\d+) v=(.*),(.*)$");
    robots.Add(new Robot(
        Convert.ToInt32(m.Groups[1].Value),
        Convert.ToInt32(m.Groups[2].Value),
        Convert.ToInt32(m.Groups[3].Value),
        Convert.ToInt32(m.Groups[4].Value)));
}

//int mapx = 11;
//int mapy = 7;


int mapx = 101;
int mapy = 103;

for (int t = 0; t < 100000; t++)
{
    foreach (var robot in robots)
        robot.DoStep(mapx, mapy);

    if (NoDoubles())
    {

        Console.WriteLine(t);
        for (int y = 0; y < mapy; y++)
        {
            for (int x = 0; x < mapx; x++)
            {
                int c = robots.Count(r => r.PosX == x && r.PosY == y);
                Console.Write(c == 0 ? "." : c.ToString());
            }
            Console.WriteLine();
        }

        Console.ReadLine();
    }
}

bool NoDoubles()
{
    for (int y = 0; y < mapy; y++)
    {
        for (int x = 0; x < mapx; x++)
        {
            if (robots.Count(r => r.PosX == x && r.PosY == y) > 1)
                return false;
        }
    }

    return true;
}

for (int y = 0; y < mapy; y++)
{
    for (int x = 0; x < mapx; x++)
    {
        int c = robots.Count(r => r.PosX == x && r.PosY == y);
        Console.Write(c == 0 ? "." : c.ToString());
    }
    Console.WriteLine();
}

int q1 = 0;
int q2 = 0;
int q3 = 0;
int q4 = 0;

foreach (var robot in robots)
{
    Console.WriteLine($"{robot.PosX} {robot.PosY}");

    if ((robot.PosX < mapx / 2) && (robot.PosY < mapy / 2))
        q1++;

    if ((robot.PosX > mapx / 2) && (robot.PosY < mapy / 2))
        q2++;

    if ((robot.PosX < mapx / 2) && (robot.PosY > mapy / 2))
        q3++;

    if ((robot.PosX > mapx / 2) && (robot.PosY > mapy / 2))
        q4++;


}


Console.WriteLine($"q {q1} {q2} {q3} {q4}");
Console.WriteLine($"total {(long)q1 * q2 * q3 * q4}");


//Console.WriteLine($"total {sumsolve}");

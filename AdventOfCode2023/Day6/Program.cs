var lines = File.ReadAllLines("dataTest.txt");

var times = lines[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
var distances = lines[1].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

long prod = 1;

for (int i = 0; i < times.Length; i++)
{
    int time = int.Parse(times[i]);
    int dist = int.Parse(distances[i]);

    int better = 0;
    for (int j = 1; j < time - 1; j++)
    {
        int d = (time - j) * j;
        if (d > dist)
            better++;
    }

    Console.WriteLine(better);
    prod *= better;
}

Console.WriteLine($"result {prod}");

long tt = 44826981;
long dd = 202107611381458;

long x = (tt - 5085566) * 5085566;
Console.WriteLine($"x 5,085,566 = {x}   {(x > dd ? 'T' : 'F')}");
x = (tt - 5085567) * 5085567;
Console.WriteLine($"x 5,085,567 = {x}   {(x > dd ? 'T' : 'F')}");

x = (tt - 39741414) * 39741414;
Console.WriteLine($"x 39741414 = {x}   {(x > dd ? 'T' : 'F')}");
x = (tt - 39741415) * 39741415;
Console.WriteLine($"x 39741415 = {x}   {(x > dd ? 'T' : 'F')}");

Console.WriteLine($"range = {(39741415 - 5085567)}");

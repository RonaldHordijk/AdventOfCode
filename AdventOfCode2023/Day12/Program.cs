var lines = File.ReadAllLines("data.txt");

int sizeX = lines.Length;
int sizeY = lines[0].Length;
var heights = new int[sizeY, sizeY];
var path = new int[sizeY, sizeY];
int endx = 0;
int endy = 0;

for (int x = 0; x < sizeX; x++)
    for (int y = 0; y < sizeY; y++)
    {
        if (lines[x][y] == 'S')
        {
            heights[x, y] = 0;
            path[x, y] = 1;
        }
        else if (lines[x][y] == 'E')
        {
            heights[x, y] = 'z' - 'a';
            endx = x;
            endy = y;
        }
        else
        {
            heights[x, y] = lines[x][y] - 'a';
        }
    }
ShowHeightMap();


for (int i = 0; i < 1000; i++)
{
    for (int x = 0; x < sizeX; x++)
        for (int y = 0; y < sizeY; y++)
        {
            int newVal = path[x, y] != 0 ? path[x, y] : int.MaxValue;

            if (x > 0
                && path[x - 1, y] > 0 &&
                Math.Abs(heights[x, y] - heights[x - 1, y]) <= 1)
            {
                newVal = Math.Min(path[x - 1, y] + 1, newVal);
            }

            if (x < sizeX - 1
                && path[x + 1, y] > 0 &&
                Math.Abs(heights[x, y] - heights[x + 1, y]) <= 1)
            {
                newVal = Math.Min(path[x + 1, y] + 1, newVal);
            }

            if (y > 0
                && path[x, y - 1] > 0 &&
                Math.Abs(heights[x, y] - heights[x, y - 1]) <= 1)
            {
                newVal = Math.Min(path[x, y - 1] + 1, newVal);
            }

            if (y < sizeY - 1
                && path[x, y + 1] > 0 &&
                Math.Abs(heights[x, y] - heights[x, y + 1]) <= 1)
            {
                newVal = Math.Min(path[x, y + 1] + 1, newVal);
            }

            if (newVal < int.MaxValue)
                path[x, y] = newVal;
        }
    ShowMap();
}

void ShowHeightMap()
{
    for (int x = 0; x < sizeX; x++)
    {
        for (int y = 0; y < sizeY; y++)
            Console.Write(heights[x, y].ToString("00") + " ");
        Console.WriteLine();
    }
    Console.WriteLine();
}

void ShowMap()
{
    for (int x = 0; x < sizeX; x++)
    {
        for (int y = 0; y < sizeY; y++)
            Console.Write(path[x, y].ToString("00") + " ");
        Console.WriteLine();
    }
    Console.WriteLine();
}


Console.WriteLine(path[endx, endy] - 1);

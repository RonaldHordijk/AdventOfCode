var lines = File.ReadAllLines("data.txt");

char[,] map = new char[lines[0].Length, lines.Length];

int xcount = lines[0].Length;
int ycount = lines.Length;

int yy = 0;
foreach (var line in lines)
{
    for (int x = 0; x < line.Length; x++)
    {
        map[x, yy] = line[x];
    }
    yy++;
}

int nrFence = 0;
List<int> fields = [];
List<Fence> fences = [];
char current = ',';

long sum = 0;
long sum2 = 0;
for (int y = 0; y < ycount; y++)
{
    for (int x = 0; x < xcount; x++)
    {
        if (map[x, y] != '.')
        {
            nrFence = 0;
            fields = [];
            fences = [];
            current = map[x, y];
            Surround(x, y);
            Console.WriteLine($"{current},  {nrFence}, {fields.Count}");
            sum += (long)nrFence * fields.Count;

            UnDoubleFences();
            nrFence = fences.Count(f => f.DX != 0 || f.DY != 0);
            Console.WriteLine($"{current},  {nrFence}, {fields.Count}");
            sum2 += (long)nrFence * fields.Count;

            ClearFields();
        }
    }
}

Console.WriteLine($"total {sum}");
Console.WriteLine($"total {sum2}");

void UnDoubleFences()
{
    bool done = false;
    for (; !done;)
    {
        done = true;
        for (int i = 0; i < fences.Count - 1; i++)
            for (int j = i; j < fences.Count; j++)
            {
                if (fences[i].CanJoin(fences[j]))
                {
                    fences[i].Join(fences[j]);
                    fences[j].DY = 0;
                    fences[j].DX = 0;
                    done = false;
                }
            }
    }
}

void ClearFields()
{
    foreach (var field in fields)
    {
        map[field % 1000, field / 1000] = '.';
    }
}


void Surround(int x, int y)
{
    if (fields.Contains(x + y * 1000))
        return;

    fields.Add(x + y * 1000);

    if (x == 0 || map[x - 1, y] != current)
    {
        nrFence++;
        fences.Add(new(x - 0.5, y - 0.5, x - 0.5, y + 0.5, -1, 0));
    }
    else
    {
        Surround(x - 1, y);
    }

    if (x == xcount - 1 || map[x + 1, y] != current)
    {
        nrFence++;
        fences.Add(new(x + 0.5, y - 0.5, x + 0.5, y + 0.5, 1, 0));
    }
    else
    {
        Surround(x + 1, y);
    }

    if (y == 0 || map[x, y - 1] != current)
    {
        nrFence++;
        fences.Add(new(x - 0.5, y - 0.5, x + 0.5, y - 0.5, 0, -1));
    }
    else
    {
        Surround(x, y - 1);
    }

    if (y == ycount - 1 || map[x, y + 1] != current)
    {
        nrFence++;
        fences.Add(new(x - 0.5, y + 0.5, x + 0.5, y + 0.5, 0, 1));
    }
    else
    {
        Surround(x, y + 1);
    }
}

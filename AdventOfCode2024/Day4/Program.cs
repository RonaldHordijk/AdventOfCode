var lines = File.ReadAllLines("data.txt");

int xcount = lines[0].Length;
int ycount = lines.Length;


int nrxmas = 0;

for (int x = 0; x < xcount - 3; x++)
{
    for (int y = 0; y < ycount; y++)
    {
        if (lines[x][y] == 'X'
            && lines[x + 1][y] == 'M'
            && lines[x + 2][y] == 'A'
            && lines[x + 3][y] == 'S')
        {
            nrxmas++;
        }
        if (lines[x][y] == 'S'
            && lines[x + 1][y] == 'A'
            && lines[x + 2][y] == 'M'
            && lines[x + 3][y] == 'X')
        {
            nrxmas++;
        }
    }
}

for (int x = 0; x < xcount; x++)
{
    for (int y = 0; y < ycount - 3; y++)
    {
        if (lines[x][y] == 'X'
            && lines[x][y + 1] == 'M'
            && lines[x][y + 2] == 'A'
            && lines[x][y + 3] == 'S')
        {
            nrxmas++;
        }
        if (lines[x][y] == 'S'
            && lines[x][y + 1] == 'A'
            && lines[x][y + 2] == 'M'
            && lines[x][y + 3] == 'X')
        {
            nrxmas++;
        }
    }
}

for (int x = 0; x < xcount - 3; x++)
{
    for (int y = 0; y < ycount - 3; y++)
    {
        if (lines[x][y] == 'X'
            && lines[x + 1][y + 1] == 'M'
            && lines[x + 2][y + 2] == 'A'
            && lines[x + 3][y + 3] == 'S')
        {
            nrxmas++;
        }
        if (lines[x][y] == 'S'
            && lines[x + 1][y + 1] == 'A'
            && lines[x + 2][y + 2] == 'M'
            && lines[x + 3][y + 3] == 'X')
        {
            nrxmas++;
        }
        if (lines[x][y + 3] == 'X'
            && lines[x + 1][y + 2] == 'M'
            && lines[x + 2][y + 1] == 'A'
            && lines[x + 3][y + 0] == 'S')
        {
            nrxmas++;
        }
        if (lines[x][y + 3] == 'S'
            && lines[x + 1][y + 2] == 'A'
            && lines[x + 2][y + 1] == 'M'
            && lines[x + 3][y + 0] == 'X')
        {
            nrxmas++;
        }
    }
}

Console.WriteLine($"nrxmas {nrxmas}");


int nrmas = 0;

for (int x = 0; x < xcount - 2; x++)
{
    for (int y = 0; y < ycount - 2; y++)
    {
        if (lines[x + 0][y + 0] == 'M'
            && lines[x + 1][y + 1] == 'A'
            && lines[x + 2][y + 2] == 'S'
            && lines[x + 0][y + 2] == 'M'
            && lines[x + 2][y + 0] == 'S'
            )
        {
            nrmas++;
        }
        if (lines[x + 0][y + 0] == 'M'
            && lines[x + 1][y + 1] == 'A'
            && lines[x + 2][y + 2] == 'S'
            && lines[x + 0][y + 2] == 'S'
            && lines[x + 2][y + 0] == 'M'
            )
        {
            nrmas++;
        }
        if (lines[x + 0][y + 0] == 'S'
            && lines[x + 1][y + 1] == 'A'
            && lines[x + 2][y + 2] == 'M'
            && lines[x + 0][y + 2] == 'M'
            && lines[x + 2][y + 0] == 'S'
            )
        {
            nrmas++;
        }
        if (lines[x + 0][y + 0] == 'S'
            && lines[x + 1][y + 1] == 'A'
            && lines[x + 2][y + 2] == 'M'
            && lines[x + 0][y + 2] == 'S'
            && lines[x + 2][y + 0] == 'M'
            )
        {
            nrmas++;
        }
    }
}

Console.WriteLine($"nrmas {nrmas}");

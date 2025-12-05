var lines = File.ReadAllLines("data.txt");

int xcount = lines[0].Length;
int ycount = lines.Length;

char[][] chars = lines.Select(s => s.ToCharArray()).ToArray();



int count = 0;

bool found = true;
while (found)
{
    found = false;


    for (int x = 0; x < xcount; x++)
    {
        for (int y = 0; y < ycount; y++)
        {
            int nb = 0;

            if (chars[x][y] != '@')
                continue;

            for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, xcount - 1); i++)
            {
                for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, ycount - 1); j++)
                {
                    if (chars[i][j] == '@')
                    {
                        nb++;
                    }
                }
            }

            if (nb < 5)
            {
                count++;
                chars[x][y] = '.';
                found = true;
            }
        }
    }
}

Console.WriteLine($"count {count}");

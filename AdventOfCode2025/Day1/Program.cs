var lines = File.ReadAllLines("data.txt");

int pos = 50;
int cnt = 0;

//0x434C49434B
// CLICK

foreach (var line in lines)
{
    int startpos = pos;

    int step = int.Parse(line[1..]);
    if (line[0] == 'L')
    {
        pos -= step;
    }
    else if (line[0] == 'R')
    {
        pos += step;
    }

    if (pos < 0)
    {
        if (startpos == 0)
            cnt--;

        while (pos < 0)
        {
            pos += 100;
            cnt++;
        }

        if (pos == 0)
        {
            cnt++;
        }
    }
    else if (pos > 99)
    {
        while (pos > 99)
        {
            pos -= 100;
            cnt++;
        }

    }
    else if (pos == 0)
    {
        cnt++;
    }
}


Console.WriteLine($"cnt {cnt}");

// part 2

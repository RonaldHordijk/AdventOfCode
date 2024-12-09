var lines = File.ReadAllLines("data.txt");

var compressed = lines[0].Select(c => c - '0');

int cnt = 0;
bool space = false;
List<int> disk = [];
foreach (var c in compressed)
{
    if (space)
    {
        for (int i = 0; i < c; i++)
            disk.Add(-1);
    }
    else
    {
        for (int i = 0; i < c; i++)
            disk.Add(cnt);
        cnt++;
    }

    space = !space;
}

//foreach (var c in disk)
//{
//    Console.Write(c.ToString());
//}
//Console.WriteLine();

int last = disk.Count - 1;
int first = 0;
for (; ; )
{
    if (last <= first)
        break;

    if (disk[last] < 0)
    {
        last--;
        continue;
    }

    if (disk[first] >= 0)
    {
        first++;
        continue;
    }

    (disk[first], disk[last]) = (disk[last], disk[first]);
}

//foreach (var c in disk)
//{
//    Console.Write(c.ToString());
//}
//Console.WriteLine();

long sum = disk.Where(d => d >= 0).Select((d, i) => (long)(d * i)).Sum();

Console.WriteLine($" first checksum {sum}");

List<Part> disk2 = [];
cnt = 0;
space = false;
foreach (var c in compressed)
{
    if (space)
    {
        disk2.Add(new Part(c, true, -1));
    }
    else
    {
        disk2.Add(new Part(c, false, cnt));
        cnt++;
    }

    space = !space;
}

for (int i = disk2.Count - 1; i > 0; i--)
{
    if (disk2[i].IsHole)
        continue;

    //find hole
    for (int j = 0; j < i; j++)
    {
        if (disk2[j].IsHole && disk2[j].Size >= disk2[i].Size)
        {
            disk2[j].IsHole = false;
            disk2[j].Value = disk2[i].Value;

            disk2[i].IsHole = true;
            disk2[i].Value = -1;

            if (disk2[j].Size > disk2[i].Size)
            {
                int newsize = disk2[j].Size - disk2[i].Size;
                disk2[j].Size = disk2[i].Size;

                disk2.Insert(j + 1, new Part(newsize, true, -1));
                i++;
            }
            break;
        }
    }
}

// unpack
disk = [];
foreach (var d2 in disk2)
{
    for (int i = 0; i < d2.Size; i++)
        disk.Add(d2.Value);
}

foreach (var c in disk)
{
    Console.Write(c.ToString());
}
Console.WriteLine();

sum = disk.Select((d, i) => (long)(d > 0 ? d * i : 0)).Sum();

Console.WriteLine($"second checksum {sum}");

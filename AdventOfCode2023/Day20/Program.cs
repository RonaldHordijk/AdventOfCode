var lines = File.ReadAllLines("data.txt");

var data = lines.Select(x => Int64.Parse(x)).ToArray();
var pos = new List<int>();
var work = (long[])data.Clone();

for (int i = 0; i < data.Length; i++)
    pos.Add(i);

for (int i = 0; i < data.Length; i++)
{
    data[i] *= 811589153;
    work[i] *= 811589153;
}

for (int ii = 0; ii < 10; ii++)
{
    for (int i = 0; i < data.Length; i++)
    {
        int p = pos[i];
        int value = (int)(data[i] % (data.Length - 1));

        if (value > 0)
        {
            for (int j = 0; j < value; j++)
            {
                int n1 = (p + j) % data.Length;
                int n2 = (p + j + 1) % data.Length;

                (work[n1], work[n2]) = (work[n2], work[n1]);

                int p1 = pos.IndexOf(n1);
                int p2 = pos.IndexOf(n2);

                pos[p1] = n2;
                pos[p2] = n1;
            }
        }
        else if (value < 0)
        {
            for (int j = 0; j < Math.Abs(value); j++)
            {
                int n1 = (p - j + 2 * data.Length) % data.Length;
                int n2 = (p - j - 1 + 2 * data.Length) % data.Length;

                (work[n1], work[n2]) = (work[n2], work[n1]);

                int p1 = pos.IndexOf(n1);
                int p2 = pos.IndexOf(n2);

                pos[p1] = n2;
                pos[p2] = n1;
            }
        }
    }
    //Dump();
}

int i0 = IndexOf0();

long v1000 = work[(i0 + 1000) % work.Length];
long v2000 = work[(i0 + 2000) % work.Length];
long v3000 = work[(i0 + 3000) % work.Length];

Console.WriteLine(v1000 + v2000 + v3000);

//var d = lines.Select(x => Int32.Parse(x)).DistinctBy(x => x).ToArray();
//Console.WriteLine(data.Length);
//Console.WriteLine(d.Length);


int IndexOf0()
{
    for (int i = 0; i < work.Length; ++i)
    {
        if (work[i] == 0)
            return i;
    }

    return -1;
}


void Dump()
{
    Console.WriteLine(string.Join(" ", work));
}

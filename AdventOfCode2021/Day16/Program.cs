var lines = File.ReadAllLines("data.txt");

List<int> bits = new();

//lines[0] = "9C0141080250320F1802104A08";

foreach (var c in lines[0])
{
    int v = int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber);
    if (v >= 8)
    {
        bits.Add(1);
        v -= 8;
    }
    else
    {
        bits.Add(0);
    }

    if (v >= 4)
    {
        bits.Add(1);
        v -= 4;
    }
    else
    {
        bits.Add(0);
    }

    if (v >= 2)
    {
        bits.Add(1);
        v -= 2;
    }
    else
    {
        bits.Add(0);
    }

    bits.Add(v);
}

Console.WriteLine(string.Join("", bits.Select(c => c.ToString())));

int versionSum = 0;
int pos = 0;
while (pos < bits.Count)
{
    int version = getvalue(pos, 3);
    versionSum += version;
    pos += 3;
    int type = getvalue(pos, 3);
    pos += 3;
    if (type == 4)
    {
        // read value
        Int64 value = 0;
        bool last = false;
        while (!last)
        {
            last = bits[pos] == 0;
            pos++;
            value *= 16;
            value += getvalue(pos, 4);
            pos += 4;
        }

        if (pos % 4 != 0)
            pos = 4 * (pos / 4) + 4;
    }
    else
    {
        List<Int64> values = new();
        // operator
        int ident = bits[pos];
        pos++;
        if (ident == 0)
        {
            int length = getvalue(pos, 15);
            pos += 15;

            int pend = pos + length;
            while (pos < pend)
            {
                values.Add(GetSubPacket());
            }
        }
        else
        {
            int length = getvalue(pos, 11);
            pos += 11;

            for (int i = 0; i < length; i++)
            {
                values.Add(GetSubPacket());
            }
        }

        Console.WriteLine(Process(type, values));

        if (pos % 4 != 0)
            pos = 4 * (pos / 4) + 4;

        pos += 4;
    }
}

Int64 Process(int type, List<long> values)
{
    if (type == 0)
    {
        return values.Sum();
    }
    else if (type == 1)
    {
        long res = 1;
        foreach (var v in values)
            res *= v;
        return res;
    }
    else if (type == 2)
    {
        return values.Min();
    }
    else if (type == 3)
    {
        return values.Max();
    }
    else if (type == 5)
    {
        return values[0] > values[1] ? 1 : 0;
    }
    else if (type == 6)
    {
        return values[0] < values[1] ? 1 : 0;
    }
    else if (type == 7)
    {
        return values[0] == values[1] ? 1 : 0;
    }

    return 0;
}

Console.WriteLine(versionSum);

Int64 GetSubPacket()
{
    int startp = pos;

    int version = getvalue(pos, 3);
    versionSum += version;
    pos += 3;
    int type = getvalue(pos, 3);
    pos += 3;
    if (type == 4)
    {
        // read values
        Int64 value = 0;
        bool last = false;
        while (!last)
        {
            last = bits[pos] == 0;
            pos++;
            value *= 16;
            value += getvalue(pos, 4);
            pos += 4;
        }

        return value;
    }
    else
    {
        List<Int64> values = new();
        // operator
        int ident = bits[pos];
        pos++;
        if (ident == 0)
        {
            int length = getvalue(pos, 15);
            pos += 15;

            int pend = pos + length;
            while (pos < pend)
            {
                values.Add(GetSubPacket());
            }
        }
        else
        {
            int length = getvalue(pos, 11);
            pos += 11;

            for (int i = 0; i < length; i++)
            {
                values.Add(GetSubPacket());
            }
        }

        return Process(type, values);
    }
}

int getvalue(int pos, int length)
{
    int res = 0;
    for (int i = 0; i < length; i++)
    {
        res = res * 2 + bits[pos + i];
    }

    return res;
}

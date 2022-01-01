var lines = File.ReadAllLines("d0.txt").ToList();

//lines.Clear();
//lines.Add("[1,1]");
//lines.Add("[2,2]");
//lines.Add("[3,3]");
//lines.Add("[4,4]");
//lines.Add("[5,5]");
//lines.Add("[6,6]");

Int64 maxVal = 0;
for (int i = 0; i < lines.Count; i++)
    for (int j = 0; j < lines.Count; j++)
    {
        if (i == j)
            continue;

        var s = Add(lines[i], lines[j]);
        Int64 val = GetValue(s);
        if (val > maxVal)
            maxVal = val;
    }

Console.WriteLine(maxVal);


var sum = lines[0];
for (int i = 1; i < lines.Count; i++)
{
    sum = Add(sum, lines[i]);
    Console.WriteLine(sum);
}

Int64 c = GetValue(sum);
Console.WriteLine(c);

long GetValue(string s)
{
    while (true)
    {
        var commaPositions = GetGroupCommas(s);
        if (commaPositions.Count == 0)
            return Int64.Parse(s);

        var pos = commaPositions[0];
        var (pstart, pend) = GetGroup(s, pos);
        var v1 = Int32.Parse(s[(pstart + 1)..pos]);
        var v2 = Int32.Parse(s[(pos + 1)..(pend)]);

        s = s[0..pstart] + (v1 * 3 + v2 * 2).ToString() + s[(pend + 1)..];
    }


}

(int, int) GetGroup(string s, int pos)
{
    int start = 0;
    int end = 0;
    for (int i = pos - 1; i > 0; i--)
    {
        if (s[i] == '[')
        {
            start = i;
            break;
        }
    }

    for (int i = pos + 1; i < s.Length; i++)
    {
        if (s[i] == ']')
        {
            end = i;
            break;
        }
    }

    return (start, end);

}

//var s = Add("[[[[4,3],4],4],[7,[[8,4],9]]]", "[1,1]");

string Add(string v1, string v2)
{
    var res = $"[{v1},{v2}]";
    //Console.WriteLine(res);

    while (true)
    {
        res = AllExplosions(res);
        res = Split(res);

        var pos1 = GetExplosionPos(res);
        var pos2 = GetSplitPosition(res);
        if (pos1 < 0 && pos2 < 0)
            break;
    }
    return res;
}

string AllExplosions(string res)
{
    while (true)
    {
        int pos = GetExplosionPos(res);
        if (pos < 0)
            break;

        res = Explode(res, pos);
        //Console.WriteLine(res);
    }
    return res;
}

string Split(string s)
{
    var pos = GetSplitPosition(s);
    if (pos < 0)
        return s;

    var value = Int32.Parse(s[pos..(pos + 2)]);

    int v1 = value / 2;
    int v2 = v1 + value % 2;

    s = s[0..pos] + $"[{v1},{v2}]" + s[(pos + 2)..];
    //Console.WriteLine(s);
    return s;
}

int GetSplitPosition(string s)
{
    for (int i = 0; i < s.Length - 1; i++)
    {
        if (Char.IsDigit((char)s[i])
            && Char.IsDigit((char)s[i + 1]))
            return i;
    }

    return -1;
}

int GetExplosionPos(string res)
{
    foreach (var pos in GetGroupCommas(res))
    {
        if (Depth(res, pos) >= 5)
            return pos;
    }
    return -1;
}

string Explode(string res, int pos)
{
    var preValue = Int32.Parse(res[(pos - 1)..pos]);
    var postValue = Int32.Parse(res[(pos + 1)..(pos + 2)]);
    var pre = res[0..(pos - 2)];
    var post = res[(pos + 3)..];

    if (char.IsDigit((char)res[pos - 2]))
    {
        preValue = Int32.Parse(res[(pos - 2)..pos]);
        pre = res[0..(pos - 3)];
    }
    if (char.IsDigit((char)res[pos + 2]))
    {
        postValue = Int32.Parse(res[(pos + 1)..(pos + 3)]);
        post = res[(pos + 4)..];
    }

    var posDigit = FindLastDigit(pre);
    if (posDigit != -1)
    {
        preValue += Int32.Parse(pre[posDigit..(posDigit + 1)]);
        if (char.IsDigit((char)pre[posDigit - 1]))
        {
            preValue -= Int32.Parse(pre[posDigit..(posDigit + 1)]);
            preValue += Int32.Parse(pre[(posDigit - 1)..(posDigit + 1)]);
            pre = pre[0..(posDigit - 1)] + preValue.ToString() + pre[(posDigit + 1)..];
        }
        else
        {
            pre = pre[0..posDigit] + preValue.ToString() + pre[(posDigit + 1)..];
        }
    }

    posDigit = FindFirstDigit(post);
    if (posDigit != -1)
    {
        postValue += Int32.Parse(post[posDigit..(posDigit + 1)]);
        if (char.IsDigit((char)post[posDigit + 1]))
        {
            postValue -= Int32.Parse(post[posDigit..(posDigit + 1)]);
            postValue += Int32.Parse(post[posDigit..(posDigit + 2)]);

            post = post[0..posDigit] + postValue.ToString() + post[(posDigit + 2)..];
        }
        else
        {
            post = post[0..posDigit] + postValue.ToString() + post[(posDigit + 1)..];
        }
    }

    return pre + "0" + post;
}

int FindFirstDigit(string s)
{
    for (int i = 0; i < s.Length; i++)
    {
        if (Char.IsDigit((char)s[i]))
            return i;
    }

    return -1;
}

int FindLastDigit(string s)
{
    for (int i = s.Length - 1; i > 0; i--)
    {
        if (Char.IsDigit((char)s[i]))
            return i;
    }

    return -1;
}

int Depth(string res, int pos)
{
    var work = res[0..pos];
    return work.Count(c => c == '[') - work.Count(c => c == ']');
}

List<int> GetGroupCommas(string sum)
{
    var res = new List<int>();
    for (int i = 1; i < sum.Length - 1; i++)
    {
        if ((sum[i] == ',')
            && Char.IsDigit((char)sum[i - 1])
            && Char.IsDigit((char)sum[i + 1]))
            res.Add(i);
    }
    return res;
}

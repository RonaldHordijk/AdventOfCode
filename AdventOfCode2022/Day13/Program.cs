using Day13;

var lines = File.ReadAllLines("data.txt");

int index = 1;
int sum = 0;

for (int i = 0; i < lines.Length; i += 3)
{
    var l1 = Parse(lines[i]);
    var l2 = Parse(lines[i + 1]);

    if (Compare(l1, l2) < 0)
    {
        Console.WriteLine("Wrong order");
    }
    else
    {
        Console.WriteLine("Right order");
        sum += index;
    }

    index++;
}

Console.WriteLine($"sum is {sum}");

List<ListElement> elements = new();
for (int i = 0; i < lines.Length; i += 3)
{
    elements.Add(Parse(lines[i]));
    elements.Add(Parse(lines[i + 1]));
}

var e2 = Parse("[[2]]");
var e6 = Parse("[[6]]");
elements.Add(e2);
elements.Add(e6);

elements.Sort(Compare);
elements.Reverse();

int i2 = elements.IndexOf(e2);
int i6 = elements.IndexOf(e6);

Console.WriteLine($"loc e2 {i2} e6 {i6} = tot {(i2 + 1) * (i6 + 1)}");


int Compare(ListElement l1, ListElement l2)
{
    if (!l1.IsList && !l2.IsList)
        return Math.Sign(l2.Value - l1.Value);

    if (l1.IsList && !l2.IsList)
    {
        return Compare(l1, new ListElement { IsList = true, List = { l2 } });
    }

    if (!l1.IsList && l2.IsList)
    {
        return Compare(new ListElement { IsList = true, List = { l1 } }, l2);
    }

    if (l1.IsList && l2.IsList)
    {
        int nrItem = Math.Min(l1.List.Count, l2.List.Count);

        for (int i = 0; i < nrItem; i++)
        {
            int res = Compare(l1.List[i], l2.List[i]);
            if (res != 0)
                return res;
        }

        if (l2.List.Count == l1.List.Count)
            return 0;

        return Math.Sign(l2.List.Count - l1.List.Count);
    }

    return 0;
}

ListElement Parse(string v)
{

    if (IsList(v))
    {
        // list
        string listtext = v = v[1..^1];
        ListElement result = new ListElement { IsList = true };
        while (!string.IsNullOrEmpty(listtext))
        {
            (string element, string rest) = GetElement(listtext);

            result.List.Add(Parse(element));

            listtext = rest;
        }

        return result;
    }

    return new ListElement
    {
        Value = int.Parse(v),
    };
}

bool IsList(string v)
{
    return v.StartsWith("[") && v.EndsWith("]");
}

(string element, string rest) GetElement(string v)
{
    if (v.StartsWith("["))
    {
        var indexEnd = GetClosingIndex(v) + 1;
        if (indexEnd == v.Length)
        {
            return (v, string.Empty);
        }

        return (v[..indexEnd], v[(indexEnd + 1)..]);
    }

    // text
    int i = v.IndexOf(",");
    if (i < 0)
    {
        return (v, string.Empty);
    }
    else
    {
        return (v[..i], v[(i + 1)..]);
    }

}

int GetClosingIndex(string v)
{
    int indent = 0;

    for (int i = 0; i < v.Length; i++)
    {
        if (v[i] == '[')
        {
            indent++;
        }

        if (v[i] == ']')
        {
            indent--;
            if (indent == 0)
                return i;
        }
    }

    return 0;
}

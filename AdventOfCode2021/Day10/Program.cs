var lines = File.ReadAllLines("data.txt");

int sum = 0;
var completes = new List<Int64>();

foreach (var line in lines)
{
    var stack = new Stack<char>();

    var error = false;
    foreach (var c in line)
    {
        if (error)
            break;

        switch (c)
        {
            case '(':
            case '[':
            case '<':
            case '{':
                stack.Push(c);
                break;
            case ')':
                if (stack.Pop() != '(')
                {
                    sum += 3;
                    error = true;
                }
                break;
            case ']':
                if (stack.Pop() != '[')
                {
                    sum += 57;
                    error = true;
                }
                break;
            case '}':
                if (stack.Pop() != '{')
                {
                    sum += 1197;
                    error = true;
                }
                break;
            case '>':
                if (stack.Pop() != '<')
                {
                    sum += 25137;
                    error = true;
                }
                break;
        }
    }
    if (!error)
    {
        Int64 lc = 0;
        while (stack.Count > 0)
        {
            switch (stack.Pop())
            {
                case '(':
                    lc = lc * 5 + 1;
                    break;
                case '[':
                    lc = lc * 5 + 2;
                    break;
                case '{':
                    lc = lc * 5 + 3;
                    break;
                case '<':
                    lc = lc * 5 + 4;
                    break;
            }
        }
        Console.WriteLine(lc);
        completes.Add(lc);
    }
}
completes.Sort();

Console.WriteLine($"sum is {sum}");

Console.WriteLine($"complete is {completes[completes.Count / 2]}");

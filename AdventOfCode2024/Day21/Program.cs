var lines = File.ReadAllLines("data.txt");
//184868
//185828
//179444

//412567997103408
//161541292646534
//223285811665866

Dictionary<int, Dictionary<string, long>> buffer = [];

long sum = 0;
long sum2 = 0;
foreach (var line in lines)
{
    Console.WriteLine(line);
    string l1 = GetNumCode(line);
    Console.WriteLine(l1);

    //string l2 = GetDirCode(l1);
    //Console.WriteLine(l2);
    //string l3 = GetDirCode(l2);
    //Console.WriteLine(l3);

    long l2 = GetCodeLength(l1, 25);
    long l = GetCodeLength(l1, 2);

    Console.WriteLine($" {l2} {l} {line[..^1]}");
    sum2 += l2 * Int32.Parse(line[..^1]);
    sum += l * Int32.Parse(line[..^1]);
}

Console.WriteLine($"sum {sum2}");
Console.WriteLine($"sum {sum}");

long GetCodeLength(string line, int depth)
{
    if (!buffer.TryGetValue(depth, out var buf))
    {
        buf = new Dictionary<string, long>();
        buffer.Add(depth, buf);
    }

    if (buf.TryGetValue(line, out long length))
        return length;

    if (depth > 1)
    {
        length = 0;

        char pos = 'A';
        foreach (char c in line)
        {
            string s = GetDirOptions(pos, c)[0] + 'A';
            //Console.WriteLine($"## {pos} {c} {depth} {s}");
            length += GetCodeLength(s, depth - 1);

            pos = c;
        }

        //string work = line;
        //while (work.Length > 0)
        //{
        //    int i = work.IndexOf('A');
        //    string s1 = work[..(i + 1)];
        //    length += GetCodeLength(GetDirCode(s1), depth - 1);
        //    work = work[(i + 1)..];
        //}

        buf.Add(line, length);

        return length;
    }
    else
    {
        //string s = GetDirCode(line);
        //length = s.Length;

        //Console.WriteLine($"## {line} {depth} {s}");

        char pos = 'A';
        foreach (char c in line)
        {
            string s = GetDirOptions(pos, c)[0] + 'A';
            //Console.WriteLine($"## {pos} {c} {depth} {s}");
            length += s.Length;

            pos = c;
        }

        buf.Add(line, length);

        return length;
    }
}

string GetDirCode(string line)
{
    char pos = 'A';
    string result = "";

    foreach (char c in line)
    {
        var options = GetDirOptions(pos, c);
        result += options[0] + 'A';
        pos = c;
    }

    return result;
}

string GetNumCode(string line)
{
    char pos = 'A';
    string result = "";

    foreach (char c in line)
    {
        List<string> options = [];

        switch (pos)
        {
            case 'A':
                result += c switch {
                    '0' => "<",
                    '1' => "^<<",
                    '2' => "<^",
                    '3' => "^",
                    '4' => "^^<<",
                    '5' => "^^<",
                    '6' => "^^",
                    '7' => "^^^<<",
                    '8' => "^^^<",
                    '9' => "^^^",
                    'A' => "",
                    _ => ""
                };
                break;
            case '0':
                result += c switch {
                    '0' => "",
                    '1' => "^<",
                    '2' => "^",
                    '3' => ">^",
                    '4' => "^^<",
                    '5' => "^^",
                    '6' => "^^>",
                    '7' => "^^^<",
                    '8' => "^^^",
                    '9' => "^^^>",
                    'A' => ">",
                    _ => ""
                };
                break;
            case '1':
                result += c switch {
                    '0' => ">v",
                    '1' => "",
                    '2' => ">",
                    '3' => ">>",
                    '4' => "^",
                    '5' => ">^",
                    '6' => "^>>",
                    '7' => "^^",
                    '8' => "^^>",
                    '9' => "^^>>",
                    'A' => ">>v",
                    _ => ""
                };
                break;
            case '2':
                result += c switch {
                    '0' => "v",
                    '1' => "<",
                    '2' => "",
                    '3' => ">",
                    '4' => "<^",
                    '5' => "^",
                    '6' => ">^",
                    '7' => "<^^",
                    '8' => "^^",
                    '9' => ">^^",
                    'A' => "v>",
                    _ => ""
                };
                break;
            case '3':
                result += c switch {
                    '0' => "<v",
                    '1' => "<<",
                    '2' => "<",
                    '3' => "",
                    '4' => "^<<",
                    '5' => "<^",
                    '6' => "^",
                    '7' => "<<^^",
                    '8' => "<^^",
                    '9' => "^^",
                    'A' => "v",
                    _ => ""
                };
                break;
            case '4':
                result += c switch {
                    '0' => ">vv",
                    '1' => "v",
                    '2' => "v>",
                    '3' => "v>>",
                    '4' => "",
                    '5' => ">",
                    '6' => ">>",
                    '7' => "^",
                    '8' => "^>",
                    '9' => "^>>",
                    'A' => ">>vv",
                    _ => ""
                };
                break;
            case '5':
                result += c switch {
                    '0' => "vv",
                    '1' => "<v",
                    '2' => "v",
                    '3' => "v>",
                    '4' => "<",
                    '5' => "",
                    '6' => ">",
                    '7' => "<^",
                    '8' => "^",
                    '9' => ">^",
                    'A' => "vv>",
                    _ => ""
                };
                break;
            case '6':
                result += c switch {
                    '0' => "vv<",
                    '1' => "v<<",
                    '2' => "<v",
                    '3' => "v",
                    '4' => "<<",
                    '5' => "<",
                    '6' => "",
                    '7' => "^<<",
                    '8' => "<^",
                    '9' => "^",
                    'A' => "vv",
                    _ => ""
                };
                break;
            case '7':
                result += c switch {
                    '0' => ">vvv",
                    '1' => "vv",
                    '2' => "vv>",
                    '3' => "vv>>",
                    '4' => "v",
                    '5' => "v>",
                    '6' => "v>>",
                    '7' => "",
                    '8' => ">",
                    '9' => ">>",
                    'A' => ">>vvv",
                    _ => ""
                };
                break;
            case '8':
                result += c switch {
                    '0' => "vvv",
                    '1' => "vv<",
                    '2' => "vv",
                    '3' => "vv>",
                    '4' => "<v",
                    '5' => "v",
                    '6' => "v>",
                    '7' => "<",
                    '8' => "",
                    '9' => ">",
                    'A' => ">vvv",
                    _ => ""
                };
                break;
            case '9':
                result += c switch {
                    '0' => "vvv<",
                    '1' => "vv<<",
                    '2' => "vv<",
                    '3' => "vv",
                    '4' => "v<<",
                    '5' => "<v",
                    '6' => "v",
                    '7' => "<<",
                    '8' => "<",
                    '9' => "",
                    'A' => "vvv",
                    _ => ""
                };
                break;
        }

        result += 'A';

        pos = c;
    }

    return result;
}


static List<string> GetDirOptions(char from, char to)
{
    List<string> options = [];

    switch (from)
    {
        case 'A':
            options = to switch {
                '^' => ["<"],
                '<' => ["v<<", "<v<"],
                'v' => ["<v", "v<"],
                '>' => ["v"],
                'A' => [""],
                _ => [""]
            };
            break;
        case '^':
            options = to switch {
                '^' => [""],
                '<' => ["v<"],
                'v' => ["v"],
                '>' => ["v>", "<v"],
                'A' => [">"],
                _ => [""]
            };
            break;
        case '<':
            options = to switch {
                '^' => [">^"],
                '<' => [""],
                'v' => [">"],
                '>' => [">>"],
                'A' => [">>^", ">^>"],
                _ => [""]
            };
            break;
        case 'v':
            options = to switch {
                '^' => ["^"],
                '<' => ["<"],
                'v' => [""],
                '>' => [">"],
                'A' => ["^>", ">^"],
                _ => [""]
            };
            break;
        case '>':
            options = to switch {
                '^' => ["<^", "^<"],
                '<' => ["<<"],
                'v' => ["<"],
                '>' => [""],
                'A' => ["^"],
                _ => [""]
            };
            break;
    }

    return options;
}

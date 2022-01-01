Cleaned("13579246899999");
Generated("13579246899999");

//       01234567890123
Cleaned("92969593497992");
Generated("92969593497992");

//         01234567890123
Cleaned("81514171161381");
Generated("81514171161381");

Cleaned("24758582374093");
Generated("24758582374093");


Cleaned("11111111111111");
Generated("11111111111111");

Cleaned("99999999999999");
Generated("99999999999999");

void Cleaned(string code)
{
    int w = 0;
    int x = 0;
    int y = 0;
    int z = 0;
    int i = 0;

    int[] buffer = code.Select(c => (int)Char.GetNumericValue(c)).ToArray();

    if (buffer[3] + 3 == buffer[4])
    {
        z = 26 * (buffer[0] + 1) + buffer[1] + 11;

        if (buffer[2] - 4 != buffer[5])
        {
            z = 26 * (26 * (buffer[0] + 1) + buffer[1] + 11) + buffer[5] + 9;
        }
    }
    else
    {
        z = 26 * (26 * (buffer[0] + 1) + buffer[1] + 11) + buffer[2] + 1;
        if (buffer[4] - 3 != buffer[5])
        {
            z = 26 * (26 * (26 * (buffer[0] + 1) + buffer[1] + 11) + buffer[2] + 1) + buffer[5] + 9;
        }
    }

    z = 26 * z + buffer[6] + 7;

    z = z / 26;
    if (buffer[6] - 6 == buffer[7])
    {
        x = 0;
    }
    else
    {
        z = 26 * (z) + buffer[7] + 11;
    }

    z = 26 * z + buffer[8] + 6;

    z = z / 26;
    if (buffer[8] + 5 == buffer[9])
    {
        x = 0;
    }
    else
    {
        z = 26 * (z) + buffer[9] + 15;
    }

    z = 26 * z + buffer[10] + 7;

    x = buffer[10] + 2;
    z = z / 26;
    if (buffer[10] + 2 == buffer[11])
    {
        x = 0;
    }
    else
    {
        z = 26 * (z) + buffer[11] + 15;
    }

    // x = buffer[1] + 7
    w = buffer[12];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 26;
    x += -4;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 8;
    y *= x;
    z += y;
    // x = buffer[0] -7

    w = buffer[13];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 26;
    x += -8;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 6;
    y *= x;
    z += y;


    Console.WriteLine(z);
}
void Generated(string code)
{
    int w = 0;
    int x = 0;
    int y = 0;
    int z = 0;
    int i = 0;

    int[] buffer = code.Select(c => (int)Char.GetNumericValue(c)).ToArray();
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 1;
    x += 11;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 1;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 1;
    x += 11;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 11;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 1;
    x += 14;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 1;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 1;
    x += 11;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 11;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 26;
    x += -8;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 2;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 26;
    x += -5;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 9;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 1;
    x += 11;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 7;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 26;
    x += -13;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 11;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 1;
    x += 12;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 6;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 26;
    x += -1;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 15;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 1;
    x += 14;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 7;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 26;
    x += -5;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 1;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 26;
    x += -4;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 8;
    y *= x;
    z += y;
    w = buffer[i++];
    x *= 0;
    x += z;
    x = x % 26;
    z /= 26;
    x += -8;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y *= 0;
    y += 25;
    y *= x;
    y += 1;
    z *= y;
    y *= 0;
    y += w;
    y += 6;
    y *= x;
    z += y;

    Console.WriteLine(z);
}

void printcode()
{
    var lines = File.ReadAllLines("data.txt");
    var output = new List<string>();
    foreach (var line in lines)
    {
        var words = line.Split(' ');

        if (words[0] == "inp")
            output.Add($"{words[1]} = buffer[i++];");
        else if (words[0] == "add")
            output.Add($"{words[1]} += {words[2]};");
        else if (words[0] == "mul")
            output.Add($"{words[1]} *= {words[2]};");
        else if (words[0] == "div")
            output.Add($"{words[1]} /= {words[2]};");
        else if (words[0] == "mod")
            output.Add($"{words[1]} = {words[1]} % {words[2]};");
        else if (words[0] == "eql")
            output.Add($"{words[1]} = {words[1]} == {words[2]} ? 1: 0;");
    }

    foreach (var l in output)
    {
        Console.WriteLine(l);
    }
}

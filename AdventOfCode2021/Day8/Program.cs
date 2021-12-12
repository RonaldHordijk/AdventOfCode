var lines = File.ReadAllLines("data.txt");

Int64 sum = 0;

foreach (var line in lines)
{

    var values = line.Split("|", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    var words = line.Replace("|", " ").Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

    var cf = words.FirstOrDefault(w => w.Length == 2)?.ToCharArray().ToList();
    var a = words.FirstOrDefault(w => w.Length == 3)?.ToCharArray().Where(c => !cf.Contains(c)).ToList();
    var bd = words.FirstOrDefault(w => w.Length == 4)?.ToCharArray().Where(c => !cf.Contains(c)).ToList();

    if (cf is null || a is null || bd is null)
        Console.WriteLine("Problem");

    var value = 0;

    foreach (var v in values)
    {
        value *= 10;

        //0
        if (v.Length == 6
            && v.ToCharArray().Count(c => cf.Contains(c)) == 2
            && v.ToCharArray().Count(c => bd.Contains(c)) == 1)
        { }

        //1
        if (v.Length == 2)
        {
            value += 1;
        }

        //2
        if (v.Length == 5
            && v.ToCharArray().Count(c => cf.Contains(c)) == 1
            && v.ToCharArray().Count(c => bd.Contains(c)) == 1)
        {
            value += 2;
        }

        //3
        if (v.Length == 5
            && v.ToCharArray().Count(c => cf.Contains(c)) == 2
            && v.ToCharArray().Count(c => bd.Contains(c)) == 1)
        {
            value += 3;
        }

        //4
        if (v.Length == 4)
        {
            value += 4;
        }

        //5
        if (v.Length == 5
            && v.ToCharArray().Count(c => cf.Contains(c)) == 1
            && v.ToCharArray().Count(c => bd.Contains(c)) == 2)
        {
            value += 5;
        }

        //6
        if (v.Length == 6
            && v.ToCharArray().Count(c => cf.Contains(c)) == 1
            && v.ToCharArray().Count(c => bd.Contains(c)) == 2)
        {
            value += 6;
        }

        //7
        if (v.Length == 3)
        {
            value += 7;
        }

        //8
        if (v.Length == 7)
        {
            value += 8;
        }

        //9
        if (v.Length == 6
            && v.ToCharArray().Count(c => cf.Contains(c)) == 2
            && v.ToCharArray().Count(c => bd.Contains(c)) == 2)
        {
            value += 9;
        }

    }
    Console.WriteLine(value);

    sum += value;


}

Console.WriteLine($"Count of 1478 is {sum}");

var lines = File.ReadAllLines("data.txt");

Int64 cycle = 0;
Int64 register = 1;
Int64 sumForce = 0;
int[] sl = new int[400];

foreach (var line in lines)
{
    cycle++;

    if ((cycle + 20) % 40 == 0 && cycle < 230)
    {
        Console.WriteLine($"cycle {cycle} reg {register} ss = {cycle * register}");
        sumForce += cycle * register;
    }

    if (line.StartsWith("noop"))
    {
        // do nothing
        long linePos = (cycle - 1) % 40;
        sl[cycle - 1] = Math.Abs(linePos - register) < 2 ? 1 : 0;
    }
    else if (line.StartsWith("addx"))
    {
        long linePos = (cycle - 1) % 40;
        sl[cycle - 1] = Math.Abs(linePos - register) < 2 ? 1 : 0;
        linePos = cycle % 40;
        sl[cycle] = Math.Abs(linePos - register) < 2 ? 1 : 0;
        //Dump();

        cycle++;

        if ((cycle + 20) % 40 == 0 && cycle < 230)
        {
            Console.WriteLine($"cycle {cycle} reg {register} ss = {cycle * register}");
            sumForce += cycle * register;
        }
        register += int.Parse(line[5..]);


    }
}

Console.WriteLine($"Tot signal {sumForce}");
Dump();

void Dump()
{
    for (int r = 0; r < 10; r++)
    {
        for (int x = 0; x < 40; x++)
        {
            Console.Write(sl[r * 40 + x] > 0 ? "#" : ".");

        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

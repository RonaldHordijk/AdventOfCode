var lines = File.ReadAllLines("data.txt");

long regA = Convert.ToInt64(lines[0].Split(':')[1]);
long regB = Convert.ToInt64(lines[1].Split(':')[1]);
long regC = Convert.ToInt64(lines[2].Split(':')[1]);

List<uint> program = lines[4].Split(':')[1]
    .Split(',')
    .Select(s => Convert.ToUInt32(s))
    .ToList();

//regC = 9;
//program = [2, 6];
//regA = 10;
//program = [5, 0, 5, 1, 5, 4];
//regA = 2024;
//program = [0, 1, 5, 4, 3, 0];
//regB = 29;
//program = [1, 7];
//regB = 2024;
//regC = 43690;
//program = [4, 0];

bool Check(long A)
{
    regA = A;
    regB = 0;
    regC = 0;

    int outputindex = 0;

    int position = 0;
    while (position < program.Count)
    {
        int opcode = (int)program[position];
        position++;
        long operand = program[position];
        position++;

        long comboOperand = operand;
        if (operand == 4)
            comboOperand = regA;
        else if (operand == 5)
            comboOperand = regB;
        else if (operand == 6)
            comboOperand = regC;

        if (opcode == 0)
        {
            // division A
            regA = (long)(regA / (Math.Pow(2, comboOperand)));
        }
        else if (opcode == 1)
        {
            //xor B 
            regB = regB ^ operand;
        }
        else if (opcode == 2)
        {
            //modulo
            regB = comboOperand % 8;
        }
        else if (opcode == 3)
        {
            //jnz
            if (regA != 0)
                position = (int)operand;
        }
        else if (opcode == 4)
        {
            //xor B C
            regB = regB ^ regC;
        }
        else if (opcode == 5)
        {
            //output
            Console.Write($"{comboOperand % 8},");

            //if (comboOperand % 8 != program[outputindex])
            //    return false;

            //outputindex++;
            //if (outputindex > 7)
            //    Console.WriteLine($"{A} {outputindex}");
        }
        else if (opcode == 6)
        {
            // division B
            regB = (long)(regA / (Math.Pow(2, comboOperand)));
        }
        else if (opcode == 7)
        {
            // division C
            regC = (long)(regA / (Math.Pow(2, comboOperand)));
        }
    }

    return (outputindex == program.Count);
}

List<int> Check2(long A)
{
    regA = A;
    regB = 0;
    regC = 0;

    List<int> output = [];

    while (regA != 0)
    {
        regB = regA % 8;
        regB = regB ^ 7;
        regC = (long)(regA / (Math.Pow(2, regB)));
        regA = regA / 8;
        regB = regB ^ regC;
        regB = regB ^ 7;

        //Console.Write($"{regB % 8},");
        output.Add((int)(regB % 8));

        //if (regB % 8 != program[outputindex])
        //    return false;

        //outputindex++;
        //if (outputindex > 7)
        //    Console.WriteLine($"{A} {outputindex}");
    }

    //return (outputindex == program.Count);
    return output;
}

void Find(long value, int nrDigits)
{
    if (nrDigits > program.Count)
    {
        Console.WriteLine(value);
        return;
    }

    for (long i = -65; i < 66; i++)
    {
        long x = value * 8 + i;
        if (x < 0)
            continue;

        var res = Check2(x);

        if (res.Count < nrDigits)
            continue;

        bool same = true;
        for (int j = 0; j < nrDigits; j++)
        {
            if (program[program.Count - 1 - j] != res[res.Count - 1 - j])
            {
                same = false;
                break;
            }
        }

        if (same)
            Find(x, nrDigits + 1);
    }
}

Find(0, 1);



//258394985014171
//258812670583707 tohigh

//for (uint i = 1; i < 4294967296; i++)
//{
//    Check2(i);
//}

// 2,4,1,7,7,5,0,3,4,0,1,7,5,5,3,0
// 2,4,1,7,7,5,0,3,4,0,1,7,5,5,3,0
//4,6,5,4,4,3,7,5,3,
Console.WriteLine();
Console.WriteLine($"regA {regA}");
Console.WriteLine($"regB {regB}");
Console.WriteLine($"regC {regC}");

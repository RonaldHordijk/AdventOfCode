var lines = File.ReadAllLines("data.txt");

var sum = 0;
var sum2 = 0;

foreach (var line in lines)
{
    var elfs = line.Split(",");
    var elf1 = elfs[0].Split("-");
    var elf2 = elfs[1].Split("-");

    int elf1Start = int.Parse(elf1[0]);
    int elf1End = int.Parse(elf1[1]);
    int elf2Start = int.Parse(elf2[0]);
    int elf2End = int.Parse(elf2[1]);

    if ((elf1Start <= elf2Start && elf1End >= elf2End)
        || (elf2Start <= elf1Start && elf2End >= elf1End))
        sum++;

    if (!((elf1Start > elf2End) || (elf1End < elf2Start)))
        sum2++;
}

Console.WriteLine($"Total overlapping {sum}");
Console.WriteLine($"Total Partial overlapping {sum2}");

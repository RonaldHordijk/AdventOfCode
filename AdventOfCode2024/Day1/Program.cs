var lines = File.ReadAllLines("data.txt");

List<Int64> list1 = [];
List<Int64> list2 = [];

foreach (var line in lines)
{
    var words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    list1.Add(Convert.ToInt64(words[0]));
    list2.Add(Convert.ToInt64(words[1]));
}

list1.Sort();
list2.Sort();

Int64 diffsom = 0;

for (int i = 0; i < list1.Count; i++)
{
    diffsom += Math.Abs(list1[i] - list2[i]);
}

Console.WriteLine($"som {diffsom}");

// part 2

Int64 sum = 0;
for (int i = 0; i < list1.Count; i++)
{
    int count = 0;
    for (int j = 0; j < list2.Count; j++)
    {
        if (list1[i] == list2[j])
        {
            count++;
        }
    }

    sum += count * list1[i];
}

Console.WriteLine($"som 2 {sum}");

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
    class Condition
    {
        public string Name { get; set; }
        public int Min1 { get; set; }
        public int Max1 { get; set; }
        public int Min2 { get; set; }
        public int Max2 { get; set; }
        public int Column { get; set; }
        public List<int> PossibleColumns { get; set; } = new List<int>();
        public int Index { get; set; } = -1;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            var conditions = new List<Condition>();

            var myValues = new List<int>();
            var correctValues = new List<List<int>>();

            int sum = 0;
            int state = 0;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    state++;
                    continue;
                }

                switch (state)
                {
                    case 0:
                    {
                            // input 
                            var match = Regex.Match(line, @"(.*): (\d+)-(\d+) or (\d+)-(\d+)");

                            if (!match.Success)
                                Console.WriteLine("invalid");
                            conditions.Add(new Condition
                            {
                                Name = match.Groups[1].Value,
                                Min1 = int.Parse(match.Groups[2].Value),
                                Max1 = int.Parse(match.Groups[3].Value),
                                Min2 = int.Parse(match.Groups[4].Value),
                                Max2 = int.Parse(match.Groups[5].Value),
                                Column = -1
                            });
                            break;

                    }
                    case 1:
                    {
                            // my ticket;
                            var values = line.Split(",");
                            if (values.Length == 1)
                                continue;

                            myValues = values.Select(v => Int32.Parse(v)).ToList();

                            break;

                    }
                    case 2:
                        {
                            // other tickets;
                            var values = line.Split(",");
                            if (values.Length == 1)
                                continue;

                            var correctSet = true;
                            foreach(var v in values)
                            {
                                int value = Int32.Parse(v);
                                bool valid = false;
                                foreach(var condition in conditions)
                                {
                                    if ((value >= condition.Min1 && value <= condition.Max1) ||
                                            (value >= condition.Min2 && value <= condition.Max2))
                                    {
                                        valid = true;
                                        break;
                                    }
                                }
                                if (!valid)
                                {
                                    sum += value;
                                    correctSet = false;
                                }
                            }

                            if (correctSet)
                            {
                                correctValues.Add(values.Select(v => Int32.Parse(v)).ToList());
                            }

                            break;
                        }
                }
            }

            Console.WriteLine($"Sum invalid is {sum}");

            foreach (var condition in conditions)
            {
                Console.Write($"{condition.Name} : ");

                if (condition.Column >= 0)
                    continue;

                for (int column = 0; column < myValues.Count; column++)
                {
                    var correctSet = true;
                    foreach (var values in correctValues)
                    {
                        int value = values[column];
                        if (!((value >= condition.Min1 && value <= condition.Max1) ||
                                (value >= condition.Min2 && value <= condition.Max2)))
                        {
                            correctSet = false;
                            break;
                        }
                    }
                    if (correctSet)
                    {
                        Console.Write($"{column}, ");
                        condition.PossibleColumns.Add(column);
                    }
                }

                Console.WriteLine();
            }

            int CondI = 0;
            while (true)
            {
                if (CondI < 0)
                {
                    Console.WriteLine($"Failure");
                    break;
                }
                if (CondI >= conditions.Count)
                {
                    Console.WriteLine($"Success");
                    break;
                }

                conditions[CondI].Index++;
                if (conditions[CondI].Index >= conditions[CondI].PossibleColumns.Count)
                {
                    conditions[CondI].Index = -1;
                    CondI--;
                    continue;
                }

                conditions[CondI].Column = conditions[CondI].PossibleColumns[conditions[CondI].Index];

                bool alreadyUsed = false;
                for(int i = 0; i< CondI;i++)
                {
                    if (conditions[i].Column == conditions[CondI].Column)
                        alreadyUsed = true;
                }

                if (alreadyUsed)
                    continue;

                CondI++;
            }

            long prod = 1;
            foreach (var condition in conditions)
            {
                Console.WriteLine($"{condition.Name} = {condition.Column}");
                if (condition.Name.StartsWith("departure"))
                {
                    prod *= myValues[condition.Column];
                }
            }

            Console.WriteLine($"Result = {prod}");


        }
    }
}

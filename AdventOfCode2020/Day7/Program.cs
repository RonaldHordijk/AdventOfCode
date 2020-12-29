using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7
{

    class Bag
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public long Totalbags { get; set; }
        public bool Processed { get; set; }

        public List<Bag> SubBags { get; set; } = new List<Bag>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            var bags = new List<Bag>();

            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"(.+) bags contain no other bags.");

                if (match.Success)
                {
                    bags.Add(new Bag
                    {
                        Name = match.Groups[1].Value
                    });
                    continue;
                }


                //wavy beige bags contain 4 pale violet bags, 5 dim tan bags, 3 pale fuchsia bags, 2 wavy tan bags.
                var parts = line.Split(",");

                match = Regex.Match(parts[0], @"(.+) bags contain (\d+) (.+) bag");
                if (match.Success)
                {
                    bags.Add(new Bag{
                        Name = match.Groups[1].Value,
                    });

                    bags.Last().SubBags.Add(new Bag{
                        Count = Convert.ToInt32(match.Groups[2].Value),
                        Name = match.Groups[3].Value
                    });
                }

                for( int i=1; i< parts.Length;i++)
                {
                    match = Regex.Match(parts[i], @" (\d+) (.+) bag");
                    if (match.Success)
                    {
                        bags.Last().SubBags.Add(new Bag{
                            Count = Convert.ToInt32(match.Groups[1].Value),
                            Name = match.Groups[2].Value
                        });
                    }
                }
            }

            var solution = new List<Bag>();
            solution.Add(new Bag
            {
                Name = "shiny gold"
            });


            for(int i =0; i< solution.Count; i++)
            {
                var seachBag = solution[i].Name;

                foreach (var bag in bags)
                {
                    if (bag.SubBags.Any(b=>b.Name == seachBag))
                    {
                        // already added
                        if (solution.Any(b => b.Name == bag.Name))
                            continue;

                        solution.Add(bag);
                    }
                }
            }


            while (bags.Any(b => !b.Processed))
            {
                for (int i = 0; i < bags.Count; i++)
                {
                    var bag = bags[i];

                    if (bag.Processed)
                        continue;

                    if (bag.SubBags.Any(b=>!b.Processed))
                        continue;

                    bag.Totalbags = 1 + bag.SubBags.Sum(b => b.Totalbags * b.Count);
                    bag.Processed = true;

                    foreach(var bag2 in bags)
                    {
                        for (int j = 0; j < bag2.SubBags.Count; j++)
                        {
                            if (bag2.SubBags[j].Name == bag.Name)
                            {
                                bag2.SubBags[j].Totalbags = bag.Totalbags;
                                bag2.SubBags[j].Processed = bag.Processed;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Nr Bags is {solution.Count -1}");
            Console.WriteLine($"Nr Bags containing {bags.First(b=>b.Name== "shiny gold").Totalbags-1}");

        }
    }
}

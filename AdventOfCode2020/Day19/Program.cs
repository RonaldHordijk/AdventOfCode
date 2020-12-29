using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day19
{
    class Part
    {
        public int Index { get; set; }
        public List<int> Order { get; set; }
        public List<int> OtherOrder { get; set; }
        public List<string> PossibleValues { get; set; } = new List<string>();
        public bool IsCyclic => OtherOrder?.Contains(Index) == true;
    }



    class Program
    {

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            var parts = new Dictionary<int, Part>();

            int mode = 0;
            int count = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    parts.Remove(0);
                    parts.Remove(8);
                    parts.Remove(11);

                    Solve(parts);
                    mode = 1;
                    continue;
                }

                if (mode == 0)
                {
                    var IndexSplit = line.Split(":");
                    int index = Int32.Parse(IndexSplit[0]);

                    string order = IndexSplit[1];
                    string second = "";
                    if (line.Contains("|"))
                    {
                        order = order.Split("|")[0];
                        second = line.Split("|")[1];
                    }

                    if (line.Contains("\""))
                    {
                        parts.Add(index, new Part
                        {
                            Index = index,
                            Order = null,
                            OtherOrder = null,
                            PossibleValues = { order.Substring(2, 1) }
                        });
                    }
                    else
                    {
                        parts.Add(index, new Part
                        {
                            Index = index,
                            Order = order.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(v => Int32.Parse(v)).ToList(),
                            OtherOrder = second.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(v => Int32.Parse(v)).ToList()
                        });
                    }
                }
                else
                {
                    if (Contains(parts, line))
                        count++;

                    //if (parts[0].PossibleValues.Contains(line))
                    //    count++;
                }
            }

            Console.WriteLine($"Number valid {count}");
        }


        private static bool Contains(Dictionary<int, Part> parts, string line)
        {
            //42+ 42+ 31+'
            // 42 42* 31* 31
            var list = new List<(int count, string remainder)> { (0, line) };

            for (int i = 0; i < list.Count; i++)
            {
                string r = list[i].remainder;
                int c = list[i].count;

                list.AddRange(
                    parts[42].PossibleValues
                      .Where(s => r.StartsWith(s))
                      .Select(s => (c + 1, r[s.Length..]))
                    );
            }

            list = list.Where(i => i.count >= 2).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                string r = list[i].remainder;
                int c = list[i].count;

                if (c == 1)
                    continue;

                if (parts[31].PossibleValues.Contains(r))
                    return true;
                
                list.AddRange(
                    parts[31].PossibleValues
                      .Where(s => r.StartsWith(s))
                      .Select(s => (c - 1, r[s.Length..]))
                    );
            }


            return false;
        }

        private static void Solve(Dictionary<int, Part> parts)
        {
            while (parts.Values.Any(p => p.PossibleValues.Count == 0))
            {
                foreach (var p in parts.Values)
                {
                    if (p.PossibleValues.Count > 0)
                        continue;

                    if (p.Order.Any(i => parts[i].PossibleValues.Count == 0))
                        continue;

                    if (p.OtherOrder.Any(i => (i != p.Index) && parts[i].PossibleValues.Count == 0))
                        continue;

                    {
                        var newstrings = new List<string>(parts[p.Order[0]].PossibleValues);
                        foreach (int index in p.Order.Skip(1))
                        {
                            var ts = new List<string>();
                            foreach (var s1 in newstrings)
                            {
                                foreach (var s2 in parts[index].PossibleValues)
                                {
                                    ts.Add(s1 + s2);
                                }
                            }
                            newstrings = ts;
                        }
                        p.PossibleValues = newstrings;
                    }

                    if (p.OtherOrder.Count > 0)
                    {
                        var newstrings = new List<string>(parts[p.OtherOrder[0]].PossibleValues);
                        foreach (int index in p.OtherOrder.Skip(1))
                        {
                            var ts = new List<string>();
                            foreach (var s1 in newstrings)
                            {
                                foreach (var s2 in parts[index].PossibleValues)
                                {
                                    ts.Add(s1 + s2);
                                }
                            }
                            newstrings = ts;
                        }
                        p.PossibleValues.AddRange(newstrings);
                    }
                }
            }
        }
    }
}

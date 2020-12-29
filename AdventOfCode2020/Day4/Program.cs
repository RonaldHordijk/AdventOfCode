using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            int nrValid = 0;

            var keys = new List<string>{"cid"};
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (keys.Count == 8)
                        nrValid++;

                    keys = new List<string> { "cid" };
                }

                foreach (var word in line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                {
                    var key = word.Split(":")[0];
                    var val = word.Split(":")[1];

                    if ((key == "byr") && ((Convert.ToInt32(val) < 1920) || (Convert.ToInt32(val) > 2002)))
                        continue;

                    if ((key == "iyr") && ((Convert.ToInt32(val) < 2010) || (Convert.ToInt32(val) > 2020)))
                        continue; 
                    
                    if ((key == "eyr") && ((Convert.ToInt32(val) < 2020) || (Convert.ToInt32(val) > 2030)))
                        continue;

                    if (key == "hgt")
                    {
                        var post = val[^2..^0];
                        if (post != "in" && post != "cm")
                            continue;

                        var height = Convert.ToInt32(val[0..^2]);

                        if ((post == "cm") && ((height < 150) || (height > 193)))
                            continue;
                        if ((post == "in") && ((height < 59) || (height > 76)))
                            continue;
                    }

                    if (key == "hcl")
                    {
                        if (val.Length != 7 || val[0] != '#')
                            continue;

                        if (val.Skip(1).Any(c=>!"0123456789abcdef".Contains(c)))
                            continue;
                    }

                    if ((key == "ecl") && (val != "amb") && (val != "blu") && (val != "brn") && (val != "gry") && (val != "grn") && (val != "hzl") && (val != "oth"))
                        continue;

                    if ((key == "pid") && ((val.Length != 9) ||  val.Any(c => !"0123456789".Contains(c))))
                        continue;

                    if (!keys.Contains(key))
                        keys.Add(key);
                }
            }

            if (keys.Count == 8)
                nrValid++;

            Console.WriteLine($"Valid = {nrValid}");
        }
    }
}

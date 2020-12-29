using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            var setters = new List<long>();
            var clearers = new List<long>();
            var switchers = new List<long>();

            //long[] mem = new long[100000];

            //foreach (var line in lines)
            //{
            //    if (line.StartsWith("mask"))
            //    {
            //        var mask = line[7..];
            //        setters = new List<long>();
            //        clearers = new List<long>();

            //        long val = 1;
            //        for (int i = 0; i < 36; i++)
            //        {
            //            if (mask[35 - i] == '1')
            //                setters.Add(val);
            //            if (mask[35 - i] == '0')
            //                clearers.Add(val);

            //            val *= 2;
            //        }

            //        continue;
            //    }

            //    var match = Regex.Match(line, @"mem\[(\d+)\] = (\d+)");

            //    if (!match.Success)
            //        Console.WriteLine("invalid");

            //    int Index = int.Parse(match.Groups[1].Value);
            //    long SetVal = Int64.Parse(match.Groups[2].Value);

            //    foreach (var setter in setters)
            //    {
            //        if ((SetVal & setter) == 0)
            //            SetVal += setter;
            //    }

            //    foreach (var clearer in clearers)
            //    {
            //        if ((SetVal & clearer) > 0)
            //            SetVal -= clearer;
            //    }

            //    mem[Index] = SetVal;
            //}

            var mem = new Dictionary<long,long>();

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    var mask = line[7..];
                    setters = new List<long>();
                    clearers = new List<long>();
                    switchers = new List<long>();

                    long val = 1;
                    for (int i = 0; i < 36; i++)
                    {
                        if (mask[35 - i] == '1')
                            setters.Add(val);
                        if (mask[35 - i] == '0')
                            clearers.Add(val);
                        if (mask[35 - i] == 'X')
                            switchers.Add(val);

                        val *= 2;
                    }

                    continue;
                }

                var match = Regex.Match(line, @"mem\[(\d+)\] = (\d+)");

                if (!match.Success)
                    Console.WriteLine("invalid");

                long Index = Int64.Parse(match.Groups[1].Value);
                long SetVal = Int64.Parse(match.Groups[2].Value);

                foreach (var setter in setters)
                {
                    if ((Index & setter) == 0)
                        Index += setter;
                }

                foreach (var switcher in switchers)
                {
                    if ((Index & switcher) > 0)
                        Index -= switcher;
                }

                long end = 1 << switchers.Count;

                for(long i = 0; i < end; i++)
                {
                    long newIndex = Index;
                    for (int j = 0; j < switchers.Count; j++)
                    {
                        if ((i & (1L << j)) > 0)
                            newIndex += switchers[j];
                    }

                    mem[newIndex] = SetVal;
                }
            }

            Console.WriteLine($"Sum is {mem.Values.Sum()}");
        }
    }
}

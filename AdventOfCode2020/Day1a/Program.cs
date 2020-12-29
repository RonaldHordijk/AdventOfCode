using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Day1a
{
    class Program
    {
        static void Main(string[] args)
        {

            var lines = File.ReadAllLines("data1a.txt");
            var values = lines.Select(l => Int32.Parse(l)).ToList();

            //for(int i = 0; i< values.Count-1; i++)
            //{
            //    for (int j = i+1; j < values.Count; j++)
            //    {
            //        if (values[i] + values[j] == 2020)
            //        {
            //            Console.WriteLine($"{values[i]} and {values[j]} prod {values[i] * values[j]}");
            //        }
            //    }
            //}


            for (int i = 0; i < values.Count - 2; i++)
            {
                for (int j = i + 1; j < values.Count - 1; j++)
                {
                    for (int k = j + 1; k < values.Count; k++)
                    {
                        if (values[i] + values[j] + values[k] == 2020)
                        {
                            Console.WriteLine($"{values[i]} and {values[j]} and {values[k]} prod {values[i] * values[j] * values[k]}");
                        }
                    }
                }

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            var line = File.ReadAllLines("data.txt")[0];
            int offset = 0;

            List<string> layers = new List<string>();
            while (offset < line.Length)
            {
                layers.Add(line.Substring(offset, 25 * 6));
                offset += 25 * 6;
            }

            int minZero = Int32.MaxValue;
            string minLayer = "";
            foreach (var layer in layers)
            {
                int cnt = layer.Count(c => c == '0');
                if (cnt < minZero)
                {
                    minZero = cnt;
                    minLayer = layer;
                }
            }

            int val = minLayer.Count(c => c == '1') * minLayer.Count(c => c == '2');
            Console.WriteLine($"Max is {val}");

            StringBuilder sb = new StringBuilder(layers[0]);
            foreach (var layer in layers)
            {
                for (int i = 0; i < layer.Length; i++)
                {
                    if (sb[i] == '2')
                        sb[i] = layer[i];
                }
            }

            var result = sb.ToString();
            result = result.Replace("2", " ");
            result = result.Replace("0", " ");
            result = result.Replace("1", "X");
            Console.WriteLine(result.Substring(0, 25));
            Console.WriteLine(result.Substring(25, 25));
            Console.WriteLine(result.Substring(50, 25));
            Console.WriteLine(result.Substring(75, 25));
            Console.WriteLine(result.Substring(100, 25));
            Console.WriteLine(result.Substring(125, 25));

        }
    }
}

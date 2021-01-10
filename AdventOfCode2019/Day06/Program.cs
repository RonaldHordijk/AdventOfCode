using System;
using System.Collections.Generic;
using System.IO;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            var data = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                var items = line.Split(')');
                data.Add(items[1], items[0]);
            }

            int sum = 0;
            foreach (string d in data.Keys)
            {
                string t1 = d;

                int count = 0;
                while (data.ContainsKey(t1))
                {
                    t1 = data[t1];
                    count++;
                }

                sum += count;
            }

            Console.WriteLine($"the number of orbits is {sum}");

            var You = new List<string>();
            var San = new List<string>();

            string t = "YOU";
            while (data.ContainsKey(t))
            {
                t = data[t];
                You.Add(t);
            }

            t = "SAN";
            while (data.ContainsKey(t))
            {
                t = data[t];
                San.Add(t);
            }

            int nrMoves = You.Count + San.Count;
            for (int i = 0; i < You.Count; i++)
            {
                if (You[You.Count - 1 - i] == San[San.Count - 1 - i])
                {
                    nrMoves -= 2;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine($"the number of moves is {nrMoves}");

        }
    }
}

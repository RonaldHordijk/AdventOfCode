using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day22
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            var deck1 = new List<int>();
            var deck2 = new List<int>();

            int mode = 0;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    mode++;
                    continue;
                }

                if (line.StartsWith("Player"))
                    continue;

                if (mode == 0)
                {
                    deck1.Add(Int32.Parse(line));
                }
                else
                {
                    deck2.Add(Int32.Parse(line));
                }
            }

            var history = new List<List<int>>();

            int round = 0;
            while (deck1.Count != 0 && deck2.Count != 0)
            {
                //Console.WriteLine($"p1{String.Join(null, deck1)}p2{String.Join(null, deck2)}");
                round++;

                if (ExistsAndAdd(history, deck1, deck2))
                    break;

                bool needSubGame = (deck1.Count > deck1[0]) && (deck2.Count > deck2[0]);
                bool winner1 = needSubGame ? SubGame(deck1, deck2) : (deck1[0] > deck2[0]);

                if (winner1)
                {
                    deck1.Add(deck1[0]);
                    deck1.Add(deck2[0]);
                }
                else
                {
                    deck2.Add(deck2[0]);
                    deck2.Add(deck1[0]);
                }

                deck1.RemoveAt(0);
                deck2.RemoveAt(0);
            }

            var deck = (deck1.Count > 0) ? deck1 : deck2;

            long sum = 0;
            for (int i = 0; i < deck.Count; i++)
            {
                sum += deck[i] * (deck.Count - i);
            }

            Console.WriteLine($"Nr round {round}");
            Console.WriteLine($"sum id  {sum}");
        }

        private static bool SubGame(List<int> odeck1, List<int> odeck2)
        {
            var history = new List<List<int>>();

            var deck1 = new List<int>();
            var deck2 = new List<int>();
            for (int i = 1; i <= odeck1[0]; i++)
                deck1.Add(odeck1[i]);
            for (int i = 1; i <= odeck2[0]; i++)
                deck2.Add(odeck2[i]);

            while (deck1.Count != 0 && deck2.Count != 0)
            {
                //string s = $"p1{String.Join(null, deck1)}p2{String.Join(null, deck2)}";
                //Console.WriteLine(s);

                if (ExistsAndAdd(history, deck1, deck2))
                    return true;

                bool needSubGame = (deck1.Count > deck1[0]) && (deck2.Count > deck2[0]);
                bool winner1 = needSubGame ? SubGame(deck1, deck2) : (deck1[0] > deck2[0]);

                if (winner1)
                {
                    deck1.Add(deck1.First());
                    deck1.Add(deck2.First());
                }
                else
                {
                    deck2.Add(deck2.First());
                    deck2.Add(deck1.First());
                }

                deck1.RemoveAt(0);
                deck2.RemoveAt(0);
            }

            return (deck1.Count > 0) ? true : false;
        }

        private static bool ExistsAndAdd(List<List<int>> history, List<int> deck1, List<int> deck2)
        {
            for (int h = 0; h < history.Count; h++)
            {
                bool exists = true;
                for (int i = 0; i < deck1.Count; i++)
                {
                    if (deck1[i] != history[h][i])
                        exists = false;
                }

                for (int i = 0; i < deck2.Count; i++)
                {
                    if (deck2[i] != history[h][i + deck1.Count+1])
                        exists = false;
                }

                if (exists)
                    return true;
            }

            var newItem = new List<int>(deck1);
            newItem.Add(-1);
            newItem.AddRange(deck2);
            history.Add(newItem);

            return false;
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> Sequence = new List<int>(30000000);
            //Sequence.Add(0);
            //Sequence.Add(3);
            //Sequence.Add(6);

            Sequence.Add(0);
            Sequence.Add(13);
            Sequence.Add(1);
            Sequence.Add(8);
            Sequence.Add(6);
            Sequence.Add(15);

            var mapLastUsed = new int[30000000];
            //for (int i = 0; i < 30000000; i++)
            //{
            //    mapLastUsed[i] = -1;
            //}
            mapLastUsed[0] = 1;
            mapLastUsed[13] = 2;
            mapLastUsed[1] = 3;
            mapLastUsed[8] = 4;
            mapLastUsed[6] = 5;
            //mapLastUsed[15] = 6;
            //mapLastUsed[6] = 2;


            while (Sequence.Count < 30000000)
            {
                var lastnum = Sequence.Last();
                int index = mapLastUsed[lastnum];
                mapLastUsed[lastnum] = Sequence.Count;

                if (index == 0)
                {                   
                    Sequence.Add(0);
                } else
                {
                    int cnt = Sequence.Count - index;
                    Sequence.Add(cnt);
                }

            }

            Console.WriteLine($"LastNumber is {Sequence.Last()}");




        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23
{
    class Node
    {
        public Node Next;
        public int value;

        public Node()
        {

        }
        public Node(int v)
        {
            value = v;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //int[] cups = new int[12000000];
            //cups[0] = 3;
            //cups[1] = 8;
            //cups[2] = 9;
            //cups[3] = 1;
            //cups[4] = 2;
            //cups[5] = 5;
            //cups[6] = 4;
            //cups[7] = 6;
            //cups[8] = 7;
            //cups[0] = 7;
            //cups[1] = 8;
            //cups[2] = 9;
            //cups[3] = 4;
            //cups[4] = 6;
            //cups[5] = 5;
            //cups[6] = 1;
            //cups[7] = 2;
            //cups[8] = 3;

            ////var cups = new List<int> { 3, 8, 9, 1, 2, 5, 4, 6, 7 };
            ////var cups = new List<int> { 7, 8, 9, 4, 6, 5, 1, 2, 3 };
            //for (int i = 10; i <= 1000000; i++)
            //{
            //    cups[i-1]= i;
            //}

            //for (int i = 0; i < 10000000; i++)
            //{
            //    //Console.WriteLine($"{i} {string.Join(',', cups.Select(v => v.ToString()))}");

            //    var sub = new List<int> { cups[1], cups[2], cups[3] };
            //    cups.RemoveRange(1, 3);

            //    int index = 0;
            //    for (int f = cups[0] -1; f>cups[0] - 5;f--)
            //    {
            //        index = cups.IndexOf(f >0 ? f: f + 999999);
            //        if (index > 0)
            //            break;
            //    }

            //    cups.InsertRange(index+1, sub);

            //    cups.Add(cups[0]);
            //    cups.RemoveAt(0);               
            //}

            //Console.WriteLine($"end {string.Join(',', cups.Select(v => v.ToString()))}");
            //while (cups[0] != 1)
            //{
            //    cups.Add(cups[0]);
            //    cups.RemoveAt(0);
            //}
            ////Console.WriteLine($"sorted {string.Join(null, cups.Select(v => v.ToString()))}");
            //Console.WriteLine($"sorted {string.Join(" ", cups.Take(4).Select(v => v.ToString()))}");


            //int offset = 0;
            //int size = 1000000;
            //for (int i = 0; i < 10000000; i++)
            //{
            //    if (i % 10000 == 0)
            //    {
            //        if (i % 100000 == 0)
            //            Console.Write('#');
            //        else
            //            Console.Write('*');
            //    }
            //    //Console.WriteLine($"{i} {string.Join(',', cups.Skip(offset).Take(size).Select(v => v.ToString()))}");

            //    int b1 = cups[offset + 1];
            //    int b2 = cups[offset + 2];
            //    int b3 = cups[offset + 3];
            //    //var sub = new List<int> { cups[offset + 1], cups[offset + 2], cups[offset + 3] };
            //    //cups.RemoveRange(offset + 1, 3);

            //    int index = 0;
            //    for (int f = cups[offset] - 1; f > cups[offset] - 5; f--)
            //    {
            //        int s = f > 0 ? f : f + size;
            //        if (s == b1 || s == b2 || s == b3)
            //            continue;

            //        index = Array.IndexOf<int>(cups, s, offset + 4);
            //        if (index > 0)
            //            break;
            //    }

            //    //cups.InsertRange(index + 1, sub);
            //    //for (int r = offset + 1; r < index - 2; r++)
            //    //{
            //    //    cups[r] = cups[r + 3];
            //    //}
            //    Array.Copy(cups, offset + 4, cups, offset + 1, index - offset - 3);

            //    cups[index - 2] = b1;
            //    cups[index - 1] = b2;
            //    cups[index] = b3;

            //    cups[offset + size] =cups[offset];
            //    //cups.RemoveAt(0);
            //    offset++;
            //}
            //while (cups[offset] != 1)
            //{
            //    cups[offset + size] = cups[offset];
            //    offset++;
            //}

            int size = 1000000;

            var root = new Node(3);
            root.Next = root;
            var last = root;
            foreach (int v in new List<int> { 8, 9, 1, 2, 5, 4, 6, 7 })
            {
                var newNode = new Node(v);
                newNode.Next = root;
                last.Next = newNode;
                last = newNode;
            }
            for (int i = 10; i <= 1000000; i++)
            {
                var newNode = new Node(i);
                newNode.Next = root;
                last.Next = newNode;
                last = newNode;
            }

            for (int i = 0; i < 10000000; i++)
            {
                //Display(root);
                if (i % 10000 == 0)
                {
                    if (i % 100000 == 0)
                        Console.Write('#');
                    else
                        Console.Write('*');
                }
                var w = root.Next;
                int b1 = w.value;
                w = w.Next;
                int b2 = w.value;
                w = w.Next;
                int b3 = w.value;
                w = w.Next;
                root.Next = w;

                for (int f = root.value - 1; f > root.value - 5; f--)
                {
                    int s = f > 0 ? f : f + size;
                    if (s == b1 || s == b2 || s == b3)
                        continue;

                    while (w.value != s)
                    {
                        w = w.Next;
                    }

                    break;
                }

                var n = new Node
                {
                    value = b1,
                    Next = new Node
                    {
                        value = b2,
                        Next = new Node
                        {
                            value = b3,
                            Next = w.Next
                        }
                    }
                };
                w.Next = n;

                root = root.Next;
            }
            //Display(root);

            //Console.WriteLine($"end {string.Join(',', cups.Skip(offset).Take(size).Select(v => v.ToString()))}");
            //Console.WriteLine($"sorted {string.Join(" ", cups.Skip(offset - 1).Take(4).Select(v => v.ToString()))}");
            Console.WriteLine($"{root.value}");
            Console.WriteLine($"{root.Next.value}");
            Console.WriteLine($"{root.Next.Next.value}");
        }

        private static void Display(Node root)
        {
            Console.Write(root.value + " ");
            var w = root;
            while (w.Next != root)
            {
                w = w.Next;
                Console.Write(w.value + " ");
            }
            Console.WriteLine();
        }
    }
}

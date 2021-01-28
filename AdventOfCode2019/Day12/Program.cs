using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day12
{
    class Moon
    {
        public int[] pos = { 0, 0, 0 };
        public int[] vel = { 0, 0, 0 };
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            var moons = new List<Moon>();

            foreach (var line in lines)
            {
                var m = Regex.Match(line, ".*=(.+),.*=(.+),.*=(.+)>");
                if (m.Success)
                {
                    var newMoon = new Moon();
                    newMoon.pos[0] = Int32.Parse(m.Groups[1].Value);
                    newMoon.pos[1] = Int32.Parse(m.Groups[2].Value);
                    newMoon.pos[2] = Int32.Parse(m.Groups[3].Value);
                    moons.Add(newMoon);
                }
            }

            Display(moons);
            //for (int i = 0; i < 1000; i++)
            //{
            //    CalculateVelocities(moons);
            //    MoveMoons(moons);
            //}
            //Display(moons);

            //var totalEnergy = CalculateTotalEnergy(moons);
            //Console.WriteLine($"Total energy is {totalEnergy}");

            var l1 = Loop(moons[0].pos[0], moons[1].pos[0], moons[2].pos[0], moons[3].pos[0]);
            Console.WriteLine($"l1 {l1}");
            var l2 = Loop(moons[0].pos[1], moons[1].pos[1], moons[2].pos[1], moons[3].pos[1]);
            Console.WriteLine($"l2 {l2}");
            var l3 = Loop(moons[0].pos[2], moons[1].pos[2], moons[2].pos[2], moons[3].pos[2]);
            Console.WriteLine($"l3 {l3}");


        }

        private static long Loop(int p1, int p2, int p3, int p4)
        {
            int v1 = 0;
            int v2 = 0;
            int v3 = 0;
            int v4 = 0;

            long cnt = 0;
            do
            {
                if (p1 < p2) { v1++; v2--; } else if (p2 < p1) { v2++; v1--; }
                if (p1 < p3) { v1++; v3--; } else if (p3 < p1) { v3++; v1--; }
                if (p1 < p4) { v1++; v4--; } else if (p4 < p1) { v4++; v1--; }
                if (p2 < p3) { v2++; v3--; } else if (p3 < p2) { v3++; v2--; }
                if (p2 < p4) { v2++; v4--; } else if (p4 < p2) { v4++; v2--; }
                if (p3 < p4) { v3++; v4--; } else if (p4 < p3) { v4++; v3--; }

                p1 += v1;
                p2 += v2;
                p3 += v3;
                p4 += v4;

                cnt++;

            } while (!(v1 == 0 && v2 == 0 && v3 == 0 && v4 == 0));

            return cnt;
        }

        private static long CalculateTotalEnergy(List<Moon> moons)
        {
            long sum = 0;

            foreach (var moon in moons)
            {
                sum += (Math.Abs(moon.pos[0]) + Math.Abs(moon.pos[1]) + Math.Abs(moon.pos[2])) * (Math.Abs(moon.vel[0]) + Math.Abs(moon.vel[1]) + Math.Abs(moon.vel[2]));
            }

            return sum;
        }

        private static void MoveMoons(List<Moon> moons)
        {
            foreach (var moon in moons)
            {
                moon.pos[0] += moon.vel[0];
                moon.pos[1] += moon.vel[1];
                moon.pos[2] += moon.vel[2];
            }
        }

        private static void CalculateVelocities(List<Moon> moons)
        {
            for (int i = 0; i < moons.Count - 1; i++)
            {
                for (int j = i + 1; j < moons.Count; j++)
                {
                    if (moons[i].pos[0] != moons[j].pos[0])
                    {
                        if (moons[i].pos[0] > moons[j].pos[0])
                        {
                            moons[i].vel[0]--;
                            moons[j].vel[0]++;
                        }
                        else
                        {
                            moons[i].vel[0]++;
                            moons[j].vel[0]--;
                        }
                    }
                    if (moons[i].pos[1] != moons[j].pos[1])
                    {
                        if (moons[i].pos[1] > moons[j].pos[1])
                        {
                            moons[i].vel[1]--;
                            moons[j].vel[1]++;
                        }
                        else
                        {
                            moons[i].vel[1]++;
                            moons[j].vel[1]--;
                        }
                    }
                    if (moons[i].pos[2] != moons[j].pos[2])
                    {
                        if (moons[i].pos[2] > moons[j].pos[2])
                        {
                            moons[i].vel[2]--;
                            moons[j].vel[2]++;
                        }
                        else
                        {
                            moons[i].vel[2]++;
                            moons[j].vel[2]--;
                        }
                    }
                }
            }
        }

        private static void Display(List<Moon> moons)
        {
            Console.WriteLine();

            foreach (var moon in moons)
            {
                Console.WriteLine($"pos <{moon.pos[0],3},{moon.pos[1],3},{moon.pos[2],3}> vel <{moon.vel[0],3},{moon.vel[1],3},{moon.vel[2],3}>");
            }
        }
    }
}

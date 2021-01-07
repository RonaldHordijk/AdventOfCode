using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Day03
{
    public class Line
    {
        public bool Vertical;
        public Point P1;
        public Point P2;
        public int Length;
        public bool Reversed;

        public Line(Point p1, Point p2)
        {
            Vertical = p1.X == p2.X;
            if (Vertical)
            {
                P1 = new Point(p1.X, Math.Min(p1.Y, p2.Y));
                P2 = new Point(p1.X, Math.Max(p1.Y, p2.Y));
                Reversed = p2.Y < p1.Y;
                Length = P2.Y - P1.Y;
            }
            else
            {
                P1 = new Point(Math.Min(p1.X, p2.X), p1.Y);
                P2 = new Point(Math.Max(p1.X, p2.X), p1.Y);
                Reversed = p2.X < p1.X;
                Length = P2.X - P1.X;
            }
        }
    }

    internal static class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            var path1 = MakePath(lines[0].Split(','));
            var path2 = MakePath(lines[1].Split(','));

            int minDist = Int32.MaxValue;
            int minLength = Int32.MaxValue;

            int l1 = 0;

            foreach (var line1 in path1)
            {
                int l2 = 0;
                foreach (var line2 in path2)
                {
                    if (DoCross(line1, line2))
                    {
                        int dist = Distance(line1, line2);
                        if (dist < minDist)
                            minDist = dist;

                        int lineDist = l1 + l2 + LineDistance(line1, line2);
                        if (lineDist < minLength)
                            minLength = lineDist;
                    }

                    l2 += line2.Length;
                }
                l1 += line1.Length;
            }

            Console.WriteLine($"Min distance is {minDist}");
            Console.WriteLine($"Min length is {minLength}");
        }

        private static int LineDistance(Line line1, Line line2)
        {
            int d = 0;

            if (line1.Vertical)
            {
                if (line1.Reversed)
                    d += Math.Abs(line1.P2.Y - line2.P1.Y);
                else
                    d += Math.Abs(line1.P1.Y - line2.P1.Y);

                if (line2.Reversed)
                    d += Math.Abs(line2.P2.X - line1.P1.X);
                else
                    d += Math.Abs(line2.P1.X - line1.P1.X);
            }
            else
            {
                if (line1.Reversed)
                    d += Math.Abs(line1.P2.X - line2.P1.X);
                else
                    d += Math.Abs(line1.P1.X - line2.P1.X);

                if (line2.Reversed)
                    d += Math.Abs(line2.P2.Y - line1.P1.Y);
                else
                    d += Math.Abs(line2.P1.Y - line1.P1.Y);

            }

            return d;
        }

        private static bool DoCross(Line line1, Line line2)
        {
            if (line1.Vertical == line2.Vertical)
                return false;

            if (line1.Vertical)
            {
                return ((line1.P1.X > line2.P1.X)
                         && (line1.P1.X < line2.P2.X))
                       && ((line2.P1.Y > line1.P1.Y)
                         && (line2.P1.Y < line1.P2.Y));
            }

            return ((line1.P1.Y > line2.P1.Y)
                     && (line1.P1.Y < line2.P2.Y))
                   && ((line2.P1.X > line1.P1.X)
                     && (line2.P1.X < line1.P2.X));
        }

        private static int Distance(Line line1, Line line2)
        {
            if (line1.Vertical)
            {
                return Math.Abs(line1.P1.X) + Math.Abs(line2.P1.Y);
            }

            return Math.Abs(line1.P1.Y) + Math.Abs(line2.P1.X);
        }

        private static List<Line> MakePath(string[] items)
        {
            var result = new List<Line>();
            var p1 = new Point(0, 0);
            var p2 = new Point(0, 0);
            foreach (var item in items)
            {

                int l = Int32.Parse(item[1..]);

                switch (item[0])
                {
                    case 'U':
                        p2.Y = p1.Y - l;
                        break;
                    case 'D':
                        p2.Y = p1.Y + l;
                        break;
                    case 'R':
                        p2.X = p1.X + l;
                        break;
                    case 'L':
                        p2.X = p1.X - l;
                        break;
                }


                result.Add(new Line(p1, p2));

                p1.X = p2.X;
                p1.Y = p2.Y;
            }

            return result;
        }
    }
}

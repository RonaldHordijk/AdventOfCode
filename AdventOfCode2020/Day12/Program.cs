using System;
using System.Drawing;
using System.IO;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            var pos = new Point(0, 0);
            var dir = new Point(0, 1);
            var waypoint = new Point(1, 10);

            foreach (var line in lines)
            {
                int d = Int32.Parse(line[1..]);

                switch (line[0])
                {
                    case 'N':
                        waypoint += new Size(d, 0);
                        break;
                    case 'E':
                        waypoint += new Size(0, d);
                        break;
                    case 'S':
                        waypoint += new Size(-d, 0);
                        break;
                    case 'W':
                        waypoint += new Size(0, -d);
                        break;
                    case 'F':
                        pos += new Size(d * waypoint.X, d * waypoint.Y);
                        break;
                    case 'L':
                        if (d == 90)
                        {
                            waypoint = RotateRight(waypoint);
                            waypoint = new Point(-waypoint.X, -waypoint.Y);
                        }
                        else if (d == 180)
                        {
                            waypoint = new Point(-waypoint.X, -waypoint.Y);
                        }
                        else if (d == 270)
                        {
                            waypoint = RotateRight(waypoint);
                        }
                        break;
                    case 'R':
                        if (d == 90)
                        {
                            waypoint = RotateRight(waypoint);
                        }
                        else if (d == 180)
                        {
                            waypoint = new Point(-waypoint.X, -waypoint.Y);
                        }
                        else if (d == 270)
                        {
                            waypoint = RotateRight(waypoint);
                            waypoint = new Point(-waypoint.X, -waypoint.Y);
                        }
                        break;
                }
            }

            //foreach (var line in lines)
            //{
            //    int d = Int32.Parse(line[1..]);

            //    switch (line[0])
            //    {
            //        case 'N':
            //            pos += new Size(d, 0);
            //            break;
            //        case 'E':
            //            pos += new Size(0, d);
            //            break;
            //        case 'S':
            //            pos += new Size(-d, 0);
            //            break;
            //        case 'W':
            //            pos += new Size(0, -d);
            //            break;
            //        case 'F':
            //            pos += new Size(d * dir.X, d * dir.Y);
            //            break;
            //        case 'L':
            //            if (d == 90)
            //            {
            //                dir = RotateRight(dir);
            //                dir = new Point(-dir.X, -dir.Y);
            //            }
            //            else if (d == 180)
            //            {
            //                dir = new Point(-dir.X, -dir.Y);
            //            } else if (d == 270)
            //            {
            //                dir = RotateRight(dir);
            //            }
            //            break;
            //        case 'R':
            //            if (d == 90)
            //            {
            //                dir = RotateRight(dir);
            //            }
            //            else if (d == 180)
            //            {
            //                dir = new Point(-dir.X, -dir.Y);
            //            }
            //            else if (d == 270)
            //            {
            //                dir = RotateRight(dir);
            //                dir = new Point(-dir.X, -dir.Y);
            //            }
            //            break;
            //    }
            //}

            Console.WriteLine($"Manhattan distance is {Math.Abs(pos.X) + Math.Abs(pos.Y)}");
        }

        private static Point RotateRight(Point dir)
        {
            return new Point(-dir.Y, dir.X);
        }
    }
}

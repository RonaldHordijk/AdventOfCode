using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            

            for (int i = 0; i < lines.Length; i++)
            {

                if (lines[i][0..3] == "acc")
                    continue;

                var bkuline = lines[i];
                if (lines[i][0..3] == "nop")
                {
                    lines[i] = lines[i].Replace("nop", "jmp");
                } else
                {
                    lines[i] = lines[i].Replace("jmp", "nop");
                }

                long acc = 0;
                int currentLine = 0;
                bool[] used = new bool[lines.Length];
                bool error = false;

                while (true)
                {

                    if (currentLine > lines.Length)
                    {
                        error = true;
                        break;
                    }

                    if (currentLine == lines.Length)
                    {
                        error = false;
                        break;
                    }

                    if (used[currentLine])
                    {
                        error = true;
                        break;
                    }

                    used[currentLine] = true;

                    var line = lines[currentLine];
                    if (line[0..3] == "nop")
                    {
                        currentLine++;
                    }
                    else if (line[0..3] == "acc")
                    {
                        currentLine++;
                        acc += Int32.Parse(line[4..]);
                    }
                    else if (line[0..3] == "jmp")
                    {
                        currentLine += Int32.Parse(line[4..]);
                    }
                }

                if (!error)
                    Console.WriteLine($"Accumulator is {acc}");

                lines[i] = bkuline;
            }

        }
    }
}

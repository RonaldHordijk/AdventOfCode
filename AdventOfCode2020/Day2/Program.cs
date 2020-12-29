using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2
{
    class Password
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public char item { get; set; }
        public string value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            int correct = 0;

            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"(\d+)-(\d+) (.): (.+)");

                if (!match.Success)
                    Console.WriteLine("invalid");

                int MinValue = int.Parse(match.Groups[1].Value);
                int MaxValue = int.Parse(match.Groups[2].Value);
                char key = match.Groups[3].Value[0];
                string password = match.Groups[4].Value;

                // first rule
                //var count = password.Where(c => c == key).Count();
                //if (count >= MinValue && count <= MaxValue)
                //    correct++;

                if ((password[MinValue -1] == key) != (password[MaxValue -1] == key))
                    correct++;
                //2-8 t: pncmjxlvckfbtrjh
            }

            Console.WriteLine($"Correct {correct}");
        }
    }
}

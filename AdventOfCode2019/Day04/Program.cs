using System;
using System.Linq;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;

            for (int i = 367479; i <= 893698; i++)
            {
                var digits = i.ToString().Select(c => (int)(c - '0')).ToArray();
                bool valid = true;
                bool repeat = false;
                for (int j = 0; j < 5; j++)
                {
                    if (digits[j] > digits[j + 1])
                    {
                        valid = false;
                        break;
                    }
                    else if (digits[j] == digits[j + 1])
                    {
                        if ((j == 0 || digits[j - 1] != digits[j]) &&
                            (j == 4 || digits[j] != digits[j + 2]))
                            repeat = true;
                    }
                }

                if (valid && repeat)
                    count++;
            }

            Console.WriteLine($"Number of passwords are {count}");
        }
    }
}

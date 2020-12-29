using System;

namespace Day25
{
    class Program
    {
        static void Main(string[] args)
        {
            //long cardPublicKey = 5764801;
            //long doorPublicKey = 17807724;
            long cardPublicKey = 6929599;
            long doorPublicKey = 2448427;

            long subjectNumber = 7;

            long value = 1;
            long loop = 0;
            while (value != cardPublicKey)
            {
                value *= subjectNumber;
                value %= 20201227L;
                loop++;
            }
            long loopCard = loop;
            Console.WriteLine($"loop card = {loop}");

            value = 1;
            loop = 0;
            while (value != doorPublicKey)
            {
                value *= subjectNumber;
                value %= 20201227L;
                loop++;
            }
            long loopDoor = loop;
            Console.WriteLine($"loop door = {loop}");

            value = 1;
            for (loop = 0; loop < loopCard; loop++)
            {
                value *= doorPublicKey;
                value %= 20201227L;
                //Console.WriteLine($"{value}");
            }

            Console.WriteLine($"SecretKey card = {value}");

            value = 1;
            for (loop = 0; loop < loopDoor; loop++)
            {
                value *= cardPublicKey;
                value %= 20201227L;
                //Console.WriteLine($"{value}");
            }

            Console.WriteLine($"SecretKey door = {value}");

        }
    }
}

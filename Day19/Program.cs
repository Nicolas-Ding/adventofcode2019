using System;
using System.Collections.Generic;
using IntCodeUtils;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");
            string input = file.ReadLine();
            long totalSurface = 0;
            var map = new Dictionary<(int, int), char>();

            // minX is the first '#' value on the previous line
            int minX = 0;
            for (int j = 20; true; j++)
            {
                Console.WriteLine($"Checking row {j}");
                int newMinX = -1;
                for (int i = minX; true; i++)
                {
                    long result = GetResultForPosition(input, i, j);
                    long resultRight = GetResultForPosition(input, i + 99, j);
                    if (result == 1 && newMinX == -1)
                    {
                        newMinX = i;
                    }
                    if (resultRight == 0 && newMinX >= 0)
                    {
                        break;
                    }
                    if (result == 1  && resultRight == 1 && GetResultForPosition(input, i, j + 99) == 1)
                    {
                        Console.WriteLine($"Found working point {i}, {j}");
                        return;
                    }
                }

                minX = newMinX;
            }


            Console.WriteLine(totalSurface);
        }

        private static long GetResultForPosition(string input, int i, int j)
        {
            IntCodeReader reader = new IntCodeReader(input);
            int callNumber = 0;
            Func<long> method = () => callNumber++ == 0 ? i : j;
            long result = reader.RunIntCode(method);
            return result;
        }
    }
}

using System;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            int correct = 0;
            for (int i = 246540; i <= 787419; i++)
            {
                correct += IsValid(i) ? 1 : 0;
            }
            Console.WriteLine(correct);

            //Console.WriteLine(IsValid(112444));
        }

        public static bool IsValid(int s)
        {
            int lastDigit = 10;
            bool hasAdjacentDuplicates = false;
            int minCountOfAdjacent = 10;
            int countOfAdjacent = 0;
            while (s > 0)
            {
                int digit = s % 10;
                if (digit == lastDigit)
                {
                    hasAdjacentDuplicates = true;
                    countOfAdjacent++;
                }
                else
                {
                    if (countOfAdjacent > 0 && countOfAdjacent < minCountOfAdjacent)
                    {
                        minCountOfAdjacent = countOfAdjacent;
                    }
                    countOfAdjacent = 0;
                }
                // We are breaking the increasing rule
                if (digit > lastDigit)
                {
                    return false;
                }
                lastDigit = digit;
                s = s / 10;
            }
            if (countOfAdjacent > 0 && countOfAdjacent < minCountOfAdjacent)
            {
                minCountOfAdjacent = countOfAdjacent;
            }
            //Console.WriteLine(minCountOfAdjacent);
            return hasAdjacentDuplicates && minCountOfAdjacent == 1;
        }
    }
}

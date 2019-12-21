using IntCodeUtils;
using System;

namespace Day21
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");
            string input = file.ReadLine();

            IntCodeReader reader = new IntCodeReader(input);

            // Jump if we have to jump and there is ground in 4 steps
            string instructions = "NOT A J;";
            instructions += "NOT B T;";
            instructions += "OR T J;";
            instructions += "NOT C T;";
            instructions += "OR T J;";
            instructions += "AND D J;";
            // But don't jump if once arrived there is a gap just after and nowhere to land
            // Jump if E or H is true (there is ground)
            instructions += "NOT E T;";
            instructions += "NOT T T;";
            instructions += "OR H T;";
            instructions += "AND T J;";
            instructions += "RUN;";

            int a = 0;

            Func<long> getInstructions = () =>
            {
                char c = instructions[a];
                a++;
                //Console.WriteLine($"asking for instructions, giving {c} : {(int) c}");
                if (c == ';')
                {
                    return 10;
                }
                return (int)c;
            };
            long output = 0;
            while (output != Int32.MinValue)
            {
                output = reader.RunIntCode(getInstructions);
                Console.WriteLine($"{(char)output} : {output}");
                //Console.Write($"{(char)output}");
            }
        }
    }
}

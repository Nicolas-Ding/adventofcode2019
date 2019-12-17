using System;
using System.Collections.Generic;
using IntCodeUtils;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");
            string input = file.ReadLine();

            IntCodeReader reader = new IntCodeReader(input);

            Dictionary<(int, int), char> map = new Dictionary<(int, int), char>();

            long output = 0;
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;

            while (output != Int32.MinValue)
            {
                output = reader.RunIntCode(); 
                switch (output)
                {
                    case 35:  // #
                    case 60:  // <
                    case 62:  // >
                    case 94:  // ^
                    case 118: // v
                        map[(x, y)] = '#';
                        x++;
                        break;
                    case 46:
                    case 88:
                        map[(x, y)] = '.';
                        x++;
                        break;
                    case 10:
                        y++;
                        x = 0;
                        break;
                    case Int32.MinValue:
                        break;
                    default:
                        throw new Exception($"{output} is not recognized as a valid output");
                }

                // can be optimized
                width = Math.Max(width, x);
                height = Math.Max(height, y);
            }

            int result = 0;
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    if (map[(i, j)] == '#' && map[(i - 1, j)] == '#' && map[(i + 1, j)] == '#' &&
                        map[(i, j - 1)] == '#' && map[(i, j + 1)] == '#')
                    {
                        Console.WriteLine($"Found intersection at {i}, {j}. Alignement parameter is {i*j}");
                        result += i * j;
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}

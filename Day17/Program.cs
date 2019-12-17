using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices.ComTypes;
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


            // R,R,8,R,12,L,8,L,8,R,12, L,8,R,10, R, 10, L, 7

            string instructions = "A,B,C,B,C ";
            instructions += "R,R ";
            instructions += "8,R,12 ";
            instructions += "L,8,L ";
            instructions += "n ";
            int a = 0;

            Func<long> getInstructions = () =>
            {
                char c = instructions[a];
                a++;
                //Console.WriteLine($"asking for instructions, giving {c} : {(int) c}");
                if (c == ' ')
                {
                    return 10;
                }
                return (int) c;
            };

            bool firstV = true;

            while (output != Int32.MinValue)
            {
                output = reader.RunIntCode(getInstructions);
                int oldX = x;
                int oldY = y;
                switch (output)
                {
                    case 35:  // #
                        map[(x, y)] = '#';
                        x++;
                        break;
                    case 60:  // <
                        map[(x, y)] = '<';
                        x++;
                        break;
                    case 62:  // >
                        map[(x, y)] = '>';
                        x++;
                        break;
                    case 94:  // ^
                        map[(x, y)] = '^';
                        x++;
                        break;
                    //case 118: // v
                    //    map[(x, y)] = 'v';
                    //    if (firstV)
                    //    {
                    //        x--;
                    //        firstV = false;
                    //    }
                    //    x++;
                    //    break;
                    case 46:
                        map[(x, y)] = '.';
                        x++;
                        break;
                    case 88:
                        map[(x, y)] = 'X';
                        x++;
                        break;
                    case 10:
                        y++;
                        x = 0;
                        break;
                    case Int32.MinValue:
                        Console.ReadLine();
                        break;
                    default:
                        //Console.WriteLine($"Unexpected output : {(char) output}");
                        break;
                        // throw new Exception($"{output} is not recognized as a valid output");
                }

                if (map.ContainsKey((oldX, oldY)))
                {
                    Console.SetCursorPosition(oldX, oldY);
                    Console.Write(map[(oldX, oldY)]);
                }

                // can be optimized
                width = Math.Max(width, x);
                height = Math.Max(height, y);
            }
        }
    }
}

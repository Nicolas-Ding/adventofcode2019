using System;
using System.Collections.Generic;

namespace Day20
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<(int, int), char> map = new Dictionary<(int, int), char>();
            System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");
            string line;
            int i = 0;
            while ((line = file.ReadLine()) != null)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    map[(i,j)] = line[j];
                }
                i++;
            }
        }
    }
}

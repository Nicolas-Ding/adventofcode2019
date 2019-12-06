using System;
using System.Collections.Generic;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            Dictionary<string, List<string>> edges = new Dictionary<string, List<string>>();
            line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                string[] planets = line.Split(')');
                if (!edges.ContainsKey(planets[0]))
                {
                    edges[planets[0]] = new List<string>();
                }
                edges[planets[0]].Add(planets[1]);
                if (!edges.ContainsKey(planets[1]))
                {
                    edges[planets[1]] = new List<string>();
                }
                edges[planets[1]].Add(planets[0]);
                line = Console.ReadLine();
            }

            List<string> current = new List<string> {"YOU"};
            int step = 0;
            while (!current.Contains("SAN"))
            {
                List<string> nextCurrent = new List<string>();
                foreach (string planet in current)
                {
                    if (edges.ContainsKey(planet))
                    {
                        nextCurrent.AddRange(edges[planet]);
                        edges.Remove(planet);
                    }
                }

                current = nextCurrent;
                step++;
            }
            Console.WriteLine(step);
        }
    }
}

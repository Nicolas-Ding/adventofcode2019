using System;
using System.Collections.Generic;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            HashSet<(int, int)> planets = new HashSet<(int, int)>();
            int rowNumber = 0;
            while (!string.IsNullOrEmpty(line))
            {
                for (int columnNumber = 0; columnNumber < line.Length; columnNumber++)
                {
                    if (line[columnNumber] == '#')
                    {
                        planets.Add((columnNumber, rowNumber));
                    }
                }
                line = Console.ReadLine();
                rowNumber++;
            }

            int maxScore = 0;
            foreach ((int x, int y) tentativePosition in planets)
            {
                int currentScore = ComputePositionScore(tentativePosition, planets);
                if (currentScore > maxScore)
                {
                    Console.WriteLine($"{currentScore} : {tentativePosition.x},{tentativePosition.y}");
                    maxScore = currentScore;
                }
            }
        }

        static int ComputePositionScore((int x, int y) position, HashSet<(int, int)> map)
        {
            HashSet<(double, double)> viewingAngles = new HashSet<(double, double)>();

            foreach ((int x, int y) nextPlanet in map)
            {

                (int x, int y) relativePlanet = (nextPlanet.x - position.x, nextPlanet.y - position.y);
                if (relativePlanet.x == 0 && relativePlanet.y == 0)
                {
                    // Do nothing, current planet
                }
                else if (relativePlanet.x == 0)
                {
                    viewingAngles.Add((0, relativePlanet.y / Math.Abs((double) relativePlanet.y)));
                }
                else
                {
                    viewingAngles.Add((relativePlanet.x / Math.Abs((double) relativePlanet.x), relativePlanet.y / Math.Abs((double) relativePlanet.x)));
                }
            }

            return viewingAngles.Count;
        }
    }
}

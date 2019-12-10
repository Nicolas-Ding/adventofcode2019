using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();
            var planets = new HashSet<(int, int)>();
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
            var planetAnglesAndPositions = new Dictionary<double, List<(double, int, int)>>();
            foreach ((int x, int y) tentativePosition in planets)
            {
                var currentDict = ComputePositionScore(tentativePosition, planets);
                if (currentDict.Count > maxScore)
                {
                    Console.WriteLine($"{maxScore} : {tentativePosition.x},{tentativePosition.y}");
                    maxScore = currentDict.Count;
                    planetAnglesAndPositions = currentDict;
                }
            }

            var sortedPlanets = new SortedList<double, (int x, int y)>();

            foreach ((double angle, List<(double angle, int x, int y)> distances) in planetAnglesAndPositions)
            {
                int addedAngle = 0;
                distances.Sort();
                foreach ((double angle, int x, int y) distance in distances)
                {
                    sortedPlanets.Add(angle + addedAngle, (distance.x, distance.y));
                    addedAngle += 360;
                }
            }

            int i = 1;
            foreach (KeyValuePair<double, (int x, int y)> planet in sortedPlanets)
            {
                Console.WriteLine($"Asteroid {i} to be vaporized is {planet.Value.x},{planet.Value.y}");
                i++;
            }
        }

        static Dictionary<double, List<(double, int, int)>> ComputePositionScore((int x, int y) position, HashSet<(int, int)> map)
        {
            var angleAndPosition = new Dictionary<double, List<(double, int, int)>>();

            foreach ((int x, int y) nextPlanet in map)
            {

                (int x, int y) relativePlanet = (nextPlanet.x - position.x, nextPlanet.y - position.y);
                if (relativePlanet.x != 0 || relativePlanet.y != 0)
                {
                    double distance = Math.Sqrt(relativePlanet.x * relativePlanet.x + relativePlanet.y * relativePlanet.y);
                    double radian = Math.Atan2(relativePlanet.y, relativePlanet.x);
                    double angle = (radian * (180 / Math.PI) + 360 + 90) % 360;
                    if (!angleAndPosition.ContainsKey(angle))
                    {
                        angleAndPosition[angle] = new List<(double, int, int)>();
                    }
                    angleAndPosition[angle].Add((distance, nextPlanet.x, nextPlanet.y));
                }
            }

            return angleAndPosition;
        }
    }
}

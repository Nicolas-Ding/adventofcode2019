using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<(int, int, int, int, int, int, int, int), long> xCycles = new Dictionary<(int, int, int, int, int, int, int, int), long>();
            Dictionary<(int, int, int, int, int, int, int, int), long> yCycles = new Dictionary<(int, int, int, int, int, int, int, int), long>();
            Dictionary<(int, int, int, int, int, int, int, int), long> zCycles = new Dictionary<(int, int, int, int, int, int, int, int), long>();


            bool xFound = false;
            bool yFound = false;
            bool zFound = false;

            long xStart = 0;
            long xStep = 0;
            long yStart = 0;
            long yStep = 0;
            long zStart = 0;
            long zStep = 0;

            List<Moon> moons = new List<Moon>();
            //moons.Add(new Moon(-1, 0, 2));
            //moons.Add(new Moon(2, -10, -7));
            //moons.Add(new Moon(4, -8, 8));
            //moons.Add(new Moon(3, 5, -1));
            //moons.Add(new Moon(-8, -10, 0));
            //moons.Add(new Moon(5, 5, 10));
            //moons.Add(new Moon(2, -7, 3));
            //moons.Add(new Moon(9, -8, -3));
            moons.Add(new Moon(12, 0, -15));
            moons.Add(new Moon(-8, -5, -10));
            moons.Add(new Moon(7, -17, 1));
            moons.Add(new Moon(2, -11, -6));

            for (long i = 0; i < 1000000000; i++)
            {
                xCycles[(
                    moons[0].Position.X,moons[0].Velocity.X, 
                    moons[1].Position.X, moons[1].Velocity.X, 
                    moons[2].Position.X, moons[2].Velocity.X, 
                    moons[3].Position.X, moons[3].Velocity.X)] = i;

                yCycles[(
                    moons[0].Position.Y, moons[0].Velocity.Y,
                    moons[1].Position.Y, moons[1].Velocity.Y,
                    moons[2].Position.Y, moons[2].Velocity.Y,
                    moons[3].Position.Y, moons[3].Velocity.Y)] = i;

                zCycles[(
                    moons[0].Position.Z, moons[0].Velocity.Z,
                    moons[1].Position.Z, moons[1].Velocity.Z,
                    moons[2].Position.Z, moons[2].Velocity.Z,
                    moons[3].Position.Z, moons[3].Velocity.Z)] = i;

                // Apply gravity on velocity
                foreach (Moon moon in moons)
                {
                    foreach (Moon moon2 in moons)
                    {
                        moon.Velocity += Point3D.Sign(moon2.Position - moon.Position);
                    }
                }
                // Apply velocity on position
                foreach (Moon moon in moons)
                {
                    moon.Position += moon.Velocity;
                }

                if (!xFound && xCycles.ContainsKey(
                    (   moons[0].Position.X, moons[0].Velocity.X,
                        moons[1].Position.X, moons[1].Velocity.X,
                        moons[2].Position.X, moons[2].Velocity.X,
                        moons[3].Position.X, moons[3].Velocity.X)))
                {
                    long start = xCycles[(moons[0].Position.X, moons[0].Velocity.X,
                        moons[1].Position.X, moons[1].Velocity.X,
                        moons[2].Position.X, moons[2].Velocity.X,
                        moons[3].Position.X, moons[3].Velocity.X)];
                    Console.WriteLine($"Found a cycle for X coordinates starting at {start} with cycle {i - start + 1}");
                    xFound = true;
                    xStart = start;
                    xStep = i - start + 1;
                }


                if (!yFound && yCycles.ContainsKey(
                    (moons[0].Position.Y, moons[0].Velocity.Y,
                        moons[1].Position.Y, moons[1].Velocity.Y,
                        moons[2].Position.Y, moons[2].Velocity.Y,
                        moons[3].Position.Y, moons[3].Velocity.Y)))
                {
                    long start = yCycles[(moons[0].Position.Y, moons[0].Velocity.Y,
                        moons[1].Position.Y, moons[1].Velocity.Y,
                        moons[2].Position.Y, moons[2].Velocity.Y,
                        moons[3].Position.Y, moons[3].Velocity.Y)];
                    Console.WriteLine($"Found a cycle for Y coordinates starting at {start} with cycle {i - start + 1}");
                    yFound = true;
                    yStart = start;
                    yStep = i - start + 1;
                }


                if (!zFound && zCycles.ContainsKey(
                    (moons[0].Position.Z, moons[0].Velocity.Z,
                        moons[1].Position.Z, moons[1].Velocity.Z,
                        moons[2].Position.Z, moons[2].Velocity.Z,
                        moons[3].Position.Z, moons[3].Velocity.Z)))
                {
                    long start = zCycles[(moons[0].Position.Z, moons[0].Velocity.Z,
                        moons[1].Position.Z, moons[1].Velocity.Z,
                        moons[2].Position.Z, moons[2].Velocity.Z,
                        moons[3].Position.Z, moons[3].Velocity.Z)];
                    Console.WriteLine($"Found a cycle for Z coordinates starting at {start} with cycle {i - start + 1}");
                    zFound = true;
                    zStart = start;
                    zStep = i - start + 1;
                }

                if (xFound && yFound && zFound)
                {
                    break;
                }
            }

            do
            {
                if (xStart <= yStart && xStart <= zStart)
                {
                    xStart += xStep;
                }
                else if (yStart <= xStart && yStart <= zStart)
                {
                    yStart += yStep;
                }
                else if (zStart <= xStart && zStart <= yStart)
                {
                    zStart += zStep;
                }
                else
                {
                    throw new Exception("shouldn't get here");
                }
            } while (!(xStart == yStart && yStart == zStart));

            Console.WriteLine($"{xStart}, {yStart}, {zStart}");
        }
    }
}

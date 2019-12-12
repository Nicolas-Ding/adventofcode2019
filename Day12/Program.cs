using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day12
{
    class Program
    {
        static long gcd(long a, long b)
        {
            return b == 0 ? a : gcd(b, a % b);
        }
        static long lcm(long m, long n)
        {
            return (long) (m * n) / gcd(m, n);
        }

        static void Main(string[] args)
        {
            HashSet<(int, int, int, int, int, int, int, int)> xCycles = new HashSet<(int, int, int, int, int, int, int, int)>();
            HashSet<(int, int, int, int, int, int, int, int)> yCycles = new HashSet<(int, int, int, int, int, int, int, int)>();
            HashSet<(int, int, int, int, int, int, int, int)> zCycles = new HashSet<(int, int, int, int, int, int, int, int)>();


            bool xFound = false;
            bool yFound = false;
            bool zFound = false;

            long xStep = 0;
            long yStep = 0;
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

            xCycles.Add((
                moons[0].Position.X, moons[0].Velocity.X,
                moons[1].Position.X, moons[1].Velocity.X,
                moons[2].Position.X, moons[2].Velocity.X,
                moons[3].Position.X, moons[3].Velocity.X));

            yCycles.Add((
                moons[0].Position.Y, moons[0].Velocity.Y,
                moons[1].Position.Y, moons[1].Velocity.Y,
                moons[2].Position.Y, moons[2].Velocity.Y,
                moons[3].Position.Y, moons[3].Velocity.Y));

            zCycles.Add((
                moons[0].Position.Z, moons[0].Velocity.Z,
                moons[1].Position.Z, moons[1].Velocity.Z,
                moons[2].Position.Z, moons[2].Velocity.Z,
                moons[3].Position.Z, moons[3].Velocity.Z));

            for (long i = 0; i < 1000000000; i++)
            {

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

                if (!xFound && xCycles.Contains(
                    (   moons[0].Position.X, moons[0].Velocity.X,
                        moons[1].Position.X, moons[1].Velocity.X,
                        moons[2].Position.X, moons[2].Velocity.X,
                        moons[3].Position.X, moons[3].Velocity.X)))
                {
                    Console.WriteLine($"Found a cycle for X coordinates with cycle {i + 1}");
                    xFound = true;
                    xStep = i + 1;
                }


                if (!yFound && yCycles.Contains(
                    (moons[0].Position.Y, moons[0].Velocity.Y,
                        moons[1].Position.Y, moons[1].Velocity.Y,
                        moons[2].Position.Y, moons[2].Velocity.Y,
                        moons[3].Position.Y, moons[3].Velocity.Y)))
                {
                    Console.WriteLine($"Found a cycle for Y coordinates with cycle {i + 1}");
                    yFound = true;
                    yStep = i + 1;
                }


                if (!zFound && zCycles.Contains(
                    (moons[0].Position.Z, moons[0].Velocity.Z,
                        moons[1].Position.Z, moons[1].Velocity.Z,
                        moons[2].Position.Z, moons[2].Velocity.Z,
                        moons[3].Position.Z, moons[3].Velocity.Z)))
                {
                    Console.WriteLine($"Found a cycle for Z coordinates with cycle {i + 1}");
                    zFound = true;
                    zStep = i + 1;
                }

                if (xFound && yFound && zFound)
                {
                    break;
                }
            }

            Console.WriteLine($"{lcm(zStep, lcm(xStep, yStep))}");
        }
    }
}

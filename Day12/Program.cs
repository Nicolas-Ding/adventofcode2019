using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Moon> moons = new List<Moon>();
            /*moons.Add(new Moon(-8, -10, 0));
            moons.Add(new Moon(5, 5, 10));
            moons.Add(new Moon(2, -7, 3));
            moons.Add(new Moon(9, -8, -3));*/
            moons.Add(new Moon(12, 0, -15));
            moons.Add(new Moon(-8, -5, -10));
            moons.Add(new Moon(7, -17, 1));
            moons.Add(new Moon(2, -11, -6));

            for (int i = 0; i < 1000000000; i++)
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
            }
        }
    }
}

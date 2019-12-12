using System;
using System.Collections.Generic;
using System.Text;

namespace Day12
{
    public class Moon
    {
        public Point3D Velocity { get; set; }  
        
        public Point3D Position { get; set; }

        public int Energy { get => Velocity.Energy * Position.Energy; }

        public Moon(int x, int y, int z)
        {
            Position = new Point3D()
            {
                X = x,
                Y = y,
                Z = z
            };
            Velocity = new Point3D();
        }
    }
}

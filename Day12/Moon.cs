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

        protected bool Equals(Moon other)
        {
            return Equals(Velocity, other.Velocity) && Equals(Position, other.Position);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Moon) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Velocity != null ? Velocity.GetHashCode() : 0) * 397) ^ (Position != null ? Position.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Moon left, Moon right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Moon left, Moon right)
        {
            return !Equals(left, right);
        }
    }
}

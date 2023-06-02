using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor
{
    public class Dot
    {
        public double X { get; set; }

        public double Y { get; set; }

        public Dot() { }
        public Dot(double x, double y)
        {
            X = x; Y = y;
        }
        public double DistanceTo(Dot otherDot)
        {
            double dx = otherDot.X - X;
            double dy = otherDot.Y - Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor.part_A
{
    internal class Dot
    {
        public double X { get; set; }

        public double Y { get; set; }

        public Dot(double x, double y) 
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}

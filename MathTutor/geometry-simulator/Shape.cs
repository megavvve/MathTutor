using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor
{
    abstract class Shape
    {
        public abstract double Square();
        public abstract double Perimeter();
        public abstract void Rotate(double a);
        public abstract void Move(double offsetX, double offsetY)

        public abstract double GetDistanceFromCenter();

    }
}

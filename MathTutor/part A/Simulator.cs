using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor.part_A
{
    internal class Simulator
    {
        public List<Rectangle> Rectangles = new List<Rectangle>();

        public Simulator() { }

        public void PrintRectangles()
        {
            foreach (var rect in Rectangles)
            {
                Console.WriteLine(rect);
            }
        }

        public Rectangle[] GetRectanglesAccordingToPredicate(Predicate<Rectangle> predicate)
        {
            return Rectangles.Where(x => predicate(x)).ToArray();
        }

        public void ShiftCoordinates(Rectangle rect, double X, double Y)
        {
            foreach (var vertice in rect.GetVertices())
            {
                vertice.X += X; vertice.Y += Y;
            }
        }

        public Rectangle GetFarthestRectangle()
        {
            var max = double.MinValue;
            Rectangle farthestRectangle = null; 
            foreach (var rect in Rectangles)
            {
                if (rect.GetVertices().Select(x => Math.Sqrt(x.X * x.X + x.Y * x.Y)).Min() > max)
                {
                    farthestRectangle = rect;
                }
            }
            return farthestRectangle;
        }

        public void StretchRectangle(Rectangle rect, int coefX, int coefY)
        {

        }

        public void R(Rectangle rect, int a)
        {
            foreach (var vertices in rect.GetVertices())
            {
                var x = vertices.X - rect.Center.X;
                var y = vertices.Y - rect.Center.Y;
                var p = Math.Sqrt(x * x + y * y);
                var newx = Math.Cos(Math.Acos(x / p) + a) * p;
                vertices.X = newx + rect.Center.X;
                vertices.Y = Math.Sqrt(p * p - newx * newx) + rect.Center.Y;
            }
        }
    }
}

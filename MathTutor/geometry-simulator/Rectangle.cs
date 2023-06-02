using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor
{
    class Rectangle : Shape
    {
        private Dot a;
        public Dot A
        {
            get { return a; }
            private set
            {
                if (value != null)
                {
                    a = value;
                }
            }
        }

        private Dot b;
        public Dot B
        {
            get { return b; }
            private set
            {
                if (value != null)
                {
                    b = value;
                }
            }
        }


        private Dot c;
        public Dot C
        {
            get { return c; }
            private set
            {
                if (value != null)
                {
                    c = value;
                }
            }
        }

        private Dot d;
        public Dot D
        {
            get { return d; }
            private set
            {
                if (value != null)
                {
                    d = value;
                }
            }
        }

        public Dot Center
        {
            get { return new Dot { X = (A.X + C.X) / 2, Y = (A.Y + C.Y) / 2 }; }
        }


        public Rectangle(Dot a, Dot b, Dot c, Dot d)
        {
            if (IsRightAngle(a, b, c) && IsRightAngle(b, c, d))
            {
                A = a; B = b; C = c; D = d;
            }
            else
            {
                throw new Exception("это не прямоугольник");
            }
        }
        private bool IsRightAngle(Dot a, Dot b, Dot c)
        {
            var firstcathetus = Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
            var secondcathetus = Math.Sqrt((c.X - b.X) * (c.X - b.X) + (c.Y - b.Y) * (c.Y - b.Y));
            var hypotenuse = Math.Sqrt((c.X - a.X) * (c.X - a.X) + (c.Y - a.Y) * (c.Y - a.Y));
            return hypotenuse == Math.Sqrt(firstcathetus * firstcathetus + secondcathetus * secondcathetus);
        }


        public override double Square()
        {
            var AB = Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
            var BC = Math.Sqrt((C.X - B.X) * (C.X - B.X) + (C.Y - B.Y) * (C.Y - B.Y));
            return 2 * (AB + BC);
        }

        public override double Perimeter()
        {
            var AB = Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
            var BC = Math.Sqrt((C.X - B.X) * (C.X - B.X) + (C.Y - B.Y) * (C.Y - B.Y));
            return AB * BC;
        }

        public List<Dot> GetVertices()
        {
            return new List<Dot>() { A, B, C, D };
        }
        public override double GetDistanceFromCenter()
        {

            double distance = Math.Sqrt(Math.Pow(Center.X, 2) + Math.Pow(Center.Y, 2));

            return distance;
        }
        public override void Rotate(double a)
        {
            foreach (var vertices in GetVertices())
            {
                var x = vertices.X - Center.X;
                var y = vertices.Y - Center.Y;
                var p = Math.Sqrt(x * x + y * y);
                var newx = Math.Cos(Math.Acos(x / p) + a) * p;
                vertices.X = newx + Center.X;
                vertices.Y = Math.Sqrt(p * p - newx * newx) + Center.Y;
            }
        }
        public override void Move(double offsetX, double offsetY)
        {
            A.X += offsetX;
            A.Y += offsetY;

            B.X += offsetX;
            B.Y += offsetY;

            C.X += offsetX;
            C.Y += offsetY;

            D.X += offsetX;
            D.Y += offsetY;
        }

        public override string ToString()
        {
            return $"Прямоугольник: левая верхняя точка{A}, правая верхняя точка({B.X}, {B.Y}), правая нижняя точка({C.X}, {D.Y}), левая нижняя точка({D.X}, {D.Y})";
        }

    }
}

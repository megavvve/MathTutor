using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor.part_A
{
    internal class Rectangle
    {
        private Dot a { get; set; }
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

        private Dot b { get; set; }
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


        private Dot c { get; set; }
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

        private Dot d { get; set; }
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


        public double Square()
        {
            var AB = Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
            var BC = Math.Sqrt((C.X - B.X) * (C.X - B.X) + (C.Y - B.Y) * (C.Y - B.Y));
            return 2*(AB + BC);
        }

        public double Perimeter()
        {
            var AB = Math.Sqrt((A.X - B.X) * (A.X - B.X) + (A.Y - B.Y) * (A.Y - B.Y));
            var BC = Math.Sqrt((C.X - B.X) * (C.X - B.X) + (C.Y - B.Y) * (C.Y - B.Y));
            return AB * BC;
        }

        public List<Dot> GetVertices()
        {
            return new List<Dot>() { A, B , C, D };
        }

        public override string ToString()
        {
            return $"A{A}, B({B.X}, {B.Y}), C({C.X}, {D.Y}), D({D.X}, {D.Y})"; 
        }
    }
}

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
                if (value  != null)
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
            get { return new Dot {X = Math.Abs(A.X - C.X), Y = Math.Abs(A.Y - C.Y) }; }
        }

        private bool IsRightAngle(Dot a, Dot b, Dot c)
        {
            var firstcathetus = Math.Sqrt((a.X - b.X)* (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
            var secondcathetus = Math.Sqrt((c.X - b.X)* (c.X - b.X) + (c.Y - b.Y) * (c.Y - b.Y));
            var hypotenuse = Math.Sqrt((c.X - a.X) * (c.X - a.X) + (c.Y - a.Y) * (c.Y - a.Y));
            return hypotenuse * hypotenuse == firstcathetus * firstcathetus + secondcathetus * secondcathetus;
        }

        public Rectangle(Dot a, Dot b, Dot c, Dot d)
        {
            if (IsRightAngle(a, b, c) && IsRightAngle(b, c, d))
            {
                A = a; B = b; C = c; D = d;
            }
        }
    }
}

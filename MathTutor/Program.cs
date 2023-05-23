using MathTutor.part_A;
using System;
using System.Security.Cryptography.X509Certificates;

namespace MathTutor 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var r1 = new Rectangle(new Dot { X = 0, Y = 0 }, new Dot { X = 4, Y = 0 }, new Dot { X = 4, Y = -3 }, new Dot { X = 0, Y = -3 });
            var r2 = new Rectangle(new Dot { X = 5, Y = 8}, new Dot { X = 5, Y = 10}, new Dot { X = 10, Y = 10}, new Dot { X = 10, Y = 8});
            
            var s = new Simulator();
            s.Rectangles.Add(r1);
            s.Rectangles.Add(r2);
            Console.WriteLine(r1);
            s.R(r1, 90);
            Console.WriteLine(r1);
            s.ShiftCoordinates(r1, 2, 1);
            Console.WriteLine(r1);
            Console.WriteLine(s.GetFarthestRectangle());
        }
    }
}
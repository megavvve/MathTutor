using System;
using System.Security.Cryptography.X509Certificates;

namespace MathTutor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Rectangle rectangle1 = new Rectangle(new Dot { X = -3, Y = 4 }, new Dot { X = 3, Y = 4 }, new Dot { X = 3, Y = 1 }, new Dot { X = -3, Y = 1 });
            Rectangle rectangle2 = new Rectangle(new Dot { X = 7, Y = 3 }, new Dot { X = 10, Y = 3 }, new Dot { X = 10, Y = 1 }, new Dot { X = 7, Y = 1 });
            */
            Simulator simulator = new Simulator();
            simulator.GeometrySimulator();

        }
    }
}
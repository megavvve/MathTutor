using System;

namespace MathTutor 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var g = new GeneratingControlWork();
            //g.PrintLists(g.Generating(5, 5));
            g.GenerateFiles(g.Generating(5, 5));
        }
    }
}
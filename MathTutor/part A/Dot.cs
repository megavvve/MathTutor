using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor.part_A
{
    internal class Dot
    {
        public required double X { get; init; }

        public required double Y { get; init; }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}

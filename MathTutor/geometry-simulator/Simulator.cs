using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor
{
    class Simulator
    {
        public List<Shape> listOfShapes = new List<Shape>();

        public Simulator() { }

        public void GeometrySimulator()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Привет, это симулятор геометрии");
            }
        }
        private void PrintShapes()
        {
            foreach (var rect in listOfShapes)
            {
                Console.WriteLine(rect);
            }
        }


        private Shape[] GetShapesAccordingToPredicate(Predicate<Shape> predicate)
        {
            return listOfShapes.Where(x => predicate(x)).ToArray();
        }



        private Shape GetFarthestRectangle()
        {
            if (listOfShapes == null || listOfShapes.Count == 0)
            {
                throw new InvalidOperationException("В симуляторе нет фигур");
            }

            Shape farthestShape = null;
            double maxDistance = double.MinValue;

            foreach (Shape shape in listOfShapes)
            {
                double distance = shape.GetDistanceFromCenter();
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    farthestShape = shape;
                }
            }

            return farthestShape;
        }
        private int CountCircularEllipses(List<Ellipse> ellipses)
        {
            int count = 0;

            foreach (Ellipse ellipse in ellipses)
            {
                if (IsCircularEllipse(ellipse))
                {
                    count++;
                }
            }

            return count;
        }
        private bool IsCircularEllipse(Ellipse ellipse)
        {

            double distanceLR = ellipse.LeftDot.DistanceTo(ellipse.RightDot);
            double distanceUD = ellipse.UpDot.DistanceTo(ellipse.DownDot);


            if (Math.Abs(distanceLR - distanceUD) < double.Epsilon)
            {
                return true;
            }

            return false;
        }

        private Rectangle GetRectangleWithMinPerimeter(List<Rectangle> rectangles)
        {
            if (rectangles == null || rectangles.Count() == 0)
                return null;

            Rectangle minPerimeterRectangle = rectangles[0];
            double minPerimeter = minPerimeterRectangle.Perimeter();

            for (int i = 1; i < rectangles.Count(); i++)
            {
                double perimeter = rectangles[i].Perimeter();
                if (perimeter < minPerimeter)
                {
                    minPerimeter = perimeter;
                    minPerimeterRectangle = rectangles[i];
                }
            }

            return minPerimeterRectangle;
        }




    }
}

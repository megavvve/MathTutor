using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor
{
    class Ellipse : Shape
    {
        public Dot LeftDot { get; private set; }
        public Dot RightDot { get; private set; }
        public Dot UpDot { get; private set; }
        public Dot DownDot { get; private set; }

        public Dot Center
        {
            get
            {
                return new Dot((LeftDot.X + RightDot.X) / 2, (UpDot.Y + DownDot.Y) / 2);
            }

        }
        public Ellipse(Dot leftDot, Dot rightDot, Dot upDot, Dot downDot)
        {
            if (leftDot == null || rightDot == null || upDot == null || downDot == null)
            {
                throw new ArgumentNullException("Одна или несколько точек эллипса не были инициализированы.");
            }
            if (isEllipse(leftDot, rightDot, upDot, downDot))
            {

                LeftDot = leftDot;
                RightDot = rightDot;
                UpDot = upDot;
                DownDot = downDot;

            }
            else
            {
                throw new Exception("Это не эллипc");
            }
        }
        private bool isEllipse(Dot leftDot, Dot rightDot, Dot upDot, Dot downDot)
        {

            double centerX = (leftDot.X + rightDot.X) / 2;
            double centerY = (upDot.Y + downDot.Y) / 2;
            Dot center = new Dot(centerX, centerY);

            double semiMajorAxis = Math.Abs(rightDot.X - leftDot.X) / 2;
            double semiMinorAxis = Math.Abs(downDot.Y - upDot.Y) / 2;

            bool isEllipse = true;
            isEllipse &= DistanceTo(center, leftDot) <= semiMajorAxis;
            isEllipse &= DistanceTo(center, rightDot) <= semiMajorAxis;
            isEllipse &= DistanceTo(center, upDot) <= semiMinorAxis;
            isEllipse &= DistanceTo(center, downDot) <= semiMinorAxis;

            return isEllipse; 
        }
        private double DistanceTo(Dot point1, Dot point2)
        {
            double deltaX = point2.X - point1.X;
            double deltaY = point2.Y - point1.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
        public override double Square()
        {
            double a = Math.Sqrt(Math.Pow(RightDot.X - LeftDot.X, 2) + Math.Pow(RightDot.Y - LeftDot.Y, 2)) / 2;
            double b = Math.Sqrt(Math.Pow(DownDot.X - UpDot.X, 2) + Math.Pow(DownDot.Y - UpDot.Y, 2)) / 2;

            double area = Math.PI * a * b;
            return area;
        }
        public override double Perimeter()
        {
            double a = Math.Abs(RightDot.X - LeftDot.X) / 2;
            double b = Math.Abs(UpDot.Y - DownDot.Y) / 2;

            double perimeter = Math.PI * (3 * (a + b) - Math.Sqrt((3 * a + b) * (a + 3 * b)));

            return perimeter;
        }
        public override void Rotate(double a)
        {

            double angleRad = a * Math.PI / 180;

            double offsetX = Center.X;
            double offsetY = Center.Y;
            double x1 = LeftDot.X - offsetX;
            double y1 = LeftDot.Y - offsetY;
            double x2 = RightDot.X - offsetX;
            double y2 = RightDot.Y - offsetY;
            double x3 = UpDot.X - offsetX;
            double y3 = UpDot.Y - offsetY;
            double x4 = DownDot.X - offsetX;
            double y4 = DownDot.Y - offsetY;

            double newX1 = x1 * Math.Cos(angleRad) - y1 * Math.Sin(angleRad);
            double newY1 = x1 * Math.Sin(angleRad) + y1 * Math.Cos(angleRad);
            double newX2 = x2 * Math.Cos(angleRad) - y2 * Math.Sin(angleRad);
            double newY2 = x2 * Math.Sin(angleRad) + y2 * Math.Cos(angleRad);
            double newX3 = x3 * Math.Cos(angleRad) - y3 * Math.Sin(angleRad);
            double newY3 = x3 * Math.Sin(angleRad) + y3 * Math.Cos(angleRad);
            double newX4 = x4 * Math.Cos(angleRad) - y4 * Math.Sin(angleRad);
            double newY4 = x4 * Math.Sin(angleRad) + y4 * Math.Cos(angleRad);

            LeftDot = new Dot(newX1 + offsetX, newY1 + offsetY);
            RightDot = new Dot(newX2 + offsetX, newY2 + offsetY);
            UpDot = new Dot(newX3 + offsetX, newY3 + offsetY);
            DownDot = new Dot(newX4 + offsetX, newY4 + offsetY);
        }
        public override void Move(double offsetX, double offsetY)
        {

            LeftDot = new Dot(LeftDot.X + offsetX, LeftDot.Y + offsetY);
            RightDot = new Dot(RightDot.X + offsetX, RightDot.Y + offsetY);
            UpDot = new Dot(UpDot.X + offsetX, UpDot.Y + offsetY);
            DownDot = new Dot(DownDot.X + offsetX, DownDot.Y + offsetY);
        }
        public void Scale(double widthScale, double heightScale)
        {

            double centerX = Center.X;
            double centerY = Center.Y;

            double newLeftX = centerX - (centerX - LeftDot.X) * widthScale;
            double newRightX = centerX + (RightDot.X - centerX) * widthScale;
            double newUpY = centerY - (centerY - UpDot.Y) * heightScale;
            double newDownY = centerY + (DownDot.Y - centerY) * heightScale;


            LeftDot = new Dot(newLeftX, LeftDot.Y);
            RightDot = new Dot(newRightX, RightDot.Y);
            UpDot = new Dot(UpDot.X, newUpY);
            DownDot = new Dot(DownDot.X, newDownY);
        }
        public override double GetDistanceFromCenter()
        {

            double distance = Math.Sqrt(Math.Pow(Center.X, 2) + Math.Pow(Center.Y, 2));

            return distance;
        }



        public override string ToString()
        {
            return $"Эллипс с левой точкой: {LeftDot}, правой точкой: {RightDot}, верхней точкой: {UpDot}, нижней точкой: {DownDot}, центром: {Center}";
        }

    }
}

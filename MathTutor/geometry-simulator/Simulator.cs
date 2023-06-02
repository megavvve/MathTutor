using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor
{
    class GeometrySimulator
    {
        public List<Shape> listOfShapes = new List<Shape>();

        public GeometrySimulator() { }

        public void GeometrySimulatorWork()
        {

            GeometrySimulator simulator = new GeometrySimulator();
            bool isRunning = true;
            Console.Clear();
            while (isRunning)
            {

                Console.WriteLine("Выберите операцию:");
                Console.WriteLine("1. Добавить прямоугольник");
                Console.WriteLine("2. Добавить эллипс");
                Console.WriteLine("3. Вывести все фигуры");
                Console.WriteLine("4. Определить площадь фигур");
                Console.WriteLine("5. Определить наиболее удаленную фигуру");
                Console.WriteLine("6. Повернуть фигуру");
                Console.WriteLine("7. Переместить фигуру");
                Console.WriteLine("8. Увеличить размер фигуры");
                Console.WriteLine("9. Получить фигуры по предикату");
                Console.WriteLine("10. Получить прямоугольник с минимальным периметром");
                Console.WriteLine("11. Получить количество окружностей-эллипсов");
                Console.WriteLine("12. Отчистить консоль");
                Console.WriteLine("13. Выйти из программы");
                Console.WriteLine();
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();
                Console.WriteLine();
                Thread.Sleep(1000);
                switch (choice)
                {
                    case "1":
                        try
                        {
                            CreateRectangle();
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Это не прямоугольник(");
                        }

                        break;
                    case "2":
                        try
                        {
                            CreateEllipse();
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Это не эллипс(");
                        }

                        break;
                    case "3":
                        Console.WriteLine("Все фигуры на плоскости:");
                        PrintShapes();
                        Console.WriteLine();
                        break;
                    case "4":
                        CalculateArea();
                        break;
                    case "5":
                        FindMostDistantShape();
                        break;
                    case "6":
                        RotateShape();
                        break;
                    case "7":
                        MoveShape();
                        break;
                    case "8":
                        ScaleShape();
                        break;
                    case "9":
                        FilterShapes();
                        break;
                    case "10":
                        FindMinPerimeterRectangle();
                        break;
                    case "11":
                        CountCircularEllipses();
                        break;
                    case "12":
                        Console.Clear();
                        Console.WriteLine("Консоль была отчищена)");
                        break;
                    case "13":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор операции. Попробуйте снова.");
                        break;
                }

                Console.WriteLine();
                Thread.Sleep(1000);
            }
        }
        private void CreateRectangle()
        {

            Console.WriteLine("Введите координаты прямоугольника (A, B, C, D):");
            Console.WriteLine();
            Console.WriteLine("Для проверки могу предложить следующие прямоуголники:\nRectangle rectangle1 = new Rectangle(new Dot { X = -3, Y = 4 }, new Dot { X = 3, Y = 4 }, new Dot { X = 3, Y = 1 }, new Dot { X = -3, Y = 1 });\nRectangle rectangle2 = new Rectangle(new Dot { X = 7, Y = 3 }, new Dot { X = 10, Y = 3 }, new Dot { X = 10, Y = 1 }, new Dot { X = 7, Y = 1 });");
            Console.WriteLine();
            Console.Write("X левой верхней точки: ");
            double ax = double.Parse(Console.ReadLine());

            Console.Write("Y левой верхней точки: ");
            double ay = double.Parse(Console.ReadLine());

            Console.Write("X правой верхней точки: ");
            double bx = double.Parse(Console.ReadLine());

            Console.Write("Y правой верхней точки: ");
            double by = double.Parse(Console.ReadLine());

            Console.Write("X правой нижней точки: ");
            double cx = double.Parse(Console.ReadLine());

            Console.Write("Y правой нижней точки: ");
            double cy = double.Parse(Console.ReadLine());

            Console.Write("X левой нижней точки: ");
            double dx = double.Parse(Console.ReadLine());

            Console.Write("Y левой нижней точки: ");
            double dy = double.Parse(Console.ReadLine());

            Rectangle rectangle = new Rectangle(new Dot(ax, ay), new Dot(bx, by), new Dot(cx, cy), new Dot(dx, dy));
            listOfShapes.Add(rectangle);

            Console.WriteLine("Прямоугольник успешно добавлен.");
        }

        private void CreateEllipse()
        {
            Console.WriteLine("Введите координаты эллипса (LeftDot, RightDot, UpDot, DownDot):");
            Console.WriteLine();
            Console.WriteLine("Для проверки могу предложить такой эллипс:Ellipse (LeftDot:(-2,2), RightDot:(2,2), UpDot(0,3), DownDot(0,1))");
            Console.WriteLine();
            Console.Write("LeftDot.X: ");
            double leftX = double.Parse(Console.ReadLine());

            Console.Write("LeftDot.Y: ");
            double leftY = double.Parse(Console.ReadLine());

            Console.Write("RightDot.X: ");
            double rightX = double.Parse(Console.ReadLine());

            Console.Write("RightDot.Y: ");
            double rightY = double.Parse(Console.ReadLine());

            Console.Write("UpDot.X: ");
            double upX = double.Parse(Console.ReadLine());

            Console.Write("UpDot.Y: ");
            double upY = double.Parse(Console.ReadLine());

            Console.Write("DownDot.X: ");
            double downX = double.Parse(Console.ReadLine());

            Console.Write("DownDot.Y: ");
            double downY = double.Parse(Console.ReadLine());

            Ellipse ellipse = new Ellipse(new Dot(leftX, leftY), new Dot(rightX, rightY), new Dot(upX, upY), new Dot(downX, downY));
            listOfShapes.Add(ellipse);

            Console.WriteLine("Эллипс успешно добавлен.");
        }

        private void CalculateArea()
        {
            Console.WriteLine("Выберите фигуру для расчета площади:");
            PrintShapes();

            Console.Write("Введите номер фигуры: ");
            int index = int.Parse(Console.ReadLine());

            double area = listOfShapes[index - 1].Square();
            Console.WriteLine("Площадь фигуры: " + area);
        }

        private void FindMostDistantShape()
        {
            Shape mostDistant = GetFarthestShape();
            Console.WriteLine("Наиболее удаленная фигура от центра координат: " + mostDistant);
        }

        private void RotateShape()
        {
            Console.WriteLine("Выберите фигуру для поворота:");
            PrintShapes();

            Console.Write("Введите номер фигуры: ");
            int index = int.Parse(Console.ReadLine());

            Console.Write("Введите угол поворота (в градусах): ");
            double angle = double.Parse(Console.ReadLine());

            listOfShapes[index - 1].Rotate(angle);
            Console.WriteLine("Фигура успешно повернута.");
        }

        private void MoveShape()
        {
            Console.WriteLine("Выберите фигуру для перемещения:");
            PrintShapes();

            Console.Write("Введите номер фигуры: ");
            int index = int.Parse(Console.ReadLine());

            Console.Write("Введите сдвиг по оси X: ");
            double shiftX = double.Parse(Console.ReadLine());

            Console.Write("Введите сдвиг по оси Y: ");
            double shiftY = double.Parse(Console.ReadLine());

            listOfShapes[index - 1].Move(shiftX, shiftY);
            Console.WriteLine("Фигура успешно перемещена.");
        }

        private void ScaleShape()
        {
            Console.WriteLine("Выберите фигуру для изменения размера:");
            PrintShapes();

            Console.Write("Введите номер фигуры: ");
            int index = int.Parse(Console.ReadLine());


            Shape shape = listOfShapes[index - 1];
            if (shape is Ellipse)
            {
                Console.Write("Введите коэффициент увеличения высоты: ");
                double heightFactor = double.Parse(Console.ReadLine());

                Console.Write("Введите коэффициент увеличения ширины: ");
                double widthFactor = double.Parse(Console.ReadLine());
                var s = shape as Ellipse;
                s.Scale(widthFactor, heightFactor);
                Console.WriteLine("Размер фигуры успешно изменен.");

            }
            else
            {
                Console.WriteLine("Нету реализации для прямоуголника(");
            }


        }

        private void FilterShapes()
        {
            Console.WriteLine("Выберите предикат для фильтрации фигур:");
            Console.WriteLine("1. Фигуры с площадью больше 100");
            Console.WriteLine("2. Фигуры с площадью меньше 100");
            Console.WriteLine("3. Фигуры с площадью равной 100");

            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            Predicate<Shape> predicate;

            switch (choice)
            {
                case "1":
                    predicate = shape => shape.Square() > 100;
                    break;
                case "2":
                    predicate = shape => shape.Square() < 100;
                    break;
                case "3":
                    predicate = shape => shape.Square() == 100;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор предиката. Фигуры не будут отфильтрованы.");
                    return;
            }

            Shape[] filteredShapes = GetShapesAccordingToPredicate(predicate);

            Console.WriteLine("Отфильтрованные фигуры:");
            DisplayShapes(filteredShapes);
        }

        private void FindMinPerimeterRectangle()
        {
            Rectangle minPerimeterRectangle = GetRectangleWithMinPerimeter(listOfShapes.Where(x => x is Rectangle).Select(x => x as Rectangle).ToList());

            Console.WriteLine("Прямоугольник с минимальным периметром:");
            Console.WriteLine(minPerimeterRectangle);
        }

        private void CountCircularEllipses()
        {
            int count = CountCircEllipses(listOfShapes.Where(x => x is Ellipse).Select(x => x as Ellipse).ToList());
            Console.WriteLine("Количество окружностей-эллипсов: " + count);
        }



        static void DisplayShapes(IEnumerable<Shape> shapes)
        {
            int index = 1;
            foreach (Shape shape in shapes)
            {
                Console.WriteLine(index + ". " + shape);
                index++;
            }
        }

        private void PrintShapes()
        {
            int index = 1;
            Console.WriteLine();
            foreach (var shape in listOfShapes)
            {
                Console.WriteLine(index + ". " + shape);
                index++;
            }
            Console.WriteLine();
        }


        private Shape[] GetShapesAccordingToPredicate(Predicate<Shape> predicate)
        {
            return listOfShapes.Where(x => predicate(x)).ToArray();
        }



        private Shape GetFarthestShape()
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
        private int CountCircEllipses(List<Ellipse> ellipses)
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

using MathTutor.KnowlendgeCheck;
using MathTutor.part_A;
using System;

namespace MathTutor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в наше консольное приложение\nК Вашему вниманию предоставляется три" +
                    " разные части модуля по математике:\nA -  симулятор геометрии\nB - генереция контрольных работ" +
                    "\nС - зазубривание теории");
            while (true)
            {
                string input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "A":
                        {
                            var simulator = new Simulator();
                            break;
                        }
                    case "B":
                        {
                            GeneratingControlWork controlWork = new GeneratingControlWork();
                            Console.WriteLine("Введите количество вариантов");
                            int countControlWork = int.Parse(Console.ReadLine());
                            Console.WriteLine("Введите количество заданий в варианте");
                            int countTasks = int.Parse(Console.ReadLine());
                            controlWork.GenerateFiles(controlWork.Generating(countTasks, countControlWork));
                            Console.WriteLine("Проверьте папку bin, там сгенерировались варианты и подсказки к заданиям!");
                            break;
                        }
                    case "C":
                        {
                            //Не забудьте скопировать input-files в bin
                            FormulaTrainer trainer = new FormulaTrainer();
                            trainer.Training();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Неккоректный ввод");
                            break;
                        }
                }
                Console.WriteLine("Введите букву:\nA -  симулятор геометрии\nB - генереция контрольных работ\nС - зазубривание теории");
            }
            
        }
    }
}
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
                            //TODO
                            break;
                        }
                    case "B":
                        {
                            GeneratingControlWork controlWork = new GeneratingControlWork();
                            int countControlWork = int.Parse(Console.ReadLine());
                            int countTasks = int.Parse(Console.ReadLine());
                            controlWork.GenerateFiles(controlWork.Generating(countTasks, countControlWork));
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
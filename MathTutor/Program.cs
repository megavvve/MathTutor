﻿using MathTutor;
using MathTutor.MemorizingTheTheory;
using System;
using MathTutor.KnowlendgeCheck;

namespace MathTutor
{
    public class Program
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
                           GeometrySimulator simulator = new GeometrySimulator();
                            simulator.GeometrySimulatorWork();
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
                            Console.WriteLine("Выбирите, что Вы хотите видеть\n1 - формулы\n2 - теоремы");
                            int input2 = int.Parse(Console.ReadLine());
                            if(input2 == 1)
                            {
                                FormulaTrainer trainer = new FormulaTrainer();
                                trainer.Training();
                            }
                            if(input2 == 2)
                            {
                                TheoremTrainer trainer = new TheoremTrainer();
                                trainer.Train();
                            }
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



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor.KnowlendgeCheck
{
    public class GeneratingControlWork : ControlWork
    {


        public GeneratingControlWork()
        {
            ReadFiles();
        }



        /// <summary>
        /// Генерация  вариантов
        /// </summary>
        /// <param name="countTasks"></param>
        /// <returns></returns>
        public List<(string question, string hint, double answer)> Generating(int countTasks, int countControlWork)
        {
            if (countTasks > Questions.Count)
            {
                Console.WriteLine("Недостаточно вопросов для генерации.");
                return new List<(string question, string hint, double answer)>();
            }

            List<(string question, string hint, double answer)> controlWorks = new List<(string question, string hint, double answer)>();
            Random rnd = new Random();
            while (countControlWork != 0)
            {
                controlWorks.Add(($"                                                                                            Вариант номер {countControlWork}", string.Empty, 0));
                for (int i = 0; i < countTasks; i++)
                {
                    int r = rnd.Next(0, Questions.Count);
                    controlWorks.Add((Questions[r], Hints[r], Answers[r]));

                    Questions.RemoveAt(r);
                    Answers.RemoveAt(r);
                    Hints.RemoveAt(r);
                }
                countControlWork--;
            }

            return controlWorks;
        }


        /// <summary>
        /// печать вариантов
        /// </summary>
        /// <param name="lists"></param>
        public void PrintLists(List<(string question, string hint, double answer)> lists)
        {
            foreach (var item in lists)
            {
                Console.WriteLine(item.question);
                Console.WriteLine();
            }

        }

        /// <summary>
        /// Создает файлы с рандомными задачами
        /// </summary>
        /// <param name="lists"></param>
        public void GenerateFiles(List<(string question, string hint, double answer)> tasks)
        {
            string fileName = "./ControlWork.txt";
            string file = "./Hints.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var task in tasks)
                {
                    writer.WriteLine(task.question);
                }
            }
            using (StreamWriter writer = new StreamWriter(file))
            {

                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine("Подсказки");
                for (int i = 0; i < tasks.Count; i++)
                {
                    writer.WriteLine(i + ". " + tasks[i].hint);
                }
            }
        }


    }
}

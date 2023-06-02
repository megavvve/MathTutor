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
        public List<(string question, string hint, List<double> answer)> Generating(int countTasks, int countControlWork)
        {
            var a = QuestionsWithFourAnswers.Count / countControlWork;
            if (countTasks * countControlWork > Questions.Count + QuestionsWithFourAnswers.Count || a == 0 && countTasks * countControlWork > Questions.Count)
            {
                Console.WriteLine("Недостаточно вопросов для генерации.");
                return new List<(string question, string hint, List<double> answer)>();
            }
            var controlWorks = new List<(string question, string hint, List<double> answer)>();
            Random rnd = new Random();
            while (countControlWork != 0)
            {
                controlWorks.Add(($"                                           Вариант номер {countControlWork}", string.Empty, new List<double>()));
                for (int i = 0; i < a; i++)
                {
                    controlWorks.Add((QuestionsWithFourAnswers[0], HintsForQuestionsWithFourAnswers[0], AnswersForQuestionsWithFourAnswers[0]));
                    QuestionsWithFourAnswers.RemoveAt(0);
                    HintsForQuestionsWithFourAnswers.RemoveAt(0);
                    AnswersForQuestionsWithFourAnswers.RemoveAt(0);
                }
                for (int i = 0; i < countTasks - a; i++)
                {
                    int r = rnd.Next(0, Questions.Count);
                    var s = new List<double>() { Answers[r] };
                    controlWorks.Add((Questions[r], Hints[r], new List<double>() { Answers[r]}));
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
        public void GenerateFiles(List<(string question, string hint, List<double> answers)> tasks)
        {
            string fileName = "./ControlWork.txt";
            string file = "./Hints.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var task in tasks)
                {
                    writer.WriteLine(task.question);
                    if (task.answers.Count >= 1)
                    {
                        if (task.answers.Count > 1)
                        {
                            var i = 1;
                            foreach (var answer in task.answers)
                            {
                                writer.WriteLine($"{i}) {answer}");
                                i++;
                            }
                        }
                        writer.WriteLine($"Ответ - {task.answers[0]}");
                    }
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

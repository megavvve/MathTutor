using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathTutor.KnowlendgeCheck
{
    public class ControlWork
    {
        public List<string> Questions { get; }
        public List<string> QuestionsWithFourAnswers { get; }
        public List<string> Hints { get; }
        public List<string> HintsForQuestionsWithFourAnswers { get; }
        public List<double> Answers { get; }
        public List<List<double>> AnswersForQuestionsWithFourAnswers { get; }


        public ControlWork()
        {
            Questions = new List<string>();
            QuestionsWithFourAnswers = new List<string>();
            Hints = new List<string>();
            HintsForQuestionsWithFourAnswers = new List<string>();
            Answers = new List<double>();
            AnswersForQuestionsWithFourAnswers = new List<List<double>>();
        }

        public void ReadFiles()
        {
            string path = "./Tasks/Tasks.txt";
            var reader = File.ReadAllLines(path);
            foreach (var task in reader)
            {
                var parts = task.Split('|');
                if (parts.Length == 7)
                {
                    QuestionsWithFourAnswers.Add(parts[1]);
                    HintsForQuestionsWithFourAnswers.Add(parts[2]);
                    var answers = new List<double>();
                    for (int i = 3; i < parts.Length; i++)
                    {
                        answers.Add(double.Parse(parts[i]));
                    }
                    AnswersForQuestionsWithFourAnswers.Add(answers);
                }
                else
                {
                    Questions.Add(parts[1]);
                    Hints.Add(parts[2]);
                    Answers.Add(double.Parse(parts[3]));
                }
            }
        }

    }
}

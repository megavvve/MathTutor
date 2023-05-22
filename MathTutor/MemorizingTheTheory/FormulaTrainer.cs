﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathTutor
{
    public class FormulaTrainer
    {
        public Dictionary<string, List<Formula>> formulas = new Dictionary<string, List<Formula>>();
        public Dictionary<string, List<Formula>> wrongAnswers = new Dictionary<string, List<Formula>>();
        public Dictionary<string, List<Formula>> correctAnswers = new Dictionary<string, List<Formula>>();
        public FormulaTrainer() { }
        public void Training()
        {
            LoadFormulas(@"./input-files/formulas.txt");

            StartForTrainig();
            List<string> topics = SelectTopics();

            Dictionary<string, Formula> formulasFromTopics = FormulasFromTopics(topics);
            if (formulasFromTopics.Count == 0)
            {
                Console.WriteLine("Проверь правильность введенный данных");
            }


            MainTrainFormula(formulasFromTopics);
            WorkOnMistakes();
            //PrintWrongAnswerStatistics("algebra", 5);
            Console.WriteLine("Напишите одну тему, которая вас интересует, чтобы вывести по ней статистику");
            PrintThemes();
            string userInputForStat = Console.ReadLine().Trim().ToLower();
            Console.WriteLine("Введите какое количество последних тренировок, которое вас интересует для вывода статистика по теме");
            int countForStat = Convert.ToInt32(Console.ReadLine());
            PrintWrongAnswerStatistics(userInputForStat, countForStat);





        }
        private void StartForTrainig()
        {
            Console.WriteLine("Привет! Это тренажер для заучивания формул");
            Console.WriteLine("Напишите темы через запятую, которые вас интересуют");
            PrintThemes();

        }
        private void WorkOnMistakes()
        {
            Console.WriteLine();
            if (wrongAnswers.Count() != 0)
            {
                Console.WriteLine("А сейчас будет работа над ошибками");
                Thread.Sleep(500);
                Console.WriteLine("Загрузка...");
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Работа над ошибками:");
                Console.WriteLine();
            }
            while (wrongAnswers.Count() != 0)
            {

                foreach (var formulaListWithTopic in wrongAnswers)
                {

                    List<Formula> formulaList = formulaListWithTopic.Value.ToList();
                    foreach (Formula formula in formulaList)
                    {
                        PrintFormulaForWorkOnMistakes(formulaListWithTopic.Key, formula);
                    }

                }
            }
            if (wrongAnswers.Count() == 0)
            {
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Работа над ошибками закончена!");
                //Console.WriteLine("Тренировка окончена, ошибок нет!");
            }
        }
        private void PrintFormulaForWorkOnMistakes(string theme, Formula formula)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("тема: " + theme);
            Console.WriteLine("Название формулы: " + formula.name);
            Console.WriteLine("Нажми на любую кнопку, чтоб увидеть правильный ответ");

            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Правильный ответ: " + formula.correct_answer);
            Console.WriteLine("Правильно ли ты ответил? (Y/N)");
            while (true)
            {

                string userInput = Console.ReadLine().Trim().ToUpper();

                if (userInput.ToUpper() == "Y" || userInput.ToUpper() == "N")
                {
                    if (userInput == "Y")
                    {
                        wrongAnswers[theme].Remove(formula);
                        if (wrongAnswers[theme].Count == 0)
                        {
                            wrongAnswers.Remove(theme);
                        }
                    }
                    break;
                }

                Console.WriteLine("Некорректный ввод! Пожалуйста, введите Y или N.");
                Console.WriteLine("Введите Y или N:");
            }

        }
        private void PrintFormulaForTraining(string theme, Formula formula)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("тема: " + theme);
            Console.WriteLine("Название формулы: " + formula.name);
            Console.WriteLine("Нажми на любую кнопку, чтоб увидеть правильный ответ");

            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Правильный ответ: " + formula.correct_answer);
            Console.WriteLine("Правильно ли ты ответил? (Y/N)");
            while (true)
            {

                string userInput1 = Console.ReadLine().Trim().ToUpper();

                if (userInput1.ToUpper() == "Y" || userInput1.ToUpper() == "N")
                {

                    if (userInput1 == "Y")
                    {

                        UpdateCorrectAnswerStats(theme, formula);
                    }
                    if (userInput1 == "N")
                    {

                        UpdateIncorrectAnswerStats(theme, formula);
                    }


                    break;
                }

                Console.WriteLine("Некорректный ввод! Пожалуйста, введите Y или N.");
                Console.WriteLine("Введите Y или N:");
            }
        }
        private void MainTrainFormula(Dictionary<string, Formula> formulasFromTopics)
        {

            foreach (var formula in formulasFromTopics)
            {
                PrintFormulaForTraining(formula.Key, formula.Value);

            }

        }

        private Dictionary<string, Formula> FormulasFromTopics(List<string> topics)
        {

            Dictionary<string, Formula> formulasFromTopics = new Dictionary<string, Formula>();
            foreach (var item in formulas)
            {
                foreach (var item2 in topics)
                {
                    if (item.Key == item2)
                    {
                        foreach (var item3 in item.Value)
                        {

                            formulasFromTopics.Add(item2, item3);
                        }

                    }
                }

            }
            return formulasFromTopics;
        }
        private List<string> SelectTopics()
        {
            List<string> topics = new List<string>();
            string userInput = Console.ReadLine().Trim().ToLower();
            string[] topicsFromInput = userInput.Split(',');

            foreach (string topic in topicsFromInput)
            {
                if (formulas.ContainsKey(topic))
                {

                    topics.Add(topic);
                }
            }

            return topics;

        }
        private void UpdateIncorrectAnswerStats(string theme, Formula formula)
        {

            if (wrongAnswers.ContainsKey(theme))
            {
                wrongAnswers[theme].Add(formula);
            }
            else
            {
                wrongAnswers.Add(theme, new List<Formula> { formula });
            }
        }
        private void UpdateCorrectAnswerStats(string theme, Formula formula)
        {

            if (correctAnswers.ContainsKey(theme))
            {
                correctAnswers[theme].Add(formula);

            }
            else
            {
                correctAnswers.Add(theme, new List<Formula> { formula });
            }
        }
        private void LoadFormulas(string path)
        {

            using (StreamReader sr = File.OpenText(path))
            {
                string line = sr.ReadLine();

                while (line != null)
                {


                    if (line != null)
                    {
                        string[] formulaWithTheme = Regex.Split(line, ",");

                        string theme = formulaWithTheme[0].ToLower();
                        Formula formula = new Formula(formulaWithTheme[1], formulaWithTheme[2]);

                        if (formulas.ContainsKey(theme))
                        {
                            formulas[theme].Add(formula);
                        }
                        else
                        {
                            formulas.Add(theme, new List<Formula> { formula });
                        }



                    }
                    line = sr.ReadLine();

                }
            }

        }
        
        private void PrintThemes()
        {
            int c = 1;
            Console.WriteLine();

            foreach (var item in formulas.Keys)
            {
                Console.WriteLine($"{c}.{item}");
                c++;
            }
            Console.WriteLine();
        }
        public void PrintWrongAnswerStatistics(string topic, int numTrainings)
        {
            if (wrongAnswers.ContainsKey(topic))
            {
                int totalTrainings = Math.Min(numTrainings, wrongAnswers[topic].Count);
                Console.WriteLine($"Статистика неправильных ответов по теме '{topic}' (основанная на {totalTrainings} тренировках):");
                Console.WriteLine($"Формула\t\tКоличество неправильных ответов\tПроцент");
                foreach (var formulaEntry in formulas[topic])
                {
                    string formulaName = formulaEntry.name;
                    int wrongCount = wrongAnswers.ContainsKey(formulaName) ? wrongAnswers[formulaName].Count : 0;
                    double percentage = (double)wrongCount / totalTrainings * 100;
                    Console.WriteLine($"{formulaName}\t\t{wrongCount}\t\t{percentage}%");
                }
            }
            else
            {
                Console.WriteLine($"Количество неправильных ответов по теме: '{topic}'.");
            }
        }

    }
}

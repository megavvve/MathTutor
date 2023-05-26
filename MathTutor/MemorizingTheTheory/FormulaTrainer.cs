using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathTutor
{
    public class FormulaTrainer
    {

        private Dictionary<string, List<Formula>> formulas = new Dictionary<string, List<Formula>>();
        private Dictionary<string, List<Formula>> wrongAnswers = new Dictionary<string, List<Formula>>();
        private Dictionary<string, List<Formula>> correctAnswers = new Dictionary<string, List<Formula>>();
        public FormulaTrainer() { }
        public void Training()
        {
            LoadFormulas(@"./input-files/formulas.txt");

            StartForTrainig();
            while (true)
            {
                List<string> topics = SelectTopics();

                Dictionary<string, List<Formula>> formulasFromTopics = FormulasFromTopics(topics);
                if (formulasFromTopics.Count == 0)
                {
                    Console.WriteLine("Проверь правильность введенный данных");
                }


                MainTrainFormula(formulasFromTopics);

                AddWrongAnswerInDB(@"input-files/data_base_for_statistic.txt");
                WorkOnMistakes();//работа над ошибками
                Console.WriteLine("Напишите одну тему, которая вас интересует, чтобы вывести по ней статистику");
                PrintThemes();//вывод тем
                string userInputForStat = Console.ReadLine().Trim().ToLower();
                Console.WriteLine("Введите какое количество последних тренировок, которое вас интересует для вывода статистика по теме");
                int countForStat = Convert.ToInt32(Console.ReadLine());
                PrintWrongAnswerStatistics(userInputForStat, countForStat, @"input-files/data_base_for_statistic.txt");
                Thread.Sleep(2000);
                Console.WriteLine("Напишите Y,если хотите продолжить тренировку");
                Console.WriteLine("Если хотите завершить, нажмите любую кнопку кроме Y");
                string userInput = Console.ReadLine().Trim().ToUpper();
                if (userInput == "Y")
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Напишите темы через запятую, которые вас интересуют");
                    PrintThemes();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("тренировка завершена!");
                    break;
                }

            }
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
        private void MainTrainFormula(Dictionary<string, List<Formula>> formulasFromTopics)
        {

            foreach (var formula in formulasFromTopics)
            {
                foreach (var item in formula.Value)
                {
                    PrintFormulaForTraining(formula.Key, item);
                }


            }

        }

        private Dictionary<string, List<Formula>> FormulasFromTopics(List<string> topics)
        {

            Dictionary<string, List<Formula>> formulasFromTopics = new Dictionary<string, List<Formula>>();
            foreach (var item in formulas)
            {
                foreach (var item2 in topics)
                {
                    if (item.Key == item2)
                    {
                        foreach (var item3 in item.Value)
                        {
                            if (formulasFromTopics.ContainsKey(item2))
                            {
                                formulasFromTopics[item2].Add(item3);
                            }
                            else
                            {
                                formulasFromTopics.Add(item2, new List<Formula> { { item3 } });
                            }

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
                string line = sr.ReadLine().Trim();

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
        private void AddWrongAnswerInDB(string path)
        {
            int lastTrain = 0;
            if (new FileInfo(path).Length == 0)
            {
                lastTrain = 0;
            }
            else
            {
                lastTrain = int.Parse(File.ReadAllLines(path).Last().Split(',')[0]);
            }

            using (var fs = new FileStream(path, FileMode.Append))
            using (var sw = new StreamWriter(fs))
            {
                foreach (var item in wrongAnswers.Keys)
                {
                    string str = $"{lastTrain + 1},{item},{wrongAnswers[item].Count}";
                    sw.WriteLine(str);
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
        public void PrintWrongAnswerStatistics(string topic, int numTrainings, string path)
        {

            List<string> data_base = File.ReadAllLines(path).ToList();
            List<string> take_last_from_DB = data_base.Where(x => x.Split(',')[1] == topic).TakeLast(numTrainings).ToList();
            Dictionary<string, Dictionary<string, int>> dict_from_DB = new Dictionary<string, Dictionary<string, int>>();
            foreach (var string_from_DB in take_last_from_DB)
            {
                if (string_from_DB != null)
                {
                    string numTrain = string_from_DB.Split(',')[0];
                    string theme = string_from_DB.Split(',')[1];
                    int countWrongAnswers = int.Parse(string_from_DB.Split(',')[2]);
                    if (dict_from_DB.ContainsKey(numTrain))
                    {
                        if (dict_from_DB[numTrain].ContainsKey(theme))
                        {
                            dict_from_DB[numTrain].Remove(theme);
                            dict_from_DB[numTrain].Add(theme, countWrongAnswers);
                        }
                        else
                        {
                            dict_from_DB[numTrain].Add(theme, countWrongAnswers);
                        }
                    }
                    else
                    {

                        dict_from_DB.Add(numTrain, new Dictionary<string, int>() { { theme, countWrongAnswers } });

                    }

                }

            }
            Console.WriteLine($"Статистика неправильных ответов по теме '{topic}' (основанная на {numTrainings} тренировках):");
            Console.WriteLine();
            Console.WriteLine($"Номер тренировки\t\tКоличество неправильных ответов");
            foreach (var numTrain in dict_from_DB.Keys)
            {
                var themeWithCount = dict_from_DB[numTrain];
                foreach (var themeInDict in themeWithCount.Keys)
                {
                    if (themeInDict == topic)
                    {

                        int wrongCount = themeWithCount[themeInDict];
                        Console.WriteLine($"Тренировка {numTrain}\t\t{wrongCount}");
                    }
                }

            }
            Console.WriteLine();
            Console.WriteLine();

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathTutor.MemorizingTheTheory
{
    public class TheoremTrainer 
    {
        private Dictionary<string, List<Theorem>> theoremes = new Dictionary<string, List<Theorem>>();
        private Dictionary<string, List<Theorem>> wrongTheorem = new Dictionary<string, List<Theorem>>();
        private Dictionary<string, List<Theorem>> correctTheorem = new Dictionary<string, List<Theorem>>();

        public TheoremTrainer() { }
        public void Training()
        {
            LoadTheoremes(@"./input-files/theorem.txt");

            StartForTrainig();
            while (true)
            {
                List<string> topics = SelectTopics();

                Dictionary<string, List<Theorem>> theoremesFromTopics = TheoremesFromTopics(topics);
                if (theoremesFromTopics.Count == 0)
                {
                    Console.WriteLine("Проверь правильность введенный данных");
                }


                MainTrainFormula(theoremesFromTopics);

                AddWrongAnswerInDB(@"input-files/db_Theorem.txt");
                WorkOnMistakesTheorem();//работа над ошибками
                Console.WriteLine("Напишите одну тему, которая вас интересует, чтобы вывести по ней статистику");
                PrintThemes();//вывод тем
                string userInputForStat = Console.ReadLine().Trim().ToLower();
                Console.WriteLine("Введите какое количество последних тренировок, которое вас интересует для вывода статистика по теме");
                int countForStat = Convert.ToInt32(Console.ReadLine());
                PrintWrongAnswerStatistics(userInputForStat, countForStat, @"input-files/db_Theorem.txt");
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
            Console.WriteLine("Привет! Это тренажер для заучивания формул/теорем");
            Console.WriteLine("Напишите темы через запятую, которые вас интересуют");
            PrintThemes();

        }

        public void WorkOnMistakesTheorem()
        {
            Console.WriteLine();
            if (wrongTheorem.Count() != 0)
            {
                Console.WriteLine("А сейчас будет работа над ошибками");
                Thread.Sleep(500);
                Console.WriteLine("Загрузка...");
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Работа над ошибками:");
                Console.WriteLine();
            }
            while (wrongTheorem.Count() != 0)
            {

                foreach (var theoremListWithTopic in wrongTheorem)
                {

                    List<Theorem> theoremList = theoremListWithTopic.Value.ToList();
                    foreach (Theorem theorem in theoremList)
                    {
                        PrintTheoremForWorkOnMistakes(theoremListWithTopic.Key, theorem);
                    }

                }
            }
            if (wrongTheorem.Count() == 0)
            {
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Работа над ошибками закончена!");

            }
        }
        public void PrintTheoremForWorkOnMistakes(string theme, Theorem theorem)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("тема: " + theme);
            Console.WriteLine("Теорема: " + theorem.condition);
            Console.WriteLine("Нажми на любую кнопку, чтоб увидеть правильный ответ");

            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Правильный ответ: " + theorem.proof);
            Console.WriteLine("Правильно ли ты ответил? (Y/N)");
            while (true)
            {

                string userInput = Console.ReadLine().Trim().ToUpper();

                if (userInput.ToUpper() == "Y" || userInput.ToUpper() == "N")
                {
                    if (userInput == "Y")
                    {
                        wrongTheorem[theme].Remove(theorem);
                        if (wrongTheorem[theme].Count == 0)
                        {
                            wrongTheorem.Remove(theme);
                        }
                    }
                    break;
                }

                Console.WriteLine("Некорректный ввод! Пожалуйста, введите Y или N.");
                Console.WriteLine("Введите Y или N:");
            }

        }


        public void PrintTheoremForTraining(string theme, Theorem theorem)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("тема: " + theme);
            Console.WriteLine("Теорема " + theorem.condition);
            Console.WriteLine("Нажми на любую кнопку, чтоб увидеть правильный ответ");

            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Правильное заключение: " + theorem.conclusion);

            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Правильная теорема: " + theorem.proof);
            Console.WriteLine("Правильно ли ты ответил? (Y/N)");
            while (true)
            {

                string userInput1 = Console.ReadLine().Trim().ToUpper();

                if (userInput1.ToUpper() == "Y" || userInput1.ToUpper() == "N")
                {

                    if (userInput1 == "Y")
                    {

                        UpdateCorrectAnswerStats(theme, theorem);
                    }
                    if (userInput1 == "N")
                    {

                        UpdateIncorrectAnswerStats(theme, theorem);
                    }


                    break;
                }

                Console.WriteLine("Некорректный ввод! Пожалуйста, введите Y или N.");
                Console.WriteLine("Введите Y или N:");
            }
        }

        public void MainTrainFormula(Dictionary<string, List<Theorem>> theoremsFromTopics)
        {

            foreach (var formula in theoremsFromTopics)
            {
                foreach (var item in formula.Value)
                {
                    PrintTheoremForTraining(formula.Key, item);
                }
            }

        }

        public Dictionary<string, List<Theorem>> TheoremesFromTopics(List<string> topics)
        {

            Dictionary<string, List<Theorem>> theoremesFromTopics = new Dictionary<string, List<Theorem>>();
            foreach (var item in theoremes)
            {
                foreach (var item2 in topics)
                {
                    if (item.Key == item2)
                    {
                        foreach (var item3 in item.Value)
                        {
                            if (theoremesFromTopics.ContainsKey(item2))
                            {
                                theoremesFromTopics[item2].Add(item3);
                            }
                            else
                            {
                                theoremesFromTopics.Add(item2, new List<Theorem> { { item3 } });
                            }

                        }

                    }
                }

            }
            return theoremesFromTopics;
        }
        public List<string> SelectTopics()
        {
            List<string> topics = new List<string>();
            string userInput = Console.ReadLine().Trim().ToLower();
            string[] topicsFromInput = userInput.Split('|');

            foreach (string topic in topicsFromInput)
            {
                if (theoremes.ContainsKey(topic))
                {

                    topics.Add(topic);
                }
            }

            return topics;

        }
        public void UpdateIncorrectAnswerStats(string theme, Theorem theorem)
        {

            if (wrongTheorem.ContainsKey(theme))
            {
                wrongTheorem[theme].Add(theorem);
            }
            else
            {
                wrongTheorem.Add(theme, new List<Theorem> { theorem });
            }
        }
        public void UpdateCorrectAnswerStats(string theme, Theorem theorem)
        {

            if (correctTheorem.ContainsKey(theme))
            {
                correctTheorem[theme].Add(theorem);

            }
            else
            {
                correctTheorem.Add(theme, new List<Theorem> { theorem });
            }
        }
        public void LoadTheoremes(string path)
        {

            using (StreamReader sr = File.OpenText(path))
            {
                string line = sr.ReadLine().Trim();

                while (line != null)
                {


                    if (line != null)
                    {
                        string[] theoremWithTheme = line.Split("|");
                        Theorem theorem = new Theorem(theoremWithTheme[1], theoremWithTheme[2], theoremWithTheme[3]);

                        theoremes.Add(theoremWithTheme[0], new List<Theorem> { theorem });
                    }
                    line = sr.ReadLine();

                }
            }

        }
        public void AddWrongAnswerInDB(string path)
        {
            int lastTrain = 0;
            if (new FileInfo(path).Length == 0)
            {
                lastTrain = 0;
            }
            else
            {
                lastTrain = int.Parse(File.ReadAllLines(path).Last().Split('|')[0]);
            }

            using (var fs = new FileStream(path, FileMode.Append))
            using (var sw = new StreamWriter(fs))
            {
                foreach (var item in wrongTheorem.Keys)
                {
                    string str = $"{lastTrain + 1},{item},{wrongTheorem[item].Count}";
                    sw.WriteLine(str);
                }
            }


        }
        public void PrintThemes()
        {
            int c = 1;
            Console.WriteLine();

            foreach (var item in theoremes.Keys)
            {
                Console.WriteLine($"{c}.{item}");
                c++;
            }
            Console.WriteLine();
        }

        public void PrintWrongAnswerStatistics(string topic, int numTrainings, string path)
        {

            List<string> data_base = File.ReadAllLines(path).ToList();
            List<string> take_last_from_DB = data_base.Where(x => x.Split('|')[1] == topic).TakeLast(numTrainings).ToList();
            Dictionary<string, Dictionary<string, int>> dict_from_DB = new Dictionary<string, Dictionary<string, int>>();
            foreach (var string_from_DB in take_last_from_DB)
            {
                if (string_from_DB != null)
                {
                    string numTrain = string_from_DB.Split(',')[0];
                    string theme = string_from_DB.Split(',')[1];
                    int countWrongAnswers = int.Parse(string_from_DB.Split('|')[2]);
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

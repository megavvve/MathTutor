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
        private List<Theorem> theorems;
        private List<string> formulas;
        private Dictionary<string, int> incorrectAnswers;

        public TheoremTrainer()
        {
            theorems = new List<Theorem>();
            formulas = new List<string>();
            incorrectAnswers = new Dictionary<string, int>();
        }

        public void LoadTheoremsFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');

                if (parts.Length == 4)
                {
                    Theorem theorem = new Theorem
                    {
                        Topic = parts[0],
                        Condition = parts[1],
                        Conclusion = parts[2],
                        Proof = parts[3]
                    };

                    theorems.Add(theorem);
                }
                else if (parts.Length == 1)
                {
                    formulas.Add(parts[0]);
                }
            }
        }

        public void AddFormula(string formula)
        {
            formulas.Add(formula);
        }

        public void AddTheorem(string topic, string condition, string conclusion, string proof)
        {
            Theorem theorem = new Theorem
            {
                Topic = topic,
                Condition = condition,
                Conclusion = conclusion,
                Proof = proof
            };

            theorems.Add(theorem);
        }

        public void Train()
        {
            LoadTheoremsFromFile("./input-files/theorem.txt");
            Console.WriteLine("Выберите тип тренировки:");
            Console.WriteLine("2. Тренировка по теоремам");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 2:
                    TrainTheorems();
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }

        

        private void TrainTheorems()
        {
            if (theorems.Count == 0)
            {
                Console.WriteLine("Нет доступных теорем для тренировки.");
                return;
            }

            Console.WriteLine("Начало тренировки по теоремам.");
            Console.WriteLine("Введите количество примеров для тренировки:");
            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Theorem theorem = GetRandomTheorem();
                Console.WriteLine($"Пример {i + 1}:");
                Console.WriteLine("Введите условие теоремы:");
                string condition = Console.ReadLine();

                if (condition != theorem.Condition)
                {
                    Console.WriteLine("Неправильное условие.");
                    AddIncorrectAnswer(theorem.Topic);
                    continue;
                }

                Console.WriteLine("Введите заключение теоремы:");
                string conclusion = Console.ReadLine();

                if (conclusion != theorem.Conclusion)
                {
                    Console.WriteLine("Неправильное заключение.");
                    AddIncorrectAnswer(theorem.Topic);
                    continue;
                }

                Console.WriteLine("Введите доказательство теоремы:");
                string proof = Console.ReadLine();

                if (proof != theorem.Proof)
                {
                    Console.WriteLine("Неправильное доказательство.");
                    AddIncorrectAnswer(theorem.Topic);
                    continue;
                }

                Console.WriteLine("Правильный ответ!");
            }

            Console.WriteLine("Тренировка по теоремам завершена.");
        }


        private Theorem GetRandomTheorem()
        {
            return theorems[new Random().Next(0, theorems.Count)];
        }

        private void AddIncorrectAnswer(string topic)
        {
            if (incorrectAnswers.ContainsKey(topic))
            {
                incorrectAnswers[topic]++;
            }
            else
            {
                incorrectAnswers.Add(topic, 1);
            }
        }

        public void PrintIncorrectAnswersStatistics(int numTrainings)
        {
            Console.WriteLine($"Статистика неправильных ответов за последние {numTrainings} тренировок:");

            var sortedIncorrectAnswers = incorrectAnswers.OrderByDescending(x => x.Value);

            foreach (var entry in sortedIncorrectAnswers)
            {
                if (entry.Value >= numTrainings)
                {
                    Console.WriteLine($"Тема: {entry.Key}, Количество неправильных ответов: {entry.Value}");
                }
            }
        }

        public void PrintShortestFormula()
        {
            if (formulas.Count == 0)
            {
                Console.WriteLine("Нет доступных формул.");
                return;
            }

            string shortestFormula = formulas.OrderBy(x => x.Length).First();
            Console.WriteLine($"Формула с наименьшей записью: {shortestFormula}");
        }

        public void PrintLongestProof()
        {
            if (theorems.Count == 0)
            {
                Console.WriteLine("Нет доступных теорем.");
                return;
            }

            Theorem longestProofTheorem = theorems.OrderByDescending(x => x.Proof.Length).First();
            Console.WriteLine($"Теорема с самым длинным доказательством: {longestProofTheorem.Topic}");
            Console.WriteLine($"Доказательство: {longestProofTheorem.Proof}");
        }

        public void PrintAllTheorems()
        {
            foreach (Theorem theorem in theorems)
            {
                Console.WriteLine($"Тема: {theorem.Topic}");
                Console.WriteLine($"Условие: {theorem.Condition}");
                Console.WriteLine($"Заключение: {theorem.Conclusion}");
                Console.WriteLine($"Доказательство: {theorem.Proof}");
                Console.WriteLine();
            }
        }
    }


}

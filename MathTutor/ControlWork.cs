using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathTutor
{
    public class ControlWork
    {
        public List<string> Questions { get; }
        public List<string> Hints { get; }
        public List<double> Answers {  get; }


        public ControlWork() 
        { 
            Questions = new List<string>();
            Hints = new List<string>();
            Answers = new List<double>();
        }

        public void ReadFiles()
        {
            string path = "./Tasks/Tasks.txt";
            string reader = File.ReadAllText(path);
            foreach (Match line in Regex.Matches(reader, @"(Задача №)(\d+): (.+\?)(.+\!)(\d+(?:\.\d+)?)"))
            {
                Questions.Add(line.Groups[3].Value);
                Hints.Add(line.Groups[4].Value);
                Answers.Add(double.Parse(line.Groups[5].Value));
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor
{
    public class Formula
    {
        public string name;
        public string correct_answer;

        public Formula(string name, string correct_answer)
        {
            this.name = name;
            this.correct_answer = correct_answer;
        }
    }
}

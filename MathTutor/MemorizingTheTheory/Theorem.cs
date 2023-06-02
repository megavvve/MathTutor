using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor
{
    public class Theorem
    {
        public string condition;
        public string conclusion;
        public string proof;

        public Theorem(string Condition, string Conclusion, string Proof) 
        { 
            condition = Condition;
            conclusion = Conclusion;
            proof = Proof;
        }   
    }
}

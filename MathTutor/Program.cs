using MathTutor.MemorizingTheTheory;
using System;

namespace MathTutor 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FormulaTrainer trainer = new FormulaTrainer();
            //trainer.Training();

            TheoremTrainer theoremTrainer = new TheoremTrainer();
            theoremTrainer.Train();
        
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Quiz
    {
        public int Score;
        public IList<Question> Questions;

        public Quiz(Question[] questions)
        {
            Score = 0;
            Questions = questions.ToList();
        }

        public Question GetNextQuestion()
        {
            var nextQuestion = Questions[Random.Range(0, Questions.Count)];
            Questions.Remove(nextQuestion);
            return nextQuestion;
        }
        
        public bool CheckIfCorrect(Question question, Choice choice)
        {
            if (choice.IsTrue)
            {
                Score++;
            }
            return choice.IsTrue;
        }
    }
}

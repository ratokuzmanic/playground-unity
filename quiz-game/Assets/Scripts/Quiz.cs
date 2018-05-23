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
            Questions = questions
                .ConcatSeed()
                .GetValid()
                .RandomizeChoicesOrder()
                .ToList();
        }

        public IMaybe<Question> GetNextQuestion()
        {
            if (!Questions.Any())
            {
                return None<Question>.Exists();
            }
            var nextQuestion = Questions[Random.Range(0, Questions.Count)];
            Questions.Remove(nextQuestion);
            return Some<Question>.Exists(nextQuestion);
        }
        
        public bool SubmitAnswer(Question question, string answer)
        {
            var selectedChoice = question
                .Choices
                .First(choice => choice.Statement == answer);

            if (selectedChoice.IsTrue)
            {
                Score++;
            }
            return selectedChoice.IsTrue;
        }
    }
}

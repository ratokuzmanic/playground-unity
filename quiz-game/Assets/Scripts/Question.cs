using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Question {
        public string Text;

        [SerializeField]
        public Choice[] Choices;

        public Question
        (
            string   text, 
            string[] choiceStatements, 
            int      correctChoiceLocation
        )
        {
            Text = text;
            Choices = choiceStatements.Select(statement => new Choice(statement)).ToArray();
            Choices[correctChoiceLocation - 1].IsTrue = true;
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Text))
            {
                return false;
            }
            if
            (
                Choices.Length == Constants.NumberOfChoices
                && Choices.Any(choice => choice.IsTrue)
                && Choices.Count(choice => choice.IsTrue) <= Constants.MaximumCorrectChoicesInSingleQuestion
                && Choices.All(choice => choice.IsValid())
                && Choices.GroupBy(choice => choice.Statement).Count() == Choices.Length
            )
            {
                return true;
            }
            return false;
        }

        public Choice[] HalfTheChoices()
        {
            var random = new System.Random();
            var correctChoice = Choices.Where(choice => choice.IsTrue);

            return Choices
                .Where(choice => !choice.IsTrue)
                .OrderBy(_ => random.Next())
                .Take(1)
                .Concat(correctChoice)
                .ToArray();
        }
    }

    public static class QuestionExtensions
    {
        public static Question[] ConcatSeed(this Question[] questions)
        {
            return questions.Concat(new[]
            {
                new Question("What is the capital city of Croatia?",           new [] { "Split", "Zagreb", "Rijeka", "Osijek" }, 2),
                new Question("How old is Jon Bon Jovi?",                       new [] { "52", "61", "54", "56"                }, 4),
                new Question("How many albums did twenty one pilots publish?", new [] { "1", "3", "4", "5"                    }, 3),
                new Question("How good is Life is Strange?",                   new [] { "Good", "Hella good", "Meh", "Bad"    }, 2),
                new Question("This is an example of a corrupt question",       new [] { "Ok", "What?"                         }, 1),
                new Question("This is also a corrupt question",                new [] { "1", "3", "1", "5"                    }, 1),
            }).ToArray();
        }

        public static Question[] GetValid(this Question[] questions)
        {
            return questions.Where(question => question.IsValid()).ToArray();
        }

        public static Question[] RandomizeChoicesOrder(this Question[] questions)
        {
            var random = new System.Random();
            foreach (var question in questions)
            {
                question.Choices = question.Choices.OrderBy(_ => random.Next()).ToArray();
            }
            return questions;
        }
    }
}

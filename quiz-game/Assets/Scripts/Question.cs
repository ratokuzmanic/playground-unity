﻿using System;
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
            if 
            (
                string.IsNullOrEmpty(Text)               ||
                Choices.Length != 4                      ||
                !Choices.Any(choice => choice.IsTrue)    ||
                !Choices.All(choice => choice.IsValid())
            )
            {
                return false;
            }
            return true;
        }
    }

    public static class QuestionExtensions
    {
        public static Question[] ConcatSeed(this Question[] questions)
        {
            return questions.Concat(new[]
            {
                new Question("What is the capital city of Croatia", new [] { "Split", "Zagreb", "Rijeka", "Osijek" }, 2),
                new Question("What is the capital city of Croatia", new [] { "Split", "Zagreb", "Rijeka", "Osijek" }, 2)
            }).ToArray();
        }

        public static Question[] GetValid(this Question[] questions)
        {
            return questions.Where(question => question.IsValid()).ToArray();
        }
    }
}
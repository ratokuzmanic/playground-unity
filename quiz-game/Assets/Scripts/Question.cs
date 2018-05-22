using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Choice
{
    public string Statement;
    public bool IsTrue;

    public Choice(string statement)
    {
        Statement = statement;
        IsTrue = false;
    }
}

[Serializable]
public class Question {
	public string Text;

    [SerializeField]
	public Choice[] Choices;

    public Question(string text, string[] choices, int correctChoiceLocation)
    {
        Text = text;
        Choices = new Choice[choices.Length];
        for (var i = 0; i < choices.Length; i++)
        {
            Choices[i] = new Choice(choices[i]);
        }
        Choices[correctChoiceLocation - 1].IsTrue = true;
    }

    public static Question[] Seed()
    {
        return new []
        {
            new Question("What is the capital city of Croatia", new [] { "Split", "Zagreb", "Rijeka", "Osijek" }, 2)
        };
    }
}

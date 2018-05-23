﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour {
        public Question[] Questions;

        private ControlsHook _controls;
        private Quiz _quiz;
        private Question _currentQuestion;

        void Start ()
        {
            _controls = GetComponent<ControlsHook>();

            Questions = Questions.ConcatSeed().GetValid();

            _quiz = new Quiz(Questions);
            SetNewQuestion();
        }

        void Update () {
	
        }

        public void SetNewQuestion()
        {
            _currentQuestion = _quiz.GetNextQuestion();

            _controls.Question.text = _currentQuestion.Text;
            for (var i = 0; i < _currentQuestion.Choices.Length; i++)
            {
                _controls.ChoiceStatements[i].text = _currentQuestion.Choices[i].Statement;
            }
        }

        public void SubmitAnswer(Text answer)
        {
            var isCorrect = _quiz.SubmitAnswer(_currentQuestion, answer.text);
            _controls.Score.text = "Bodovi: " + _quiz.Score;

            SetDisappearingMessage(isCorrect, isCorrect ? "Correct!" : "Sorry :(");
            StartCoroutine(DelayAction(SetNewQuestion));
        }

        private void SetDisappearingMessage(bool isPositive, string message)
        {
            _controls.Choices.SetActive(false);

            _controls.Message.color = isPositive ? new Color(0, 1, 0) : new Color(1, 0, 0);
            _controls.Message.text = message;

            StartCoroutine(DelayAction(RemoveMessage));
        }

        private void RemoveMessage()
        {
            _controls.Message.text = "";
            _controls.Choices.SetActive(true);
        }

        private IEnumerator DelayAction(Action action)
        {
            yield return new WaitForSeconds(Constants.WaitTimeForDelayedAction);
            action();
        }
    }
}

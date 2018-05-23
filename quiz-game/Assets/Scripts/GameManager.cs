using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour {
        public Question[] Questions;

        private ControlsHook _controls;

        private Quiz _quiz;
        private Question _currentQuestion;
        private TimeSpan _playingTime;
        private bool _timerIsActive;

        private void Start()
        {
            _controls = GetComponent<ControlsHook>();
            _quiz = new Quiz(Questions);
            SetNewQuestion();
        }

        private void Update()
        {
            StartCoroutine(IncreaseTimer());
        }

        private void SetNewQuestion()
        {

            var maybeQuestion = _quiz.GetNextQuestion();

            maybeQuestion.Case(
                some: question =>
                {
                    _timerIsActive = true;
                    _currentQuestion = question;
                    _controls.Question.text = _currentQuestion.Text;
                    for (var i = 0; i < _currentQuestion.Choices.Length; i++)
                    {
                        _controls.ChoiceButtons[i].Statement.text = _currentQuestion.Choices[i].Statement;
                    }
                },
                none: () => SetMessage(false, "No more questions")
            );
        }

        public void SubmitAnswer(Text answer)
        {
            _timerIsActive = false;

            var isCorrect = _quiz.SubmitAnswer(_currentQuestion, answer.text);
            _controls.Score.text = "Score: " + _quiz.Score;

            ResetButtons();
            SetDisappearingMessage(isCorrect, isCorrect ? "Correct!" : "Sorry :(");
            StartCoroutine(DelayAction(SetNewQuestion));
        }

        public void PlayJoker()
        {
            _controls.Joker.SetActive(false);

            var newChoices = _currentQuestion.HalfTheChoices();
            foreach (var choiceButton in _controls.ChoiceButtons)
            {
                if (newChoices.All(choice => choice.Statement != choiceButton.Statement.text))
                {
                    choiceButton.Button.SetActive(false);
                }
            }
        }

        private void ResetButtons()
        {
            foreach (var choiceButton in _controls.ChoiceButtons)
            {
                choiceButton.Button.SetActive(true);
            }
        }

        private void SetMessage(bool isPositive, string message)
        {
            _controls.Choices.SetActive(false);

            _controls.Message.color = isPositive ? new Color(0, 1, 0) : new Color(1, 0, 0);
            _controls.Message.text = message;
        }

        private void SetDisappearingMessage(bool isPositive, string message)
        {
            SetMessage(isPositive, message);
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

        private IEnumerator IncreaseTimer()
        {
            yield return new WaitForSeconds(1);
            if (_timerIsActive)
            {
                _playingTime = _playingTime.Add(new TimeSpan(0, 0, 1));
                _controls.Timer.text = "Time: " + _playingTime;
            }
        }
    }
}

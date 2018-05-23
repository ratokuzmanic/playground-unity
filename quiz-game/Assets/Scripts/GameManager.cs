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
            for (int i = 0; i < _currentQuestion.Choices.Length; i++)
            {
                _controls.Choices[i].text = _currentQuestion.Choices[i].Statement;
            }
        }

        public void SubmitAnswer(Text answer)
        {
            var isCorrect = _quiz.CheckIfCorrect(
                _currentQuestion, 
                _currentQuestion.Choices.First(choice => choice.Statement == answer.text)
            );
            _controls.Score.text = "Bodovi: " + _quiz.Score;
        }
    }
}

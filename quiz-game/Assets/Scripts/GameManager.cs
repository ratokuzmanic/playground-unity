using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour {
        public Question[] Questions;

        private ControlsHook _controls;
        private Quiz _quiz;

        void Start ()
        {
            _controls = GetComponent<ControlsHook>();

            Questions = Questions.ConcatSeed().GetValid();

            _quiz = new Quiz(Questions);
            var next = _quiz.GetNextQuestion();
            _controls.Question.text = next.Text;
        }

        void Update () {
	
        }

        void SubmitAnswer()
        {
            
        }
    }
}

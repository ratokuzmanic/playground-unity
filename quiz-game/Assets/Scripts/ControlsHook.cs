using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ControlsHook : MonoBehaviour
    {
        public Text Question;

        [SerializeField]
        public Text[] ChoiceStatements;

        public GameObject Choices;

        public Text Score;

        public Text Timer;

        public Text Message;
    }
}

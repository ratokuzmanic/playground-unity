using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ControlsHook : MonoBehaviour
    {
        public Text Question;

        [SerializeField]
        public Text[] Choices;

        public Text Score;
    }
}

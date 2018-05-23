using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour {
        public Question[] Questions;
	
        void Start ()
        {
            Questions = Questions.ConcatSeed().GetValid();
        }

        void Update () {
	
        }
    }
}

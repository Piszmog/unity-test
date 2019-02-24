using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;


        Text text;


        void Awake ()
        {
            text = GetComponent <Text> ();
            score = 0;
        }


        void Update ()
        {
            text.text = "Score: " + score;
        }
    }
}

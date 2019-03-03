using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private static int _score;
        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
            _score = 0;
        }

        private void Update()
        {
            text.text = "Score: " + _score;
        }
    }
}
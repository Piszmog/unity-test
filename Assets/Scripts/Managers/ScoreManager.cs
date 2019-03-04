using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    /// <inheritdoc />
    /// <summary>
    /// Class the manages the user's score.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        private static int _score;
        private Text text;

        /// <summary>
        /// Increments the user's score.
        /// </summary>
        /// <param name="amount">The amount to increase the score by.</param>
        public static void IncreaseScore(int amount)
        {
            _score += amount;
        }

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
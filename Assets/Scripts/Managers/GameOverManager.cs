using Player;
using UnityEngine;

namespace Managers
{
    /// <inheritdoc />
    /// <summary>
    /// Class the manages the game over animation..
    /// </summary>
    public class GameOverManager : MonoBehaviour
    {
        private static readonly int GameOver = Animator.StringToHash("GameOver");

        public PlayerHealth playerHealth;

        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger(GameOver);
            }
        }
    }
}
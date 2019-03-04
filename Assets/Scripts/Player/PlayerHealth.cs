using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    /// <inheritdoc />
    /// <summary>
    /// Class the represents the player's health system.
    /// Whenever the player is damaged, the screen flashes a color to indicate to the user that their player has been damaged.
    /// If enough damaged has been incurred, the player will die.
    /// </summary>
    public class PlayerHealth : MonoBehaviour
    {
        private static readonly int Die = Animator.StringToHash("Die");

        public int startingHealth = 100;
        public int currentHealth;
        public Slider healthSlider;
        public Image damageImage;
        public AudioClip deathClip;
        public float flashSpeed = 5f;
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

        private Animator animator;
        private AudioSource playerAudio;
        private PlayerMovement playerMovement;
        private bool isDead;
        private bool isDamaged;
        private PlayerShooting playerShooting;

        /// <summary>
        /// Decreases the player's health by the amount provided. Once the player's health reaches zero or is below, the player dies.
        /// </summary>
        /// <param name="amount">The amount to decrease the health by.</param>
        public void TakeDamage(int amount)
        {
            isDamaged = true;
            currentHealth -= amount;
            healthSlider.value = currentHealth;
            playerAudio.Play();

            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }

        /// <summary>
        /// Reloads the scene.
        /// </summary>
        public void RestartLevel()
        {
            SceneManager.LoadScene(0);
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<PlayerMovement>();
            playerShooting = GetComponentInChildren<PlayerShooting>();
            currentHealth = startingHealth;
        }

        private void Update()
        {
            damageImage.color = isDamaged
                ? flashColour
                : Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            isDamaged = false;
        }

        private void Death()
        {
            isDead = true;
            playerShooting.DisableEffects();
            animator.SetTrigger(Die);
            playerAudio.clip = deathClip;
            playerAudio.Play();
            playerMovement.enabled = false;
            playerShooting.enabled = false;
        }
    }
}
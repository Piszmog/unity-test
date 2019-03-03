using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
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

        //PlayerShooting playerShooting;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<PlayerMovement>();
            //playerShooting = GetComponentInChildren <PlayerShooting> ();
            currentHealth = startingHealth;
        }

        private void Update()
        {
            damageImage.color = isDamaged
                ? flashColour
                : Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            isDamaged = false;
        }

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

        private void Death()
        {
            isDead = true;
            //playerShooting.DisableEffects ();
            animator.SetTrigger(Die);
            playerAudio.clip = deathClip;
            playerAudio.Play();
            playerMovement.enabled = false;
            //playerShooting.enabled = false;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(0);
        }
    }
}
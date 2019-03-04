using UnityEngine;

namespace Enemy
{
    /// <inheritdoc />
    /// <summary>
    /// Class that controls the enemy's health.
    /// If enough damage has been incurred, the enemy will die.
    /// For each enemy that dies, the user's score increases by the set amount.
    /// </summary>
    public class EnemyHealth : MonoBehaviour
    {
        private static readonly int Dead = Animator.StringToHash("Dead");
        public int startingHealth = 100;
        public int currentHealth;
        public float sinkSpeed = 2.5f;
        public AudioClip deathClip;
        public int scoreValue = 10;

        private Animator anim;
        private AudioSource enemyAudio;
        private ParticleSystem hitParticles;
        private CapsuleCollider capsuleCollider;
        private bool isDead;
        private bool isSinking;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyAudio = GetComponent<AudioSource>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            currentHealth = startingHealth;
        }

        private void Update()
        {
            if (isSinking)
            {
                transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
            }
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
            if (isDead)
            {
                return;
            }

            enemyAudio.Play();
            currentHealth -= amount;
            hitParticles.transform.position = hitPoint;
            hitParticles.Play();
            if (currentHealth <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            isDead = true;
            capsuleCollider.isTrigger = true;
            anim.SetTrigger(Dead);
            enemyAudio.clip = deathClip;
            enemyAudio.Play();
        }

        public void StartSinking()
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            isSinking = true;
            //ScoreManager.score += scoreValue;
            Destroy(gameObject, 2f);
        }
    }
}
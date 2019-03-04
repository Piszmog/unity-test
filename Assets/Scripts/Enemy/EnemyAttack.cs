using Player;
using UnityEngine;

namespace Enemy
{
    /// <inheritdoc />
    /// <summary>
    /// Class to control the enemy's attach behavior.
    /// </summary>
    public class EnemyAttack : MonoBehaviour
    {
        private static readonly int PlayerDead = Animator.StringToHash("PlayerDead");

        public float timeBetweenAttacks = 0.5f;
        public int attackDamage = 10;

        private Animator animator;
        private GameObject player;
        private PlayerHealth playerHealth;
        private bool isPlayerInRange;
        private float timer;
        private EnemyHealth enemyHealth;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                isPlayerInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                isPlayerInRange = false;
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks && isPlayerInRange && enemyHealth.currentHealth > 0)
            {
                Attack();
            }

            if (playerHealth.currentHealth <= 0)
            {
                animator.SetTrigger(PlayerDead);
            }
        }

        private void Attack()
        {
            timer = 0f;
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}
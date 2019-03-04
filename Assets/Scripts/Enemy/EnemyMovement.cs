using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    /// <inheritdoc />
    /// <summary>
    /// Class the controls the enemy's movement. An enemy will navigate to the player. If the player dies,
    /// the enemy will stop navigating to the player.
    /// </summary>
    public class EnemyMovement : MonoBehaviour
    {
        private PlayerHealth playerHealth;
        private EnemyHealth enemyHealth;
        private Transform player;
        private NavMeshAgent navigation;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
            navigation = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                navigation.SetDestination(player.position);
            }
            else
            {
                navigation.enabled = false;
            }
        }
    }
}
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        //PlayerHealth playerHealth;
        //EnemyHealth enemyHealth;
        private Transform player;
        private UnityEngine.AI.NavMeshAgent navigation;


        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            //playerHealth = player.GetComponent <PlayerHealth> ();
            //enemyHealth = GetComponent <EnemyHealth> ();
            navigation = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        private void Update()
        {
            //if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            //{
            navigation.SetDestination(player.position);
            //}
            //else
            //{
            //    nav.enabled = false;
            //}
        }
    }
}
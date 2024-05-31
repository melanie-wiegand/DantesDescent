using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private float detectionRange = 1000f; // Range within which the enemy can detect the player
    private float stoppingDistance = 5f; // Distance at which the enemy stops chasing
    public Animator animator; // Reference to the Animator component

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        
        // Get the NavMeshAgent component attached to this GameObject
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Ensure the Animator component is attached
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Check if the NavMeshAgent is on a NavMesh
        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not on a NavMesh. Ensure the enemy starts on a NavMesh.");
        }
        else
        {
            Debug.Log("NavMeshAgent is on the NavMesh.");
        }
    }

    void Update()
    {
        if (player != null && navMeshAgent.isOnNavMesh)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            Debug.Log("Distance to player: " + distanceToPlayer);

            if (distanceToPlayer <= detectionRange)
            {
                navMeshAgent.SetDestination(player.position);
                Debug.Log("Setting destination to player position: " + player.position);

                if (distanceToPlayer <= stoppingDistance)
                {
                    navMeshAgent.isStopped = true;
                    animator.SetBool("isChasing", false);
                    animator.SetBool("isAttacking", true);
                    Debug.Log("Reached stopping distance. Attacking.");
                }
                else
                {
                    navMeshAgent.isStopped = false;
                    animator.SetBool("isChasing", true);
                    animator.SetBool("isAttacking", false);
                    FacePlayer(); // Ensure enemy faces the player
                    Debug.Log("Chasing player.");
                }
            }
            else
            {
                navMeshAgent.isStopped = true;
                animator.SetBool("isChasing", false);
                animator.SetBool("isAttacking", false);
                Debug.Log("Player out of range. Stopping.");
            }
        }
    }

    // Make the enemy face the player
    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}

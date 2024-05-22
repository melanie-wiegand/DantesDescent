using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, Player;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 20;
    public float walkSpeed = 4f;
    public float defaultAngularSpeed = 360f;

    // Fleeing
    public float fleeDuration = 8f;
    public bool isFleeing = false;
    public float fleeSpeed = 8f;
    public float fleeingAngularSpeed = 600f;

    // States
    public float sightRange = 30;
    public bool playerInSightRange;

    private void Awake()
    {
        player = GameObject.Find("DanteObject").transform;
        agent = GetComponent<NavMeshAgent>();
        walkPointRange = 20;
        sightRange = 20;
    }

    private void Update()
    {
        // Check for sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);

        // Set states
        if(!playerInSightRange && !isFleeing) Patrolling();
        if(playerInSightRange && !isFleeing) ChasePlayer();
    }

    private void Patrolling()
    {
        // Set turning speed
        agent.angularSpeed = defaultAngularSpeed;

        // Find a walkpoint
        if(!walkPointSet) SearchWalkPoint();

        // Set walk speed
        agent.speed = walkSpeed;

        // Travel to walkpoint
        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        // Distance from the walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        Vector3 randomDirection = Random.insideUnitSphere * walkPointRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkPointRange, 1);
        walkPoint = hit.position;
        walkPointSet = true;
    }

    private void ChasePlayer() 
    {
        // Set turning speed
        agent.angularSpeed = defaultAngularSpeed;

        // Set destintion to the player location
        agent.SetDestination(player.position);

        // Set walk speed
        agent.speed = walkSpeed;
    }

    IEnumerator FleeCoroutine()
    {
        // Set fleeing state
        isFleeing = true;

        // Set turning speed
        agent.angularSpeed = fleeingAngularSpeed;

        // Flee from player for fleeDuration seconds
        float startTime = Time.time;
        while (Time.time - startTime < fleeDuration)
        {
            // Set flee speed
            agent.speed = fleeSpeed;

            // Calculate flee direction (away from player)
            Vector3 fleeDirection = transform.position - player.position;
            fleeDirection.y = 0f; // Ensure the wolf doesn't flee vertically

            // Normalize flee direction
            fleeDirection.Normalize();

            // Set destination away from player
            agent.SetDestination(transform.position + fleeDirection * 10f); // Adjust distance as needed

            yield return null;
        }

        // Reset fleeing state
        isFleeing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Campfire"))
        {
            StartCoroutine(FleeCoroutine());
        }
    }
}


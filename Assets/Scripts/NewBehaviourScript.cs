using UnityEngine;
using UnityEngine.AI;

public class SharkRandomNavigation : MonoBehaviour
{
    public WaypointManager waypointManager;
    public float waterHeight = 10f; // The height of the water surface
    public float initialTime = 30f; // Time in seconds to navigate between initial waypoints

    private NavMeshAgent navMeshAgent;
    private Transform currentWaypoint;
    private float timer;
    private bool useRandomWaypoints = false;
    private int initialWaypointIndex = 0;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Ensure the NavMeshAgent is on a NavMesh
        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not on a NavMesh. Ensure the shark starts on a NavMesh.");
        }

        // Initialize the timer
        timer = initialTime;

        // Set the first waypoint
        SetInitialWaypoint();
    }

    void Update()
    {
        // Update the timer
        if (!useRandomWaypoints)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                useRandomWaypoints = true;
                SetRandomWaypoint();
            }
        }

        // Check if the shark has reached the current waypoint
        if (navMeshAgent.remainingDistance < 0.5f)
        {
            if (useRandomWaypoints)
            {
                SetRandomWaypoint();
            }
            else
            {
                SetNextInitialWaypoint();
            }
        }

        // Manually adjust the shark's height to stay below the water surface
        Vector3 position = transform.position;
        position.y = Mathf.Min(position.y, waterHeight - 1.0f); // Stay 1 unit below the water surface
        transform.position = position;
    }

    void SetInitialWaypoint()
    {
        if (waypointManager != null && waypointManager.initialWaypoints.Length > 0)
        {
            currentWaypoint = waypointManager.initialWaypoints[initialWaypointIndex];
            navMeshAgent.SetDestination(currentWaypoint.position);
        }
    }

    void SetNextInitialWaypoint()
    {
        initialWaypointIndex = (initialWaypointIndex + 1) % waypointManager.initialWaypoints.Length;
        SetInitialWaypoint();
    }

    void SetRandomWaypoint()
    {
        if (waypointManager != null && waypointManager.randomWaypoints.Length > 0)
        {
            int randomIndex = Random.Range(0, waypointManager.randomWaypoints.Length);
            currentWaypoint = waypointManager.randomWaypoints[randomIndex];
            navMeshAgent.SetDestination(currentWaypoint.position);
        }
    }
}

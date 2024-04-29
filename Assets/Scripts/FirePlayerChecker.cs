using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Survival))]
public class FirePlayerChecker : MonoBehaviour
{
    public float detectionRadius = 4f;
    public LayerMask playerLayer;

    private bool playerInRange = false;

    private void Update()
    {
        // Check if the player is within the detection radius
        playerInRange = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);
    }

    // Returns a boolean for playerInRange
    public bool IsPlayerInRange()
    {
        return playerInRange;
    }

    // Draws the radius on the scene
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
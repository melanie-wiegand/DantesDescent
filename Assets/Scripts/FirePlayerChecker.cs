using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayerChecker : MonoBehaviour
{
    public Transform player;

    private bool playerInRange = false;

    private void Update()
    {
        // Check if the player is within the detection radius
        if((transform.position - player.position).sqrMagnitude < 25.0f)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    // Returns a boolean for playerInRange
    public bool IsPlayerInRange()
    {
        return playerInRange;
    }
}
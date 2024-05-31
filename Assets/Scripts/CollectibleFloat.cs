using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFloat : MonoBehaviour
{
    public Transform waterTransform; // Assign the water GameObject here
    private Vector3 initialPosition;
    private bool shouldFollowWater = false;

    void Start()
    {
        // Store the initial position of the collectible
        initialPosition = transform.position;
    }

    void Update()
    {
        // Check if the water's height has reached the collectible's initial height
        if (waterTransform.position.y >= initialPosition.y)
        {
            shouldFollowWater = true;
        }

        // If the water has reached or surpassed the collectible's height, follow the water
        if (shouldFollowWater)
        {
            transform.position = new Vector3(transform.position.x, waterTransform.position.y, transform.position.z);
        }
    }
}

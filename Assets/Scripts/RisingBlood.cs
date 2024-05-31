using UnityEngine;

public class RisingWater : MonoBehaviour
{
    public float riseSpeed = 0.5f; // Speed at which the water rises
    public float maxHeight = 10f; // Maximum height the water should reach

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the water
        initialPosition = transform.position;
    }

    void Update()
    {
        // Check if the water has reached the maximum height
        if (transform.position.y < maxHeight)
        {
            // Move the water up over time
            transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
        }
    }

    // Optional: Reset the water position
    public void ResetWater()
    {
        transform.position = initialPosition;
    }
}

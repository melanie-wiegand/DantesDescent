using UnityEngine;

public class RisingWater : MonoBehaviour
{
    public float riseSpeed = 0.5f; // Speed at which the water rises
    public float maxHeight = 10f; // Maximum height the water should reach
    public GameObject player; // Reference to the player
    public GameObject underwaterCanvas; // Reference to the underwater Canvas

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the water
        initialPosition = transform.position;
        
        // Ensure the underwater UI is initially hidden
        if (underwaterCanvas != null)
        {
            underwaterCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the water has reached the maximum height
        if (transform.position.y < maxHeight)
        {
            // Move the water up over time
            transform.position += new Vector3(0, riseSpeed * Time.deltaTime, 0);
        }

        // Check if the water height is higher than the player's height
        if (player != null)
        {
            float playerHeight = player.transform.position.y;
            float waterHeight = transform.position.y;

            if (waterHeight > playerHeight)
            {
                ShowUnderwaterUI();
            }
            else
            {
                HideUnderwaterUI();
            }
        }
    }

    void ShowUnderwaterUI()
    {
        if (underwaterCanvas != null)
        {
            underwaterCanvas.SetActive(true);
        }
    }

    void HideUnderwaterUI()
    {
        if (underwaterCanvas != null)
        {
            underwaterCanvas.SetActive(false);
        }
    }
}
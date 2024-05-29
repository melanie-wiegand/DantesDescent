using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingBlood : MonoBehaviour
{
    public float startHeight = -60f;
    public float endHeight = 50f;
    public float duration = 120f;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rise());
    }

    // Raises the blood over time
    IEnumerator Rise()
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector3 startPosition = new Vector3(transform.position.x, startHeight, transform.position.z);
        Vector3 endPosition = new Vector3(transform.position.x, endHeight, transform.position.z);

        transform.position = startPosition; // Initialize position

        while (elapsedTime < duration)
        {
            // Smoothly transition the position over time
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is exactly the end position
        transform.position = endPosition;
        isMoving = false;
    }
}

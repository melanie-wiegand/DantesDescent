using System.Collections;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    public float rotationSpeed = 30f;  // Speed of rotation
    private float currentAngle = 0f;   // Current angle of rotation relative to the initial orientation
    private float rotationLimit = 25f; // Angle limit for rotation
    private int direction = 1;         // Direction of rotation, 1 for clockwise, -1 for counterclockwise

    private void Start()
    {
        StartCoroutine(RotateWithinLimits());
    }

    IEnumerator RotateWithinLimits()
    {
        while (true) 
        {
            // Update the current angle based on rotation speed and direction
            currentAngle += rotationSpeed * Time.deltaTime * direction;

            // If the current angle exceeds the rotation limits, reverse the direction
            if (Mathf.Abs(currentAngle) > rotationLimit)
            {
                currentAngle = rotationLimit * direction;  // Clamp the angle to the limit
                direction *= -1;  // Reverse the direction

                // Pause at either side for a random amount of time between 1 and 3 seconds
                float pauseTime = Random.Range(1f, 3f);
                yield return new WaitForSeconds(pauseTime);
            }

            // Apply the rotation relative to the initial orientation
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);

            yield return null; 
        }
    }
}


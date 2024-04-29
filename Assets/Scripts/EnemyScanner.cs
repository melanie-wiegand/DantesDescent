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
            }

            // Apply the rotation relative to the initial orientation
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);

            yield return null; 
        }
    }
}


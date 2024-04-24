using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            // Movement speed of the player
    public float turnSpeed = 20f;       // Turning speed of the player

    Rigidbody m_Rigidbody;              // Rigidbody component attached to the player
    Vector3 m_Movement;                 // Stores the movement direction
    Quaternion m_Rotation = Quaternion.identity; // Stores the rotation of the player

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Get horizontal input
        float vertical = Input.GetAxis("Vertical");     // Get vertical input

        m_Movement.Set(horizontal, 0f, vertical);       // Set movement vector based on input
        m_Movement.Normalize();                         // Normalize the movement vector

        // Check if there is input from the user
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        // Rotate towards the direction of movement
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        // Call the Move method to move and rotate the player
        Move();
    }

    // Move the player using Rigidbody
    void Move()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed * Time.deltaTime); // Move the player
        m_Rigidbody.MoveRotation(m_Rotation); // Rotate the player
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float crouchSpeedMultiplier = 0.5f; 

    [Header("Keybinds")]
    public KeyCode crouchKey = KeyCode.LeftShift;
    public Transform orientation;

    private float originalHeight;
    private float originalScaleY;
    public float crouchHeightMultiplier = 0.5f;

    float horizontalInput;
    float verticalInput;

    Rigidbody rb;
    CapsuleCollider playerCollider;

    public bool canMove = true;  // Public variable to control movement

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerCollider = GetComponent<CapsuleCollider>();

        if (playerCollider != null) {
            originalHeight = playerCollider.height;
            originalScaleY = transform.localScale.y;
        } else {
            Debug.LogError("No CapsuleCollider found on the player!");
        }
    }


    private void Update()
    {
        if (canMove) {
            MyInput();
            ManageCrouch();
        }
    }

    private void FixedUpdate()
    {
        if (canMove) {
            MovePlayer();
            RotateToCursor();
        }

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }   
    private void MovePlayer()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;
        float currentSpeed = moveSpeed;

        if (Input.GetKey(crouchKey)) {
            currentSpeed *= crouchSpeedMultiplier;
        }

        rb.velocity = moveDirection.normalized * currentSpeed;

    } 

    public void StopAllMovement() {
    if (rb != null) {
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero; 
    }
}

    private void ManageCrouch()
    {
        if (Input.GetKey(crouchKey))
        {
            playerCollider.height = originalHeight * crouchHeightMultiplier;
            playerCollider.center = new Vector3(playerCollider.center.x, -0.5f, playerCollider.center.z);
            transform.localScale = new Vector3(transform.localScale.x, originalScaleY * crouchHeightMultiplier, transform.localScale.z);
        }
        else
        {
            playerCollider.height = originalHeight;
            playerCollider.center = new Vector3(playerCollider.center.x, 0.0f, playerCollider.center.z);
            transform.localScale = new Vector3(transform.localScale.x, originalScaleY, transform.localScale.z);
        }
    }
    
    private void RotateToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
            Vector3 direction = (heightCorrectedPoint - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10));
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float crouchSpeedMultiplier = 0.5f;
    public float crouchHeightMultiplier = 0.5f;

    [Header("Camera")]
    public Camera playerCamera;

    [Header("Keybinds")]
    public KeyCode crouchKey = KeyCode.LeftShift;

    [Header("Audio")]
    public AudioSource audioSource;

    private float originalHeight;
    private float originalScaleY;
    private Vector3 moveDirection = Vector3.zero;

    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    private Survival survival;


    public bool canMove = true;
    float horizontalInput;
    float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        playerCollider = GetComponent<CapsuleCollider>();
        survival = GetComponent<Survival>(); 


        if (playerCollider != null)
        {
            originalHeight = playerCollider.height;
            originalScaleY = transform.localScale.y;
        }
        else
        {
            Debug.LogError("No CapsuleCollider found on the player!");
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (canMove)
        {
            HandleInput();
            ManageCrouch();
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            MovePlayer();
            RotateToCursor();
        }
    }

    private void HandleInput()
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
            direction = -direction;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10));
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

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            survival.AddToHunger(5);

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

}



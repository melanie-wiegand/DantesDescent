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

    // Collectable hunger addend
    [Header("Hunger Addends")]
    public int HamHungerAmount = 10;
    public int DrumstickHungerAmount = 5;

    private float originalHeight;
    private float originalScaleY;
    private Vector3 moveDirection = Vector3.zero;

    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    private Survival survival;

    private Animator animator;

    public bool canMove = true;
    float horizontalInput;
    float verticalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        playerCollider = GetComponent<CapsuleCollider>();
        survival = GetComponent<Survival>(); 
        animator = GetComponent<Animator>();  // Initialize the Animator


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
            animator.SetBool("IsCrouching", true);
        } else {
            animator.SetBool("IsCrouching", false);
        }

        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetFloat("MoveX", horizontalInput);
        animator.SetFloat("MoveY", verticalInput);

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
        // Check if player collides with a collectable
        if(other.gameObject.CompareTag("Drumstick"))
        {
            // Disable collectable
            other.gameObject.SetActive(false);

            // Increase hunger
            survival.AddToHunger(DrumstickHungerAmount);

            // Play pick up sound
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }

        if(other.gameObject.CompareTag("Ham"))
        {
            // Disable colletable
            other.gameObject.SetActive(false);

            // Increase hunger
            survival.AddToHunger(HamHungerAmount);

            // Play pick up sound
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        
        // Check if player enters the campfire radius
        if(other.gameObject.CompareTag("Campfire"))
        {
            // Change (survival) TempOT to -1f to warm the player
            survival.UpdateTempWarm();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if player exits the campfire radius
        if(other.gameObject.CompareTag("Campfire"))
        {
            // Change (survival) TempOT to 0.5f to cool the player
            survival.UpdateTempCool();
        }      
    }
}



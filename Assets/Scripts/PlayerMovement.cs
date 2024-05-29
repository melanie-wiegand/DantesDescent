using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float crouchSpeedMultiplier = 0.5f;
    public float crouchHeightMultiplier = 0.5f;
    public float sprintSpeedMultiplier = 1.5f;

    [Header("Camera")]
    public Camera playerCamera;

    [Header("Keybinds")]
    public KeyCode crouchKey = KeyCode.LeftShift;
    public KeyCode toggleTorchKey = KeyCode.E;
    public KeyCode sprintKey = KeyCode.Space;

    [Header("Audio")]
    public AudioSource audioSource;

    // Collectable hunger addend
    [Header("Hunger Addends")]
    public int hamHungerAmount = 10;
    public int drumstickHungerAmount = 5;

    [Header("UI Elements")]
    public GameObject torchPromptText; 

    public GameObject torchFireEffect; 
    private bool isNearCampfire = false;

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
    private bool isSprinting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        playerCollider = GetComponent<CapsuleCollider>();
        survival = GetComponent<Survival>(); 
        animator = GetComponent<Animator>();

        if (torchFireEffect != null)
        {
            torchFireEffect.SetActive(false);
            Debug.Log("Torch flame effect assigned: " + torchFireEffect.name);
        }

        if (torchPromptText != null)
        {
            torchPromptText.SetActive(false); // Ensure the prompt text is initially hidden
        }


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
            ManageSprint();
        }

        if (Input.GetKeyDown(toggleTorchKey) && isNearCampfire)
        {
            ToggleTorchFire();
        }

        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            animator.SetTrigger("attack");
            //Check for detection of Wolf GameObject here
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

        if (isSprinting)
        {
            currentSpeed *= sprintSpeedMultiplier;
        }

        if (Input.GetKey(crouchKey)) {
            currentSpeed *= crouchSpeedMultiplier;
            animator.SetBool("IsCrouching", true);
        } else {
            animator.SetBool("IsCrouching", false);
        }

        animator.SetFloat("Speed", moveDirection.magnitude);
        animator.SetFloat("MoveX", horizontalInput);
        animator.SetFloat("MoveY", verticalInput);
        animator.SetBool("IsSprinting", isSprinting);
        
        Vector3 velocity = moveDirection.normalized * currentSpeed;
        velocity.y = rb.velocity.y; // Preserve the Y velocity for gravity

        rb.velocity = velocity;
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
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
            Vector3 direction = (heightCorrectedPoint - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 10));
        }
    }

    private void ManageSprint()
    {
        if (Input.GetKeyDown(sprintKey))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(sprintKey))
        {
            isSprinting = false;
        }
    }

    private void ManageCrouch()
    {
        if (Input.GetKey(crouchKey))
        {
            playerCollider.height = originalHeight * crouchHeightMultiplier;
            playerCollider.center = new Vector3(playerCollider.center.x, playerCollider.center.y * -0.5f, playerCollider.center.z);
            transform.localScale = new Vector3(transform.localScale.x, originalScaleY * crouchHeightMultiplier, transform.localScale.z);
        }
        else
        {
            playerCollider.height = originalHeight;
            playerCollider.center = new Vector3(playerCollider.center.x, playerCollider.center.y, playerCollider.center.z);
            transform.localScale = new Vector3(transform.localScale.x, originalScaleY, transform.localScale.z);
        }
    }

    private void ToggleTorchFire()
    {
        if (torchFireEffect != null)
        {
            animator.SetTrigger("lightingTorch");
            torchFireEffect.SetActive(!torchFireEffect.activeSelf);
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
            survival.UpdateHunger(drumstickHungerAmount);

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
            survival.UpdateHunger(hamHungerAmount);

            // Play pick up sound
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }

        // Check if player enters the campfire radius
        if(other.gameObject.CompareTag("Campfire"))
        {
            isNearCampfire = true;
            if (torchPromptText != null)
            {
                torchPromptText.SetActive(true); // Show the prompt text when near a campfire
            }
            // Change (survival) TempOT to -1f to warm the player
            survival.SetNearCampfireState();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if player exits the campfire radius
        if(other.gameObject.CompareTag("Campfire"))
        {
            isNearCampfire = false;
            if (torchPromptText != null)
            {
                torchPromptText.SetActive(false); // Hide the prompt text when leaving the campfire
            }
            // Change (survival) TempOT to 0.5f to cool the player
            survival.ResetTemperatureState();
        }      
    }
}



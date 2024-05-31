using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxygenController : MonoBehaviour
{
    // The maximum oxygen level
    public float maxOxygen = 100f;

    // Reference to the oxygen slider
    public Slider oxygenSlider;

    // Indicate whether the player is underwater or not
    private bool isUnderwater = false;

    // Refernece to the game over screen
    public GameOverScreen gameOverScreen;

    // Reference to the blood object
    public GameObject bloodObject;

    // The maximum slowing effect from the water
    public float maxSlowSpeedModifier = 0.5f;

    // Speed modifier based on water depth
    private float speedModifier = 1f;

    // Reference to player movement script
    private PlayerMovement playerMovement;

    // The oxygen states
    public enum OxygenState
    {
        Breathing,
        Underwater
    }

    // The current oxygen state
    public OxygenState currentState = OxygenState.Breathing;

    // Called once
    void Start()
    {
        // Set the maximum value of the oxygen slider to maxOxygen
        oxygenSlider.maxValue = maxOxygen;

        // Set the value of the slider to full
        oxygenSlider.value = maxOxygen;

        // PlayerMovement component
        playerMovement = GetComponent<PlayerMovement>();

        // Disable slider if necessary
        DisableSlider(oxygenSlider);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is underwater
        CheckUnderwater();

        // Set the current state
        SetState();

        // Switch the case
        UpdateOxygen();

        // Check if the player has drowned
        if(currentState == OxygenState.Underwater)
        {
            CheckDrowned();
        }
    }

    public void UpdateOxygen() {
        // Update oxygen state
        switch (currentState)
        {
            // Oxygen increases
            case OxygenState.Breathing:
                isUnderwater = false;
                oxygenSlider.value += 10f * Time.deltaTime;
                break;

            // Oxygen decreases
            case OxygenState.Underwater:
                isUnderwater = true;
                oxygenSlider.value -= 10f * Time.deltaTime;
                break;
        }

    }

    private void CheckUnderwater()
    {
        // The player's Y value
        float playerY = GetPlayerMaxY() - 0.5f;

        // The blood's Y value
        float bloodY = GetYPosition(bloodObject);

        // Check if the blood level is above the player and set isUnderwater
        if (bloodY < playerY)
        {
            isUnderwater = false;
        }
        else
        {
            isUnderwater = true;
        }
    }

    private float GetYPosition(GameObject obj)
    {
        // Return the y position of the game object
        return obj.transform.position.y;
    }

    private float GetPlayerMaxY()
    {
        Collider collider = GetComponent<Collider>();
        if(collider != null)
        {
            return collider.bounds.max.y;
        }
        else
        {
            return transform.position.y;
        }
    }

    private void SetState()
    {
        // Set the current state based on if the player is underwater or not
        if(isUnderwater)
        {
            currentState = OxygenState.Underwater;
        }
        else 
        {
            currentState = OxygenState.Breathing;
        }
    }

    private void CheckDrowned()
    {
        if(oxygenSlider.value <= 0)
        {
            Debug.Log("You drowned!");
            //gameOverScreen.ShowDrownLoss();
        }
    }

    // returns a slow modifier
    public float SlowSpeed()
    {
        // The player's Y position
        float playerY = transform.position.y;

        // The blood's Y position
        float bloodY = GetYPosition(bloodObject);

        // The depth of the water based on the player position
        float depth = Mathf.Max(0, bloodY - playerY);

        // The speed modifier
        return Mathf.Lerp(speedModifier, maxSlowSpeedModifier, depth);
    }

    // Disable a slider if the level isn't level 2
    public void DisableSlider(Slider slider)
    {
        // Get the active scene
        Scene activeScene = SceneManager.GetActiveScene();

        // If the scene name isn't level 1, disable the slider
        if (activeScene.name != "Level 2 (Blood)")
        {
            slider.gameObject.SetActive(false);
        }
    }
}
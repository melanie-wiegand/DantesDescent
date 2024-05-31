using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                oxygenSlider.value -= 2f * Time.deltaTime;
                break;
        }

    }

    private void CheckUnderwater()
    {
        // The player's Y value
        float playerY = transform.position.y;

        // The blood's Y value
        float bloodY = GetYPosition(bloodObject);

        // Check if the blood level is above the player and set isUnderwater
        if (bloodY > playerY)
        {
            isUnderwater = true;
        }
        else
        {
            isUnderwater = false;
        }
    }

    float GetYPosition(GameObject obj)
    {
        // Return the y position of the game object
        return obj.transform.position.y;
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
}
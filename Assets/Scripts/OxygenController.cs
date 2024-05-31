using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenController : MonoBehaviour
{
    [Header("Player Oxygen")]
    public float maxOxygen = 100f;
    public Slider oxygenSlider;
    public float oxygenOT;
    private bool isUnderwater = false;

    public GameOverScreen gameOverScreen;

    public enum OxygenState
    {
        Breathing,
        Underwater
    }

    public OxygenState currentState = OxygenState.Breathing;

 
    void Start()
    {
        oxygenSlider.maxValue = maxOxygen;
        oxygenSlider.value = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        CheckUnderwater();
        SetState();
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
        // if the water is above the player's head height
            // isUnderwater = true;
        // else
            // isUnderwater = false;
    }

    private void SetState()
    {
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
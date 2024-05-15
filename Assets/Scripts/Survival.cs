using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Survival : MonoBehaviour
{
    [Header("Player Hunger")]
    public float maxHunger = 100f;
    public float hunger = 0f;
    public Slider hungerSlider;
    public float hungerOT = 0.5f;

    [Header("Player Temperature")]
    public float maxTemp = 100f;
    public float temperature = 0f;
    public float tempOT = 0.5f;
    public Slider tempSlider;

    public enum TemperatureState
    {
        Default,
        CyclingHail,
        NearCampfire
    }

    public TemperatureState currentState = TemperatureState.Default;

 
    // Start is called before the first frame update
    void Start()
    {
        // Initialize hunger and temperature to 100%
        hunger = maxHunger;
        temperature = maxTemp;
        hungerSlider.maxValue = 100f;
        tempSlider.maxValue = 100f;
        ResetTemperatureState();
//        UpdateSliders();      
    }

    // Update is called once per frame
    void Update()
    {
        // Update temperature based on the current state
        switch (currentState)
        {
            // temperature changes at a normal rate
            case TemperatureState.Default:
                tempOT = 0.5f;
                temperature -= tempOT * Time.deltaTime;
                UpdateSliders();
                break;

            // Temperature decreases more rapidly
            case TemperatureState.CyclingHail:
                tempOT = 1f;
                temperature -= tempOT * Time.deltaTime;
                UpdateSliders();
                break;

            // Temperature increases
            case TemperatureState.NearCampfire:
                tempOT = 3f;
                temperature += tempOT * Time.deltaTime;
                UpdateSliders();
                break;
        }

        // Bound the temperature and hunger
        temperature = Mathf.Clamp(temperature, 0f, 100f);
        hunger = Mathf.Clamp(hunger, 0f, 100f);

        // Hunger decreases over time
        hunger = hunger - hungerOT * Time.deltaTime;

        // Update sliders
        UpdateSliders();
    }

    // Updates the hunger and temperature sliders
    public void UpdateSliders()
    {
//        Debug.Log("Updating sliders: Hunger: " + (hunger / maxHunger) + ", Temperature: " + (temperature / maxTemp));
        hungerSlider.value = hunger;
        tempSlider.value = temperature;
    }

    // Increases hunger by amount
    public void AddToHunger(int amount)
    {
        hunger += amount;
    }

    // Set state to cycling hail
    public void SetCyclingHailState()
    {
        currentState = TemperatureState.CyclingHail;
    }

    // Set state to near campfire
    public void SetNearCampfireState()
    {
        // Check if the current state is not cycling hail before transitioning
        if (currentState != TemperatureState.CyclingHail)
        {
            currentState = TemperatureState.NearCampfire;
        }
    }

    // Set state to default
    public void ResetTemperatureState()
    {
        currentState = TemperatureState.Default;

    }
}

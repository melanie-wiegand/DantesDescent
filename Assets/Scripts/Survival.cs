using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Survival : MonoBehaviour
{
    [Header("Player Hunger")]
    public float maxHunger = 100f;
    public Slider hungerSlider;
    public float hungerOT;

    [Header("Player Temperature")]
    public float maxTemp = 100f;
    public float tempOT;
    public Slider tempSlider;


    public enum TemperatureState
    {
        Default,
        CyclingHail,
        NearCampfire
    }

    public TemperatureState currentState = TemperatureState.Default;

 
    void Start()
    {
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;
        tempSlider.maxValue = maxTemp;
        tempSlider.value = maxTemp;
        ResetTemperatureState();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTemperature();
        UpdateHunger(-hungerOT * Time.deltaTime);

    }

    public void UpdateTemperature() {
        // Update temperature based on the current state
        switch (currentState)
        {
            // temperature changes at a normal rate
            case TemperatureState.Default:
                tempSlider.value -= 0.1f * Time.deltaTime;
                break;

            // Temperature decreases more rapidly
            case TemperatureState.CyclingHail:
                tempSlider.value -= 0.5f * Time.deltaTime;
                break;

            // Temperature increases
            case TemperatureState.NearCampfire:
                tempSlider.value += 10f * Time.deltaTime;
                break;
        }

    }

    // Increases hunger by amount
    public void UpdateHunger(float amount)
    {
        hungerSlider.value += amount;
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
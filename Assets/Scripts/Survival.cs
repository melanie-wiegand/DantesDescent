using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public GameOverScreen gameOverScreen;

    public enum TemperatureState
    {
        Default,
        CyclingHail,
        NearCampfire
    }

    public TemperatureState currentState = TemperatureState.Default;

 
    void Start()
    {
        DisableSlider(tempSlider);
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
        //if (hungerSlider.value <= 0)
        //{
        //    gameOverScreen.ShowHungerLoss();
        //}
        //if (tempSlider.value <= 0)
        //{
        //    gameOverScreen.ShowTempLoss();
        //}
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

    // Disable a slider if the level isn't level 1
    public void DisableSlider(Slider slider)
    {
        // Get the active scene
        Scene activeScene = SceneManager.GetActiveScene();

        // If the scene name isn't level 1, disable the slider
        if (activeScene.name != "Level 1")
        {
            slider.gameObject.SetActive(false);
        }
    }
}
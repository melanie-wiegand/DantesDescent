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
    public float hailTempOT = 1f;
    public float campfireTempOT = 3f;
    public Slider tempSlider;

    [Header("Hail")]
    public GameObject hailParticle; // Reference to the hail particle GameObject
    private float cycleDurationMin = 7f; // Minimum duration of the cycle
    private float cycleDurationMax = 12f; // Maximum duration of the cycle
    private float breakDuration = 15f; // Duration of the break between cycles
    public bool isCycling = false; // Flag to track if the hail cycle is active

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
        ResetTemperatureState();
        UpdateSliders();

        // Start the first cycle
        StartCycle();        
    }

    // Update is called once per frame
    void Update()
    {
        // Update temperature based on the current state
        switch (currentState)
        {
            // temperature changes at a normal rate
            case TemperatureState.Default:
                temperature -= tempOT * Time.deltaTime;
                break;

            // Temperature decreases more rapidly
            case TemperatureState.CyclingHail:
                temperature -= hailTempOT * Time.deltaTime;
                break;

            // Temperature increases
            case TemperatureState.NearCampfire:
                temperature += campfireTempOT * Time.deltaTime;
                break;
        }

        // Bound the temperature and hunger
        temperature = Mathf.Clamp(temperature, 0f, 100f);
        temperature = Mathf.Clamp(hunger, 0f, 100f);

        // Hunger decreases over time
        hunger = hunger - hungerOT * Time.deltaTime;

        // Update sliders
        UpdateSliders();
    }

    // Updates the hunger and temperature sliders
    void UpdateSliders()
    {
        Debug.Log("Updating sliders: Hunger: " + (hunger / maxHunger) + ", Temperature: " + (temperature / maxTemp));
        hungerSlider.value = hunger / maxHunger;
        tempSlider.value = temperature / maxTemp;
    }

    // Increases hunger by amount
    public void AddToHunger(int amount)
    {
        this.hunger = this.hunger + amount;
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
        if(!isCycling)
        {
            currentState = TemperatureState.Default;
        }
    }

    // Method to start the hail cycle
    private void StartCycle()
    {
        // Set isCycling flag to true
        isCycling = true;

        // Set TempOT to 2f
        SetCyclingHailState();

        // Activate the hail particle GameObject
        hailParticle.SetActive(true);

        // Reset the opacity of the particle system
        ResetOpacity();

        // Generate a random duration for the cycle
        float cycleDuration = Random.Range(cycleDurationMin, cycleDurationMax);

        // Invoke the EndCycle method after the cycle duration
        Invoke("EndCycle", cycleDuration);
    }

        // Method to end the hail cycle
    private void EndCycle()
    {
        // Start fading out the particle system
        FadeOut();

        // Set isCycling flag to false
        isCycling = false;

        // Set TempOT to -0.5f
        ResetTemperatureState();        

        // Invoke the StartCycle method after the break duration
        Invoke("StartCycle", breakDuration);
    }

    // Method to reset the opacity of the particle system
    private void ResetOpacity()
    {
        // Get the ParticleSystem component from the hail particle GameObject
        ParticleSystem particleSystem = hailParticle.GetComponent<ParticleSystem>();

        // Get the main module of the particle system
        ParticleSystem.MainModule mainModule = particleSystem.main;

        // Reset the start color alpha value to 1 (fully opaque)
        Color startColor = mainModule.startColor.color;
        startColor.a = 1f;
        mainModule.startColor = startColor;
    }

    // Method to fade out the particle system
    private void FadeOut()
    {
        // Get the ParticleSystem component from the hail particle GameObject
        ParticleSystem particleSystem = hailParticle.GetComponent<ParticleSystem>();

        // Get the main module of the particle system
        ParticleSystem.MainModule mainModule = particleSystem.main;

        // Start a coroutine to gradually reduce the opacity of the particle system
        StartCoroutine(FadeOutCoroutine(mainModule));
    }

    // Coroutine to gradually reduce the opacity of the particle system
    private System.Collections.IEnumerator FadeOutCoroutine(ParticleSystem.MainModule mainModule)
    {
        // Get the initial start color alpha value
        float startAlpha = mainModule.startColor.color.a;

        // Define the fade-out duration
        float fadeOutDuration = 1f; // Adjust as needed

        // Define the time elapsed
        float elapsedTime = 0f;

        // Gradually reduce the opacity of the particle system over the fade-out duration
        while (elapsedTime < fadeOutDuration)
        {
            // Calculate the new alpha value based on the elapsed time and duration
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeOutDuration);

            // Set the new start color alpha value
            Color startColor = mainModule.startColor.color;
            startColor.a = newAlpha;
            mainModule.startColor = startColor;

            // Update the time elapsed
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final alpha value is set to 0
        Color finalColor = mainModule.startColor.color;
        finalColor.a = 0f;
        mainModule.startColor = finalColor;
    }
}

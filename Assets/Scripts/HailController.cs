using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailController : MonoBehaviour
{
    public GameObject hailParticle; // Reference to the hail particle GameObject
    private float cycleDurationMin = 7f; // Minimum duration of the cycle
    private float cycleDurationMax = 12f; // Maximum duration of the cycle
    private float breakDuration = 15f; // Duration of the break between cycles
    public bool isCycling = false; // Flag to track if the hail cycle is active
    public Survival survival;

    // Start is called before the first frame update
    void Start()
    {
        // Start the first cycle
        StartCycle();
    }

    // Method to start the hail cycle
    public void StartCycle()
    {
        // Set isCycling flag to true
        isCycling = true;

        // Set TempOT to 2f
        survival.SetCyclingHailState();

        // Activate the hail particle GameObject
        hailParticle.SetActive(true);

        // Reset the opacity of the particle system
        ResetOpacity();

        // Generate a random duration for the cycle
        float cycleDuration = Random.Range(cycleDurationMin, cycleDurationMax);

        // Invoke the EndCycle method after the cycle duration
        if(hailParticle.activeSelf)
        {
            Invoke("EndCycle", cycleDuration);
        }
    }

        // Method to end the hail cycle
    private void EndCycle()
    {
        // Start fading out the particle system
        if(hailParticle.activeSelf)
        {
            FadeOut();
        }
        // Set isCycling flag to false
        isCycling = false;

        // Set TempOT to -0.5f
        survival.ResetTemperatureState();        

        // Invoke the StartCycle method after the break duration
        if(hailParticle.activeSelf)
        {
            Invoke("StartCycle", breakDuration);
        }
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
        if(hailParticle.activeSelf)
        {
            StartCoroutine(FadeOutCoroutine(mainModule));
        }
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

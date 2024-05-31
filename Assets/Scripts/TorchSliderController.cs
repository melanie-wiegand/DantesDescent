using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TorchSliderController : MonoBehaviour
{
    // Torch variables
    public float maxTorch = 100f;
    public bool isLit = false;
    public float extinguishOT;

    // References
    private PlayerMovement playerMovement;
    public Slider torchSlider;
    public Light torchFireLight;

    // Light variables
    public float initialIntensity;
    public float endIntensity = 0.0f;

    [SerializeField, Range(0f, 10f)]
    public float currentLower;

    [SerializeField, Range(0f, 10f)]
    public float currentUpper;

    [SerializeField, Range(0f, 10f)]
    float initialLower = 0f;

    [SerializeField, Range(0f, 10f)]
    float initialUpper = 10f;


    // Start is called before the first frame update
    void Start()
    {
        // Set the max value of the slider
        torchSlider.maxValue = maxTorch;

        // Disable slider if necessary
        DisableSlider(torchSlider);

        // Components
        playerMovement = GetComponent<PlayerMovement>();

        // Set the initial light intensity
        torchFireLight.intensity = initialIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        // Torch decrease level over time
        torchSlider.value += extinguishOT * Time.deltaTime;

        // Check if the torch value has reached 0
        CheckExtinguish();

        LightIntensity();
    }

    public void LightTorch()
    {
        // Set the value to full
        torchSlider.value = maxTorch;

        // Set the decrease rate
        extinguishOT = -3f;

        // The torch is lit
        isLit = true;
    }

    public void ExtinguishTorch()
    {
        // Set the value to 0
        torchSlider.value = 0;

        // Set the decrease rate to 0
        extinguishOT = 0f;

        // The torch is not lit
        isLit = false;
    }

    private void CheckExtinguish()
    {
        // Check if the slider has hit 0
        if(torchSlider.value <= 0)
        {
            // Extinguish the torch
            StartCoroutine(playerMovement.TurnOffTorchFire(0.3f));
        }
    }

    private void LightIntensity()
    {
        // Lerp the light intensity based on the slider value
        float normalizedSliderValue = torchSlider.value / 100.0f;

        currentLower = Mathf.Lerp(endIntensity, initialLower, normalizedSliderValue);

        currentUpper = Mathf.Lerp(endIntensity, initialUpper, normalizedSliderValue);

        torchFireLight.intensity = Random.Range(currentLower, currentUpper);
    }

    // Disable a slider if the level isn't level 3
    public void DisableSlider(Slider slider)
    {
        // Get the active scene
        Scene activeScene = SceneManager.GetActiveScene();

        // If the scene name isn't level 3, disable the slider
        if (activeScene.name != "Level 3 (Darkness - Working)")
        {
            slider.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TorchSliderController : MonoBehaviour
{
    // Maximum torch level
    public float maxTorch = 100f;

    // Reference to torch slider
    public Slider torchSlider;

    // Indicate if the torch is lit
    public bool isLit = false;

    // How quickly the torch extinguishes
    public float extinguishOT;

    // Reference to player movement script
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        // Set the max value of the slider
        torchSlider.maxValue = maxTorch;

        // Disable slider if necessary
        DisableSlider(torchSlider);

        // PlayerMovement component
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Torch decrease level over time
        torchSlider.value += extinguishOT * Time.deltaTime;

        // Check if the torch value has reached 0
        CheckExtinguish();
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

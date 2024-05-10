using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Survival : MonoBehaviour
{
    [Header("Player Hunger")]
    public float MaxHunger = 100f;
    public float Hunger = 0f;
    public Slider HungerSlider;
    public float HungerOT = 0.5f;

    [Header("Player Temperature")]
    public float MaxTemp = 100f;
    public float Temperature = 0f;
    public float TempOT = 0.5f;
    public Slider TempSlider;
 
    // Start is called before the first frame update
    void Start()
    {
        // Initialize hunger and temperature to 100%
        Hunger = MaxHunger;
        Temperature = MaxTemp;
    }

    // Update is called once per frame
    void Update()
    {
        // Hunger and temperature decrease over time
        Hunger = Hunger - HungerOT * Time.deltaTime;
        Temperature = Temperature - TempOT * Time.deltaTime;

        // Update the sliders each frame
        UpdateSliders();

        // Cap the maximum temperature when player is warming up
        if(TempOT == -1f)
        {
            if(Temperature >= MaxTemp)
                Temperature = 100;
        }

        // Max hunger
        if(Hunger >= 100)
        {
            Hunger = 100;
        }

        // Min hunger and temperature (adjust later to death screens)
        if(Hunger <= 0)
        {
            Hunger = 0;
        }

        // Cap minimum temperature (adjust later to death screens)
        if(Temperature <= 0)
        {
            Temperature = 0;
        }
    }
    // Changes the temperature over time to warm the player
    public void UpdateTempWarm()
    {
        TempOT = -1f;
    }

    // Changes the temperature over time to cool the player
    public void UpdateTempCool()
    {
        TempOT = 0.5f;
    }

    // Updates the hunger and temperature sliders
    void UpdateSliders()
    {
        HungerSlider.value = Hunger / MaxHunger;
        TempSlider.value = Temperature / MaxTemp;
    }

    // Increases hunger by amount
    public void AddToHunger(int amount)
    {
        this.Hunger = this.Hunger + amount;
    }
}

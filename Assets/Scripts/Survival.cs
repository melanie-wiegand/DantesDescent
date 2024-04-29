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

    public FirePlayerChecker firePlayerChecker;

    // Start is called before the first frame update
    void Start()
    {
        Hunger = MaxHunger;
        Temperature = MaxTemp;
    }

    // Update is called once per frame
    void Update()
    {
        Hunger = Hunger - HungerOT * Time.deltaTime;
        Temperature = Temperature - TempOT * Time.deltaTime;

        UpdateSliders();

        if(firePlayerChecker.IsPlayerInRange())
        {
            TempOT = -1f;
        }
        else
        {
            TempOT = 0.5f;
        }
    }

    void UpdateSliders()
    {
        HungerSlider.value = Hunger / MaxHunger;
        TempSlider.value = Temperature / MaxTemp;
    }

    public void AddToHunger(int amount)
    {
        this.Hunger = this.Hunger + amount;
    }

    public void TempWarm()
    {
        TempOT = -1f;
    }

    public void TempCool()
    {
        TempOT = 0.5f;
    }
}

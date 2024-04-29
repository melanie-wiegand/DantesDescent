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
        Hunger = MaxHunger;
        Temperature = MaxTemp;
    }

    // Update is called once per frame
    void Update()
    {
        Hunger = Hunger - HungerOT * Time.deltaTime;
        Temperature = Temperature - TempOT * Time.deltaTime;

        UpdateSliders();
    }

    void UpdateSliders()
    {
        HungerSlider.value = Hunger / MaxHunger;
        TempSlider.value = Temperature / MaxTemp;
    }
}

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
    public Transform player;
 
    // Start is called before the first frame update
    void Start()
    {
        Hunger = MaxHunger;
        Temperature = MaxTemp;
    }

    // Update is called once per frame
    void Update()
    {
        // Hunger and temperature decrease over time
        Hunger = Hunger - HungerOT * Time.deltaTime;
        Temperature = Temperature - TempOT * Time.deltaTime;

        UpdateSliders();

 //       firePlayerChecker = GetNearestFire();
        // Adjust temperature if the player is near a campfire
        if(firePlayerChecker.IsPlayerInRange())
        {
            TempOT = -1f;
            // Check for max temperature
            if(Temperature >= MaxTemp)
                Temperature = 100;
        }
        else if(!firePlayerChecker.IsPlayerInRange())
        {
            TempOT = 0.5f;
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

        if(Temperature <= 0)
        {
            Temperature = 0;
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

/*
    GameObject GetNearestFire(GameObject player, GameObject[] fires)
    {
        GameObject nearestFire = null;
        float nearestDistance = float.MaxValue;

        for(int i = 0; i < fires.length; i++)
        {
            float distance = (fires[i].transform.position - player.transform.position).sqrMagnitude;

            if(distance < nearestDistance)
            {
                nearestFire = fires[i];
                nearestDistance = distance;
            }
        }
        return nearestFire;
    }
*/
}

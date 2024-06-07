using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{
    public Menu menu;
    public Timer timer; // Reference to the Timer script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer.StopTimer();
            menu.ReturnToMenu();
        }
    }
}

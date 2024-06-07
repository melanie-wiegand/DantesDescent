using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public Timer timer; // Reference to the Timer script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer.StopTimer(); // Stop the timer and save the time
            gameOverScreen.ShowWin();
        }
    }
}

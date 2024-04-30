using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public GameOverScreen gameOverScreen; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {

            gameOverScreen.ShowGameOver();

        }
    }

}


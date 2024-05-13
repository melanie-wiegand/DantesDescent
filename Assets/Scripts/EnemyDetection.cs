using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public GameOverScreen gameOverScreen; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(gameOverScreen.Jumpscare());
            //gameOverScreen.Jumpscare();

            gameOverScreen.ShowGameOver();
        }
    }

}


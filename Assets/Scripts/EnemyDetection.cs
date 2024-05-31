using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public GameOverScreen gameOverScreen; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (gameObject.CompareTag("Wolf"))
            {
                StartCoroutine(gameOverScreen.WJumpscare());
            }
            else
            {
                StartCoroutine(gameOverScreen.ZJumpscare());
            }
            //gameOverScreen.Jumpscare();

            gameOverScreen.ShowGameOver();
        }
    }

}


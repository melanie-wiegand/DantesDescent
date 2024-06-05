using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public GameOverScreen gameOverScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&  gameOverScreen.GameOver == false) 
        {

            gameOverScreen.GameOver = true;
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


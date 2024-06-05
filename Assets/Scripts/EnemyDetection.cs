using UnityEngine;
using System.Collections;

public class EnemyDetection : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public ThirdPersonCam thirdPersonCam;
    public Camera playerCamera;
    public float rotationSpeed = 4f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&  gameOverScreen.GameOver == false) 
        {
            thirdPersonCam.LockCursor();
            gameOverScreen.GameOver = true;
            StartCoroutine(Jumpscare(other.transform));
            
        }
    }

    private IEnumerator Jumpscare(Transform player)
    {
        //yield return StartCoroutine(RotateCamera(player));
        
        if (gameObject.CompareTag("Wolf"))
        {
            yield return StartCoroutine(gameOverScreen.WJumpscare());
        }
        else
        {
            yield return StartCoroutine(gameOverScreen.ZJumpscare());
        }
        //gameOverScreen.Jumpscare();

        gameOverScreen.ShowGameOver();
    }

    private IEnumerator RotateCamera(Transform player)
    {
        Vector3 directionToEnemy = (transform.position - player.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        while (Quaternion.Angle(playerCamera.transform.rotation, lookRotation) > 0.1f)
        {
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        playerCamera.transform.rotation = lookRotation;
    }

}


using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public GameObject gameEndedScreen;
    public Camera mainCamera;
    public ThirdPersonCam thirdPersonCam;

    public PlayerMovement2 playerMovement;

    private void Start() {
        gameEndedScreen.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(RotateCameraTowards(transform.position)); 
            gameEndedScreen.SetActive(true); 
            Debug.Log("Game over screen activated: " + gameEndedScreen.activeSelf);  // This should log true

            PauseGameplay();
            playerMovement.StopAllMovement();
            thirdPersonCam.UnlockCursor();
        }
    }

    System.Collections.IEnumerator RotateCameraTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - mainCamera.transform.position).normalized;
        float rotationSpeed = 2f;
        float dotProduct;

        while ((dotProduct = Vector3.Dot(mainCamera.transform.forward, direction)) < 0.99f) {
            Vector3 newDir = Vector3.RotateTowards(mainCamera.transform.forward, direction, rotationSpeed * Time.unscaledDeltaTime, 0.0f);
            mainCamera.transform.rotation = Quaternion.LookRotation(newDir);
            yield return null;
        }
    }
    
    public void PauseGameplay() {
        playerMovement.canMove = false;
    }
}


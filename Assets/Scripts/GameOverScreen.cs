using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;
    public ThirdPersonCam thirdPersonCam;
    public PlayerMovement2 playerController;

    void Start() {
        gameOverPanel.SetActive(false); 
        Debug.Log("Game over screen activated: " + gameOverPanel.activeSelf);

    }

    public void RestartFromCheckpoint(Transform checkpoint) {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").transform.position = checkpoint.position;
        gameOverPanel.SetActive(false);  
        thirdPersonCam.LockCursor(); 
        playerController.canMove = true;

    }

    public void RestartGame(Transform startPoint) {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").transform.position = startPoint.position; 
        gameOverPanel.SetActive(false);
        thirdPersonCam.LockCursor(); 
        playerController.canMove = true;
    }
    
}

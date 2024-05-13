using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;
    public ThirdPersonCam thirdPersonCam;
    public PlayerMovement playerMovement;
    public GameObject statusBars;
    public GameObject JumpscareScreen;


    void Start()
    {
        gameOverPanel.SetActive(false);
        JumpscareScreen.SetActive(false);
    }

    public IEnumerator Jumpscare()
    {
        //StartCoroutine(Jumpscare());
        JumpscareScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        JumpscareScreen.SetActive(false);
        StopCoroutine(Jumpscare());
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        statusBars.SetActive(false);
        playerMovement.canMove = false;
        playerMovement.StopAllMovement();
        thirdPersonCam.UnlockCursor();
    }

    public void RestartFromCheckpoint(Transform checkpoint)
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").transform.position = checkpoint.position;
        gameOverPanel.SetActive(false);
        thirdPersonCam.LockCursor();
        playerMovement.canMove = true;

    }

    public void RestartGame(Transform startPoint)
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").transform.position = startPoint.position;
        gameOverPanel.SetActive(false);
        thirdPersonCam.LockCursor();
        playerMovement.canMove = true;
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;
    public ThirdPersonCam thirdPersonCam;
    public PlayerMovement playerMovement;
    public GameObject statusBars;
    public GameObject ZombieJumpscare;
    public GameObject WolfJumpscare;
    public GameObject WinScreen;
    public GameObject HungerScreen;
    public GameObject TempScreen;
    public bool GameOver;

    void Start()
    {
        gameOverPanel.SetActive(false);
        ZombieJumpscare.SetActive(false);
        WolfJumpscare.SetActive(false);
        WinScreen.SetActive(false);
        HungerScreen.SetActive(false);
        TempScreen.SetActive(false);
        GameOver = false;
    }

    public IEnumerator ZJumpscare()
    {
        //StartCoroutine(Jumpscare());

        ZombieJumpscare.SetActive(true);
        yield return new WaitForSeconds(3);
        ZombieJumpscare.SetActive(false);
        StopCoroutine(ZJumpscare());
    }

    public IEnumerator WJumpscare()
    {
        //StartCoroutine(Jumpscare());

        WolfJumpscare.SetActive(true);
        yield return new WaitForSeconds(3);
        WolfJumpscare.SetActive(false);
        StopCoroutine(WJumpscare());
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        statusBars.SetActive(false);
        playerMovement.canMove = false;
        playerMovement.StopAllMovement();
        thirdPersonCam.UnlockCursor();
    }

    public void ShowHungerLoss()
    {
        HungerScreen.SetActive(true);
        //statusBars.SetActive(false);
        playerMovement.canMove = false;
        playerMovement.StopAllMovement();
        thirdPersonCam.UnlockCursor();
    }

    public void ShowTempLoss()
    {
        TempScreen.SetActive(true);
        //statusBars.SetActive(false);
        playerMovement.canMove = false;
        playerMovement.StopAllMovement();
        thirdPersonCam.UnlockCursor();
    }
    
    public void ShowWin()
    {
        WinScreen.SetActive(true);
        statusBars.SetActive(false);
        playerMovement.canMove = false;
        playerMovement.StopAllMovement();
        thirdPersonCam.UnlockCursor();
    }

    //public void RestartFromCheckpoint(Transform checkpoint)
    //{
     //   Time.timeScale = 1;
    //    GameObject.FindGameObjectWithTag("Player").transform.position = checkpoint.position;
     //   gameOverPanel.SetActive(false);
     //   thirdPersonCam.LockCursor();
    //    thirdPersonCam.LockCursor();
    //    playerMovement.canMove = true;

    //}

    public void RestartGame()
    {
        Time.timeScale = 1;
       // GameObject.FindGameObjectWithTag("Player").transform.position = startPoint.position;
        gameOverPanel.SetActive(false);
        thirdPersonCam.LockCursor();
        playerMovement.canMove = true;
        GameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        thirdPersonCam.LockCursor();
        playerMovement.canMove = true;
        SceneManager.LoadScene(0);
    }

    public void GoNextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 5);
    }

}

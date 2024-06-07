using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject BlackScreen;
    public TMP_Text purgatoryBestTimeText;
    public TMP_Text gluttonyBestTimeText;
    public TMP_Text fraudBestTimeText;
    public TMP_Text violenceBestTimeText;

    private void Start()
    {
        BlackScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Display best times for each level
        DisplayBestTimes();
    }

    public void PurgatoryStart()
    {
        BlackScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void GluttonyStart()
    {
        BlackScreen.SetActive(true);
        SceneManager.LoadScene(2);
    }

    public void FraudStart()
    {
        BlackScreen.SetActive(true);
        SceneManager.LoadScene(3);
    }

    public void ViolenceStart()
    {
        BlackScreen.SetActive(true);
        SceneManager.LoadScene(4);
    }

    public void ReturnToMenu()
    {
        BlackScreen.SetActive(true);
        SceneManager.LoadScene(0);
    }

    private void DisplayBestTimes()
    {
        if (purgatoryBestTimeText != null)
            purgatoryBestTimeText.text = FormatTime(BestTimeManager.GetBestTime(1));
        if (gluttonyBestTimeText != null)
            gluttonyBestTimeText.text = FormatTime(BestTimeManager.GetBestTime(2));
        if (fraudBestTimeText != null)
            fraudBestTimeText.text = FormatTime(BestTimeManager.GetBestTime(3));
        if (violenceBestTimeText != null)
            violenceBestTimeText.text = FormatTime(BestTimeManager.GetBestTime(4));
    }

    private string FormatTime(float time)
    {
        if (time == float.MaxValue)
            return "N/A";

        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}

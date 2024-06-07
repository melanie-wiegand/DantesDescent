using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text currentTimeText;
    private float currentTime;
    private bool isRunning;

    void Start()
    {
        currentTime = 0f;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            currentTimeText.text = FormatTime(currentTime);
        }
    }

    public void StopTimer()
    {
        isRunning = false;
        SaveBestTime(currentTime);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    private void SaveBestTime(float time)
    {
        string levelKey = "Level_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + "_BestTime";
        if (PlayerPrefs.HasKey(levelKey))
        {
            float bestTime = PlayerPrefs.GetFloat(levelKey);
            if (time < bestTime)
            {
                PlayerPrefs.SetFloat(levelKey, time);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(levelKey, time);
        }
    }
}



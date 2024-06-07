using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text bestTimeText;

    void Start()
    {
        UpdateBestTimeUI();
    }

    public void UpdateBestTimeUI()
    {
        int levelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        float bestTime = BestTimeManager.GetBestTime(levelIndex);
        if (bestTime != float.MaxValue)
        {
            bestTimeText.text = "Best Time: " + FormatTime(bestTime);
        }
        else
        {
            bestTimeText.text = "Best Time: N/A";
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
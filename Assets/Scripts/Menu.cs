using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject BlackScreen;
    public Text text;

    public void Start()
    {
        BlackScreen.SetActive(false);
    }
    public void OnPlayButton()
    {
        BlackScreen.SetActive(true);
        //StartCoroutine(FadeTextToFullAlpha(1f, text));
        SceneManager.LoadScene(1);
    }

    // from https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}

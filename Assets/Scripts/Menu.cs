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
        SceneManager.LoadScene(5);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject music;
    public GameOverScreen gameOverScreen;
    void Start()
    {
        music.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverScreen.GameOver == true)
        {
            music.SetActive(false);
        }
    }
}

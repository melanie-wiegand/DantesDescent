using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelAbandonHopeController : MonoBehaviour
{
    public Canvas canvas;
    public float fadeDuration = 3f;

    void Start()
    {
        SetInitialAlpha(canvas, 1f);
        StartCoroutine(FadeCanvas(canvas, fadeDuration));
    }

    // Set the initial alpha value for all Graphic components within the Canvas
    private void SetInitialAlpha(Canvas canvas, float alpha)
    {
        Graphic[] graphics = canvas.GetComponentsInChildren<Graphic>();
        foreach (Graphic graphic in graphics)
        {
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }

    private IEnumerator FadeCanvas(Canvas canvas, float duration)
    {
        Graphic[] graphics = canvas.GetComponentsInChildren<Graphic>();

        // Store the initial alpha value of all the graphics
        float[] startAlphas = new float[graphics.Length];
        for (int i = 0; i < graphics.Length; i++)
        {
            startAlphas[i] = graphics[i].color.a;
        }

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Interpolate the alpha value for each graphic
            foreach (Graphic graphic in graphics)
            {
                Color color = graphic.color;
                color.a = Mathf.Lerp(startAlphas[0], 0f, t); // Fade to transparent
                graphic.color = color;
            }

            yield return null;
        }

        // Disable the Canvas after fading
        canvas.gameObject.SetActive(false);
    }
}

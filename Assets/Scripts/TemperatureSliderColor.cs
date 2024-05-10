using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureSliderColor : MonoBehaviour
{
    public Slider TemperatureSlider;
    public Image TemperatureSliderFill;
    public Color orangeColor = new Color(1f, 0.5f, 0f);
    public Color blueColor = new Color(73f / 255f, 132f / 255f, 184f / 255f);
 
    // Update is called once per frame
    void Update()
    {
        float sliderValue = TemperatureSlider.value;

        Color lerpedColor = Color.Lerp(blueColor, orangeColor, sliderValue / 1f);

        TemperatureSliderFill.color = lerpedColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorSelection : MonoBehaviour
{
    Color color;

    public Slider sliderRed;
    public Slider sliderGreen;
    public Slider sliderBlue;
    public Image preview;
    public DrawScript drawScript;

	void Update ()
    {
        color = new Color(sliderRed.value, sliderGreen.value, sliderBlue.value, 1);
        if (preview)
            preview.color = color;
        if (drawScript)
            drawScript.color = color;
	}
}

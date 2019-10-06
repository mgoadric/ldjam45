using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public bool canFade;
    private Color alphaColor;
    public float timeToFade = 1.0f;

    public void Start()
    {
        canFade = false;
        alphaColor = GetComponent<SpriteRenderer>().material.color;
        alphaColor.a = 0;
    }
    public void Update()
    {
        if (canFade && GetComponent<SpriteRenderer>().material.color.a > 0.01f)
        {
            GetComponent<SpriteRenderer>().material.color = Color.Lerp(GetComponent<SpriteRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
        }
    }

}

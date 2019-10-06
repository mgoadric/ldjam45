using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private bool canFade;
    private Color alphaColor;
    private float timeToFade = 1.0f;

    public void Start()
    {
        canFade = false;
        alphaColor = GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;
    }
    public void Update()
    {
        if (canFade)
        {
            GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
        }
    }

}

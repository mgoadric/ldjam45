using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private Color alphaColor;
    public float timeToFade = 1.0f;

    public void Start()
    {
        alphaColor = GetComponent<SpriteRenderer>().material.color;
        alphaColor.a = 0;
    }

    public void Update()
    {

    }

    IEnumerator Fade()
    {
        while (GetComponent<SpriteRenderer>().material.color.a > 0.01f)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            GetComponent<SpriteRenderer>().material.color = Color.Lerp(GetComponent<SpriteRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
        }
        Destroy(this.gameObject);
    }

    public void FadeMe()
    {
        StartCoroutine("Fade");
    }

}

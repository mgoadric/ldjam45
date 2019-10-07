using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    private Color alphaColor;
    public float timeToFade = 1.0f;

    public void Start()
    {
        alphaColor = GetComponent<SpriteRenderer>().color;
    }

    public void Update()
    {

    }

    IEnumerator Fade()
    {
        Debug.Log("Fade Start: " + Mathf.Abs(GetComponent<SpriteRenderer>().color.a - alphaColor.a));
        while (Mathf.Abs(GetComponent<SpriteRenderer>().color.a - alphaColor.a) > 0.01f)
        {
            Debug.Log("Fade Middle: " + Mathf.Abs(GetComponent<SpriteRenderer>().color.a - alphaColor.a));
            yield return new WaitForSeconds(Time.deltaTime);
            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, alphaColor, timeToFade * Time.deltaTime);
        }
        if (alphaColor.a == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void FadeMe(int newAlpha)
    {
        alphaColor.a = newAlpha;
        StartCoroutine("Fade");
    }

}

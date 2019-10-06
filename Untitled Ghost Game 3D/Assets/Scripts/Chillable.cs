using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chillable : MonoBehaviour
{

    private float stayCount = 0.0f;
    public bool chilled;
    public bool whispered;
    private AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger for chilling!");
        stayCount = 0f;
        if (!chilled)
        {
            audioData.Play(0);
        }
    }

    // stayCount allows the OnTriggerStay to be displayed less often
    // than it actually occurs.
    private void OnTriggerStay(Collider other)
    {
        stayCount = stayCount + Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!chilled)
        {
            Debug.Log("Just chilled for " + stayCount);
            if (stayCount > 3.0f)
            {
                chilled = true;
            }
            else
            {
                audioData.Stop();
            }
            } else if (!whispered)
        {
            Debug.Log("Just whispered for " + stayCount);
            whispered |= stayCount > 5.0f;
        }
    }
}

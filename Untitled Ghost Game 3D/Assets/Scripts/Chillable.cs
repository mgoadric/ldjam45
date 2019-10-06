using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloatState { WHISPERING, CHILLING, NONE};

public class Chillable : MonoBehaviour
{

    private float stayCount = 0.0f;
    public bool chilled;
    public bool whispered;
    public FloatState mystate;
    private AudioSource audioData;
    public AudioClip chill;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        mystate = FloatState.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger for whispers and chilling!");
        stayCount = 0f;
        if (!whispered)
        {
            audioData.Play(0);
            mystate = FloatState.WHISPERING;
        } else if (!chilled) {
            audioData.PlayOneShot(chill);
            mystate = FloatState.CHILLING;
        }
    }

    // stayCount allows the OnTriggerStay to be displayed less often
    // than it actually occurs.
    private void OnTriggerStay(Collider other)
    {
        stayCount = stayCount + Time.deltaTime;
        if (mystate == FloatState.WHISPERING && !whispered)
        {
            if (stayCount > 3.0f)
            {
                whispered = true;
            }
        } else if (mystate == FloatState.CHILLING && !chilled)
        {
            if (stayCount > 5.0f)
            {
                chilled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        audioData.Stop();
        mystate = FloatState.NONE;
    }
}

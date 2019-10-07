using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFinder : MonoBehaviour
{
    private float stayCount = 0.0f;
    public bool hasKeys;
    private AudioSource audioData;
    public AudioClip chill;
    public bool inCar;

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
        if (other.gameObject.layer == 11)
        {
            Debug.Log("Trigger for key finding!");
            stayCount = 0f;
            if (!hasKeys)
            {
                audioData.PlayOneShot(chill);
            }
        }
    }

    // stayCount allows the OnTriggerStay to be displayed less often
    // than it actually occurs.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            stayCount = stayCount + Time.deltaTime;
            if (!hasKeys)
            {
                if (stayCount > 1.0f)
                {
                    hasKeys = true;
                    Destroy(other.gameObject);
                }
            }
        }
    }
}

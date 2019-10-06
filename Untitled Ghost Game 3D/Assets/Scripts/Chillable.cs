using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chillable : MonoBehaviour
{

    private float stayCount = 0.0f;
    public bool chilled;
    public bool whispered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        stayCount = 0;
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
            chilled |= stayCount > 3.0f;
        } else if (!whispered)
        {
            whispered |= stayCount > 5.0f;
        }
    }
}

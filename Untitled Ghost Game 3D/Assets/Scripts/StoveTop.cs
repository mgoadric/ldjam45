using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveTop : MonoBehaviour
{

    public bool cooked;
    private float stayCount;

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
        if (other.gameObject.layer == 12)
        {
            Debug.Log("Trigger for stovetop!");
            stayCount = 0f;
        }
    }

    // stayCount allows the OnTriggerStay to be displayed less often
    // than it actually occurs.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            stayCount = stayCount + Time.deltaTime;
            if (!cooked)
            {
                if (stayCount > 3.0f)
                {
                    cooked = true;
                }
            }
        }
    }
}

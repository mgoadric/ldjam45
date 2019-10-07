﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public GameObject fireEffect;
    public float xOffset, yOffset, zOffset;
    private Transform tf;
    private bool flameOn=false;
    public bool cooked;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    public void OnPush()
    {
        if(!flameOn)
        {
            Vector3 effectSource = tf.position;
            fireEffect = Instantiate(fireEffect, effectSource, Quaternion.identity);
            flameOn = true;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public void flameOff()
    {
        Destroy(fireEffect);
    }
}

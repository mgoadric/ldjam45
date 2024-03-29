﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    private Transform tf;

    public float hauntRadius;

    private bool button1Prev;
    private float button1HoldStart;
    public float button1HoldScaled;

    public bool pushUnlocked;
    public float pushStrength;
    public float pushRadius;
    public float pushCD;
    private float nextPush;
    private bool canPush;
    public GameObject pushEffect;

    public bool holdUnlocked;
    public bool isHolding;
    public float holdStrength;
    public float holdRadius;
    public float holdDelay;
    public GameObject[] grabbedObjects;
    public GameObject[] grabAnchors;
    public GameObject[] grabParticles;
    public GameObject grabEffect;
    
    
    public bool button2Active;
    private bool button2Prev;
    private float button2HoldStart;
    public float button2HoldScaled;

    private AudioSource audioData;

    public AudioClip pushSound;


    


    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        button1HoldScaled = -1;
        button2HoldScaled = -1;
        canPush = true;
        nextPush = 0;
        pushUnlocked = true;
        holdUnlocked = true;
        button2Active = true;
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextPush)
        {
            canPush = true;
        }
    }

    public void getInput(bool button1, bool button2)
    {
        if (!button1Prev && button1)
            Button1Down();
        else if (button1Prev && button1)
            Button1Held();
        else if (button1Prev && !button1)
            Button1Up();
        else
            Button1NoInput();

        if (!button2Prev && button2)
            Button2Down();
        else if (button2Prev && button2)
            Button2Held();
        else if (button2Prev && !button2)
            Button2Up();
        else
            Button2NoInput();

        button1Prev = button1;
        button2Prev = button2;
    }



    public void NoPush()
    {
        canPush = true;
    }

    public void SpacePressed()
    {
        if (canPush)
        {
            Push();
        }
    }

    private void Button1Down()
    {
        //Debug.Log("Button1Down()");
        button1HoldStart = Time.time;
        if(pushUnlocked)
        {
            if (canPush)
            {
                Push();
                nextPush = Time.time + pushCD;
                canPush = false;
            }
        }
    }

    private void Button1Held()
    {
        //Debug.Log("Button1Held()");

        button1HoldScaled = Time.time - button1HoldStart;
        if (holdUnlocked)
        {
            if (button1HoldScaled > holdDelay)
            {
                if (!isHolding)
                {
                    Grab();
                }
                else
                {
                    Hold();
                }
            }
        }
    }

    private void Button1Up()
    {
        //Debug.Log("Button1Up()");
        button1HoldScaled = -1;
        resetGrabAnchors();
        if (isHolding)
        {
            audioData.Stop();
        }
    }

    public void Button1NoInput()
    {
        isHolding = false;
        if (Time.time > nextPush)
        {
            canPush = true;
        }

    }

    private void Button2Down()
    {
        Debug.Log("Button2Down()");
        button2HoldStart = Time.time;
    }

    private void Button2Held()
    {
        Debug.Log("Button2Held()");
        button2HoldScaled = Time.time - button2HoldStart;
    }

    private void Button2Up()
    {
        Debug.Log("Button2Up()");
        button2HoldScaled = -1;

    }

    public void Button2NoInput()
    {

    }

    private void Push()
    {
        audioData.PlayOneShot(pushSound);
        GameObject particles = Instantiate(pushEffect, transform.position, Quaternion.identity);
        Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration);
        Debug.Log(message: "Pushing, radius=" + pushRadius);
        Collider[] hitColliders = Physics.OverlapSphere(tf.position, pushRadius);
        int i = 0;
        string objectForces = "";
        while (i < hitColliders.Length)
        {
            GameObject hitObject = hitColliders[i].gameObject;
            Vector3 heading = hitObject.transform.position - tf.position;
            var distance = heading.magnitude;
            if (hitObject.tag == "Moveable")
            {
                Vector3 pushForce = (heading / distance * (1 / distance) * pushStrength);
                hitObject.GetComponent<Rigidbody>().AddForce(pushForce);
                objectForces += " hit " + hitObject.name + " with force " + pushForce.magnitude;
            }
            else if (hitColliders[i].gameObject.tag == "ActionOnPush")
            {
                Debug.Log(message: "ActionOnPush Found!");
                hitColliders[i].gameObject.GetComponent<FridgeDoor>().OnPush();
            }
            else if (hitColliders[i].gameObject.tag == "Stovetop")
            {
                Debug.Log(message: "Stovetop Found!");
                hitColliders[i].gameObject.GetComponent<Stove>().OnPush();
            }
            i++;
        }
        if(objectForces != "")
            Debug.Log(message: objectForces);
    }

    private void Grab()
    {
        audioData.Play();
        isHolding = true;
        Collider[] holdColliders = Physics.OverlapSphere(tf.position, holdRadius);
        int i = 0;
        int k = 0;
        while (i < holdColliders.Length)    
        {   
            if (holdColliders[i].gameObject.tag == "Moveable")
                k++;
            i++;
        }
        Debug.Log(message: "i=" + i + " k=" + k);
        grabbedObjects = new GameObject[k];
        grabAnchors = new GameObject[k];
        grabParticles = new GameObject[k];

        i = 0;
        k = 0;
        while (i < holdColliders.Length)
        {
            if (holdColliders[i].gameObject.tag == "Moveable")
            {
                GameObject hitObject = holdColliders[i].gameObject;
                GameObject grabPoint = new GameObject(("grab_" + hitObject.name));
                GameObject grabParticle = Instantiate(grabEffect, hitObject.transform.position,Quaternion.identity);
                grabPoint.transform.position = hitObject.transform.position;
                grabPoint.transform.parent = tf;
                grabbedObjects[k] = hitObject;
                grabAnchors[k] = grabPoint;
                grabParticles[k] = grabParticle;
                k++;
            }
            
            i++;
        }     
    }

    private void Hold()
    {
        if(grabbedObjects.Length==grabAnchors.Length)
        {
            int i = 0;
            while(i<grabAnchors.Length)
            {
                GameObject grabbed = grabbedObjects[i];
                GameObject anchor = grabAnchors[i];
                grabParticles[i].GetComponent<Transform>().position = grabbed.transform.position;
                Vector3 heading = anchor.transform.position - grabbed.transform.position;
                var distance = heading.magnitude;
                Vector3 holdForce = -heading*(.05f*distance);
                grabbedObjects[i].GetComponent<Rigidbody>().AddForce(heading);


                i++;
            }
        }
    }

    private void resetGrabAnchors()
    {
        int i = 0;
        while(i<grabAnchors.Length)
        {
            Destroy(grabParticles[i]);
            Destroy(grabAnchors[i]);
            i++;
        }
    }
}

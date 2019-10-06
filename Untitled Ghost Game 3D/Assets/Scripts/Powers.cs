using System.Collections;
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

    public bool holdUnlocked;
    public bool isHolding;
    public float holdStrength;
    public float holdRadius;
    public float holdDelay;
    public GameObject[] grabbedObjects;
    public GameObject[] grabAnchors;
    
    
    public bool button2Active;
    private bool button2Prev;
    private float button2HoldStart;
    public float button2HoldScaled;






    public GameObject pushEffect;


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
        if (button1HoldScaled > holdDelay)
        {
            if (!isHolding)
            {
                Grab();
            }
            else
            {

            }
        }
    }

    private void Button1Up()
    {
        //Debug.Log("Button1Up()");
        button1HoldScaled = -1;


        if (Time.time > nextPush)
        {
            canPush = true;
        }

    }

    public void Button1NoInput()
    {
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
            i++;
        }
        Debug.Log(message: objectForces);
    }

    private void Grab()
    {
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
        i = 0;
        k = 0;

        GameObject hitObject = holdColliders[i].gameObject;
        GameObject grabPoint = new GameObject("grab_" + hitObject.name);
        grabAnchors[k] = new GameObject();
        grabPoint.transform.parent = tf;
    }
}

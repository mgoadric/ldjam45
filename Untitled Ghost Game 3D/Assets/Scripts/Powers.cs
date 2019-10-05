using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    private Transform tf;
    public bool button1Prev;
    private float button1HoldTime;
    public float button1HoldScaled;

    private bool button2Prev;
    private float button2HoldTime;
    public float button2HoldScaled;


    public float hauntRadius;

    public float pushStrength;
    public float pushRadius;
    public float pushCD;
    private float nextPush;
    private bool canPush;


    public GameObject pushEffect;


    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        canPush = true;
        nextPush =0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getInput(bool button1, bool button2)
    {
        if (!button1Prev && button1)
            Button1Down();
        if(button1Prev && button1)
            Button1Held();
        if (button1Prev && !button1)
            Button1Up();

        if (!button2Prev && button2)
            Button2Down();
        if (button2Prev && button2)
            Button2Held();
        if (button2Prev && !button2)
            Button2Up();

        if (!button1 && !button2)
            NoInput();
        button1Prev = button1;
        button2Prev = button2;
    }

    public void NoInput()
    {
        if(Time.time > nextPush)
        {
            canPush = true;
        }
        
    }

    public void NoPush()
    {
        canPush = true;
    }

    public void SpacePressed()
    {
        if(canPush)
        {
            Push();
        }
    }

    private void Button1Down()
    {
        Debug.Log("Button1Down()");
        Push();
    }

    private void Button1Held()
    {
        Debug.Log("Button1Held()");
    }

    private void Button1Up()
    {
        Debug.Log("Button1Up()");
        if (Time.time > nextPush)
        {
            canPush = true;
        }

    }

    private void Button2Down()
    {
        Debug.Log("Button2Down()");
    }

    private void Button2Held()
    {
        Debug.Log("Button2Held()");
    }
    
    private void Button2Up()
    {
        Debug.Log("Button2Up()");
    }

    public void Push()
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
        nextPush = Time.time + pushCD;
        canPush = false;
    }
}

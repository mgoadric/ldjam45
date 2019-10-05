using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    private Transform tf;

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

    public void Push()
    {    
        Instantiate(pushEffect, transform.position, Quaternion.identity);
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

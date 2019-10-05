using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    private Rigidbody rb;
    public float moveSpeed;
    public float pushStrength;
    public float pushRadius;
    private bool hasPushed;
    public GameObject pushEffect;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hasPushed = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // https://answers.unity.com/questions/1373810/how-to-move-the-character-using-wasd.html
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector3.left * moveSpeed);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector3.right * moveSpeed);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(Vector3.forward * moveSpeed);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(Vector3.back * moveSpeed);
        if (Input.GetKey(KeyCode.Space))
        {
            Push(pushRadius);
        } else
        {
            hasPushed = false;
        }

    }

    private void Push(float radius)
    {        
        if(!hasPushed)
        {
            Instantiate(pushEffect, transform.position, Quaternion.identity);
            Debug.Log(message: "Pushing, radius=" + radius);
            Collider[] hitColliders = Physics.OverlapSphere(rb.position, radius);
            int i = 0;
            string objectForces = "";
            while (i < hitColliders.Length)
            {
                GameObject hitObject = hitColliders[i].gameObject;
                Vector3 heading = hitObject.transform.position - rb.position;
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
            hasPushed = true;
        }
    }
}

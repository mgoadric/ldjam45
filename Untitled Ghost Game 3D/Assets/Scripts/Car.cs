using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    private NavMeshAgent agent;

    public bool inCar;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding with layer " + other.gameObject.layer);
        if (other.gameObject.layer == 0 && other.gameObject.tag == "Resident")
        {
            Debug.Log("Trigger for Car!");
            if (!inCar)
            {
                Debug.Log("Now it is true");
                inCar = true;
            }
        }
    }

    public void SetDestination(Transform dest)
    {
        agent.destination = dest.position;
    }
}

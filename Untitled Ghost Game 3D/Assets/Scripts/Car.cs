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
        if (other.gameObject.layer == 10)
        {
            Debug.Log("Trigger for Car!");
            if (!inCar)
            {
                inCar = true;
            }
        }
    }

    public void SetDestination(Transform dest)
    {
        agent.destination = dest.position;
    }
}

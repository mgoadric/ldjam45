using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Resident : MonoBehaviour
{
    public Transform goal1;
    public Transform goal2;
    private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {   
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal1.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, goal1.position));
        // Just trying to get simple back and forth working.
        if (Vector3.Distance(transform.position, goal1.position) < 1.0f)
        {
            agent.destination = goal2.position;
        } else if (Vector3.Distance(transform.position, goal2.position) < 1.0f)
        {
            agent.destination = goal1.position;
        }
    }
}

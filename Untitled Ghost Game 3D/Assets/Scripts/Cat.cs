using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour
{

    public GameObject spirit;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Transform dest)
    {
        agent.destination = dest.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

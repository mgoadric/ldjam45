using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour
{

    public GameObject spirit;
    private NavMeshAgent agent;

    private IEnumerator coroutine;


    public void Waffle(Transform goal1, Transform goal2)
    {
        coroutine = PingPong(goal1, goal2);
        StartCoroutine(coroutine);
    }

    public void StopWaffle()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator PingPong(Transform goal1, Transform goal2)
    {
        agent.destination = goal1.position;
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log(Vector3.Distance(transform.position, goal1.position));

            // Just trying to get simple back and forth working.
            if (Vector3.Distance(transform.position, goal1.position) < 1.0f)
            {
                agent.destination = goal2.position;
            }
            else if (Vector3.Distance(transform.position, goal2.position) < 1.0f)
            {
                agent.destination = goal1.position;
            }
        }
    }

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

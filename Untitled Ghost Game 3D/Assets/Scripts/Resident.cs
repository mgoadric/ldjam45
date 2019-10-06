using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Resident : MonoBehaviour
{
    public Transform goal1;
    public Transform goal2;
    private NavMeshAgent agent;

    public GameObject dialogBubble;

    private bool talking;

    public void Dialog(string line, float duration)
    {
        if (!talking)
        {
            talking = true;

            // make the resident dialog bubble appear
            GameObject bar = Instantiate(dialogBubble, this.transform);

            // add the text line for duration
            GameObject go = bar.transform.GetChild(0).gameObject;
            TextMeshPro tmpugui = go.GetComponent<TextMeshPro>();
            tmpugui.SetText(line);

            // make it disappear 
            StartCoroutine(RemoveDialog(bar, duration));
        }
    }

    IEnumerator RemoveDialog(GameObject go, float duration)
    {
        yield return new WaitForSeconds(duration);
        talking = false;
        Destroy(go);
    }

    public void SetDestination(Transform dest)
    {
        agent.destination = dest.position;
    }

    // Start is called before the first frame update
    void Start()
    {   
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal1.position;
        talking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, goal1.position));
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

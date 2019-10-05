using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { START, MIDDLE, END }

public class Manager : MonoBehaviour
{

    public State gameState;

    public GameObject goalbar;
    private List<GameObject> currentGoals;

    public GameObject instructions;

    public static Manager S;

    private void Awake()
    {
        S = this;
        Debug.Log("Starting");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WipeBar()
    {
        // destroy the goals and remove from currentGoals
    }

    void AddGoal(string goal)
    {
        GameObject bar = Instantiate(goalbar);
        Vector3 temp = bar.transform.position;
        temp.y += 50 * currentGoals.Count;
        bar.transform.position = temp;
        // add in the goal text

        currentGoals.Add(bar);
    }

    public void StartGame()
    {
        currentGoals = new List<GameObject>();
        AddGoal("Wake up Resident");

        StartCoroutine("GameScript");

    }

    IEnumerator GameScript()
    {

        // SETUP

        // WAIT FOR PLAYER TO DO SOMETHING

        while (gameState == State.START)
        {
            yield return new WaitForSeconds(0.05f);
        }

    }
}

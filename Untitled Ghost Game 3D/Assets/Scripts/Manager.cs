using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { START, MIDDLE, END }

public class Manager : MonoBehaviour
{

    public GameObject gamePrefab;
    public State gameState;
    public GameObject game;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void AddGoal()
    {

    }

    public void StartGame()
    {
        if (game)
        {
            Destroy(game);
        }
        game = Instantiate(gamePrefab);
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

﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum State { START, MIDDLE, END }

public class Manager : MonoBehaviour
{

    public State gameState;

    public GameObject canvas;
    public GameObject goalbar;
    public Sprite completedGoalSprite;
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
        if (Input.GetKeyDown(KeyCode.Y))
            AddGoal("Do Next Thing");
        if (Input.GetKeyDown(KeyCode.U))
            WipeBar();
    }

    void WipeBar()
    {
        lock (this)
        {
            // destroy the goals and remove from currentGoals
            foreach (GameObject go in currentGoals)
            {
                Destroy(go);
            }
            currentGoals.Clear();
        }
    }

    void AddGoal(string goal)
    {
        lock (this)
        {
            // make previous goal dark
            if (currentGoals.Count > 0)
            {
                GameObject prev = currentGoals[currentGoals.Count - 1];
                prev.GetComponent<Image>().sprite = completedGoalSprite;
            }

            GameObject bar = Instantiate(goalbar, canvas.transform);
            Vector3 temp = bar.transform.position;

            float shrink = Screen.height / 900f; 

            temp.y -= 65 * shrink * currentGoals.Count;
            bar.transform.position = temp;

            // add in the goal text
            GameObject go = bar.transform.GetChild(0).gameObject;
            TextMeshProUGUI tmpugui = go.GetComponent<TextMeshProUGUI>();
            tmpugui.SetText(goal);

            currentGoals.Add(bar);
            Debug.Log("Added in " + goal);
        }
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum State { WHISPER, CHILL, CATWHISPER, CATCHILL, OPENFRIDGE, COOK, KEYS, CAR, END }

public class Manager : MonoBehaviour
{

    public State gameState;

    public GameObject canvas;
    public GameObject goalbar;
    public Sprite completedGoalSprite;
    private List<GameObject> currentGoals;

    public GameObject introscreen;
    public GameObject instructions;
    public GameObject goaltop;

    public GameObject pulseInfoGui;
    public GameObject holdInfoGui;

    public GameObject cat;
    public GameObject resident;
    public GameObject ghost;
    public GameObject fridge;

    public GameObject showerLoc;
    public GameObject bed1;
    public GameObject bed2;
    public GameObject stoveloc;
    public GameObject downstairs1;
    public GameObject carloc;
    public GameObject stairs;
    public GameObject stovetop;

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
        /*
        if (Input.GetKeyDown(KeyCode.Y))
        {
            AddGoal("Do Next Thing");
            resident.GetComponent<Resident>().Dialog("what was\nthat?", 2f);
        }
        if (Input.GetKeyDown(KeyCode.U))
            WipeBar();
        if (Input.GetKeyDown(KeyCode.O))
            pulseInfoGui.SetActive(true);
        if (Input.GetKeyDown(KeyCode.P))
            holdInfoGui.SetActive(true);
            */          
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

        StartCoroutine("GameScript");

    }

    IEnumerator GameScript()
    {

        // SETUP
        ghost.transform.GetChild(0).GetComponent<Powers>().pushUnlocked = false;
        ghost.transform.GetChild(0).GetComponent<Powers>().holdUnlocked = false;

        yield return new WaitForSeconds(2f);
        introscreen.GetComponent<Fader>().FadeMe();

        yield return new WaitForSeconds(2f);

        // make gui show up here!
        instructions.SetActive(true);
        goaltop.SetActive(true);

        // WAKE UP THE PLAYER
        AddGoal("Wake up resident");
        resident.GetComponent<Resident>().Dialog("ZZzzZ\nzzZZZzZ", 2f);


        pulseInfoGui.SetActive(true);
        pulseInfoGui.GetComponent<TextMeshProUGUI>().SetText("Hover to whisper");

        while (gameState == State.WHISPER)
        {
            yield return new WaitForSeconds(0.05f);
            if (resident.GetComponent<Resident>().spirit.GetComponent<Chillable>().whispered)
            {
                gameState = State.CHILL;
                AddGoal("Chill the resident");
                resident.GetComponent<Resident>().Dialog("Who's\nthere?", 4f);
            }
        }

        holdInfoGui.SetActive(true);
        holdInfoGui.GetComponent<TextMeshProUGUI>().SetText("and to chill");

        resident.GetComponent<Resident>().Waffle(bed1.transform, bed2.transform);

        while (gameState == State.CHILL)
        {
            yield return new WaitForSeconds(0.05f);
            if (resident.GetComponent<Resident>().spirit.GetComponent<Chillable>().chilled)
            {
                gameState = State.CATWHISPER;
                AddGoal("Wake the cat");
                resident.GetComponent<Resident>().Dialog("SOOO\nCOOLD!!", 4f);
            }

        }

        resident.GetComponent<Resident>().StopWaffle();
        resident.GetComponent<Resident>().SetDestination(showerLoc.transform);

        cat.GetComponent<Cat>().spirit.GetComponent<BoxCollider>().enabled = true;
        while (gameState == State.CATWHISPER)
        {
            yield return new WaitForSeconds(0.05f);
            if (cat.GetComponent<Cat>().spirit.GetComponent<Chillable>().whispered)
            {
                gameState = State.CATCHILL;
                AddGoal("Chill the cat");
                resident.GetComponent<Resident>().Dialog("Tabi,\nyou ok?", 4f);
                cat.GetComponent<AudioSource>().Play(0);
            }
        }

        resident.GetComponent<Resident>().SetDestination(bed2.transform);
        cat.GetComponent<Cat>().Waffle(downstairs1.transform, stairs.transform);

        while (gameState == State.CATCHILL)
        {
            yield return new WaitForSeconds(0.05f);
            if (cat.GetComponent<Cat>().spirit.GetComponent<Chillable>().chilled)
            {
                gameState = State.OPENFRIDGE;
                WipeBar();
                AddGoal("Open the fridge");
                resident.GetComponent<Resident>().Dialog("Come here\nTabi!", 4f);
            }
        }

        cat.GetComponent<Cat>().StopWaffle();
        cat.GetComponent<Cat>().SetDestination(resident.transform);
        holdInfoGui.SetActive(false);
        pulseInfoGui.GetComponent<TextMeshProUGUI>().SetText("Tap J to Push");


        // activate pulse
        ghost.transform.GetChild(0).GetComponent<Powers>().pushUnlocked = true;
        pulseInfoGui.SetActive(true);

        while (gameState == State.OPENFRIDGE)
        {
            yield return new WaitForSeconds(0.05f);
            if (fridge.GetComponent<Animator>().GetBool("pushed"))
            {
                gameState = State.COOK;
                AddGoal("Cook a meal");
                resident.GetComponent<Resident>().Dialog("More noises?", 4f);
            }
        }

        resident.GetComponent<Resident>().SetDestination(stairs.transform);
        cat.GetComponent<Cat>().SetDestination(downstairs1.transform);

        // activate hold
        ghost.transform.GetChild(0).GetComponent<Powers>().holdUnlocked = true;
        holdInfoGui.SetActive(true);
        holdInfoGui.GetComponent<TextMeshProUGUI>().SetText("Hold J to Grab");

        while (gameState == State.COOK)
        {
            yield return new WaitForSeconds(0.05f);
            // finished when can cooking on stove
            if (stovetop.GetComponent<Stove>().cooked)
            {
                gameState = State.KEYS;
                AddGoal("Find keys");
                resident.GetComponent<Resident>().Dialog("Smells\ngood!", 4f);
            }

        }

        // resident goes to stove and eats
        resident.GetComponent<Resident>().SetDestination(stoveloc.transform);
        cat.GetComponent<Cat>().SetDestination(showerLoc.transform);

        yield return new WaitForSeconds(5f);
        resident.GetComponent<Resident>().Dialog("Yum!", 4f);
        yield return new WaitForSeconds(5f);
        resident.GetComponent<Resident>().Dialog("Where are\nmy keys?", 4f);

        // then they try to find their car keys
        resident.GetComponent<Resident>().Waffle(bed1.transform, downstairs1.transform);


        while (gameState == State.KEYS)
        {
            yield return new WaitForSeconds(0.05f);
            // finished when can cooking on stove
            if (resident.GetComponent<Resident>().spirit.GetComponent<Chillable>().hasKeys)
            {
                gameState = State.CAR;
                AddGoal("Good work!");
                resident.GetComponent<Resident>().Dialog("Goodbye\nghost?", 4f);
            }

        }

        // keys are found, get in car and head out
        resident.GetComponent<Resident>().StopWaffle();

        resident.GetComponent<Resident>().SetDestination(carloc.transform);

        while (gameState == State.CAR)
        {
            yield return new WaitForSeconds(0.05f);
            // finished when can cooking on stove
            if (resident.GetComponent<Resident>().spirit.GetComponent<Chillable>().inCar)
            {
                gameState = State.END;
                AddGoal("You made a friend!");
                resident.GetComponent<Resident>().Dialog("That was\nweird.", 4f);
            }

        }

    }
}

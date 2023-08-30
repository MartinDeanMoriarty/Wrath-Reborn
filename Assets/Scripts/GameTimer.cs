using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameTimer : MonoBehaviour
{
    private float timeRemaining = 180; // 3 minutes in seconds
    GameController GameController;
    public string GameCounterString; 
    public GUIStyle TextStyle;
    [HideInInspector] public Rect Button;
    TitleScreen TitleScreen;
    public bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        GameController = gameObject.GetComponent<GameController>();
        TitleScreen = gameObject.GetComponent<TitleScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.payingGame)
        {
            //StartCoroutine(CountdownTimer());
            timerIsRunning = true;
        }
        else
        {
            timerIsRunning = false;
            //StopCoroutine(CountdownTimer());
        }

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                GameController.payingGame = false;
                TitleScreen.EndGame();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        GameCounterString = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnGUI()
    {
        if (GameController.payingGame)
        {
            Button = new Rect(Screen.width/2-50,0,0,0);
            GUI.Button(Button, GameCounterString, TextStyle);
        }  
    }
}

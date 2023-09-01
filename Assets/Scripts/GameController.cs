using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game settings")]
    [Tooltip("The variations of sheep to spawn")]
    public List<GameObject> objectsToSpawn;
    [Tooltip("All the spawnpoints")]
    public List<GameObject> SpawnPoint;
    [Header("Hit Counter")]
    [Tooltip("Text Style and background")]
    public GUIStyle TextStyle;
    [HideInInspector] public Rect Button;
    [Header("Sheep counting stuff")]
    [Tooltip("Maximum number of sheeps at the same time")]
    public int maxSheeps = 10;
    [Header("DoNotChange")]
    public int sheepCount = 0;
    public int SheepHits = 0;
    public string CountString;
    public bool playingGame = false;
    public int maxRoundHits = 0;
    public int allHits = 0;

    private void Start()
    {
        CountString = SheepHits.ToString();
        playingGame = false;

        //Load highscores with PlayerPrefs
        int defaultValue = 0; // Replace this with a default value if the keys do not exist        
        maxRoundHits = PlayerPrefs.GetInt("Round", defaultValue);
        allHits = PlayerPrefs.GetInt("All", defaultValue);
    }
    void SpawnSheepRandomly()
    {
        if (sheepCount < maxSheeps)
        {
            for (int i = 0; i < SpawnPoint.Count; i++)
            {
                Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Count)], SpawnPoint[i].transform.position, Quaternion.identity);
                sheepCount++;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SpawnSheepRandomly();
    }

    void OnGUI()
    {
        if (playingGame)
        {
            CountString = "Hits: " + SheepHits.ToString();
            Button = new Rect();
            GUI.Button(Button, CountString, TextStyle);
        }
        else
        {
            CountString = "Round: " + maxRoundHits.ToString() + " | All: " + allHits.ToString();
            Button = new Rect();
            GUI.Button(Button, CountString, TextStyle);
        }
    }
}

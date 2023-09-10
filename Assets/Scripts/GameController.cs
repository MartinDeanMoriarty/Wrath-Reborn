using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GUIStyle ResetTextStyle;
    [HideInInspector] public Rect Button;
    [Header("Sheep counting stuff")]
    [Tooltip("Maximum number of sheeps at the same time")]
    public int maxSheeps = 10;
    [Header("DoNotChange")]
    public int sheepCount = 0;
    public int SheepHits = 0;
    public string RoundString;
    public string AllString;
    public bool playingGame = false;
    public int maxRoundHits = 0;
    public int allHits = 0;
    [HideInInspector] public Rect Medal;
    public GUIStyle BronzeMedal;
    public int BronzePoints = 50;
    public GUIStyle SilverMedal;
    public int SilverPoints = 80;
    public GUIStyle GoldMedal;
    public int GoldPoints = 100;
    public GUIStyle PlatinMedal;
    public int PlatinPoints = 130;
    GUIStyle ActualMedal;

    private void Start()
    {
        RoundString = SheepHits.ToString();
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
    void Update()
    {
        SpawnSheepRandomly();
        if (Input.GetKey(KeyCode.F9)) { ClearRoundHighscore(); }
        if (Input.GetKey(KeyCode.F12)) { ClearAllHighscore(); }
    }

    void OnGUI()
    {
        if (playingGame)
        {
            RoundString = "Hits: " + SheepHits.ToString();
            Button = new Rect(5, 5, 150, 0);
            GUI.Button(Button, RoundString, TextStyle);
        }
        else
        {
            if (maxRoundHits == 0) { ActualMedal = null; }
            if (maxRoundHits >= BronzePoints && maxRoundHits < SilverPoints) { ActualMedal = BronzeMedal; }
            if (maxRoundHits >= SilverPoints && maxRoundHits < GoldPoints) { ActualMedal = SilverMedal; }
            if (maxRoundHits >= GoldPoints && maxRoundHits < PlatinPoints) { ActualMedal = GoldMedal; }
            if (maxRoundHits >= PlatinPoints) { ActualMedal = PlatinMedal; }

            RoundString = "Round: " + maxRoundHits.ToString();
            AllString = " |    All: " + allHits.ToString();

            Button = new Rect(5, 5, 150, 0);
            GUI.Button(Button, RoundString, TextStyle);
            Button = new Rect(155, 5, 150, 0);
            GUI.Button(Button, AllString, TextStyle);

            Button = new Rect(5, 35, 0, 0);
            GUI.Button(Button, "Reset F9", ResetTextStyle);
            Button = new Rect(195, 35, 0, 0);
            GUI.Button(Button, "Reset F12", ResetTextStyle);

            if (ActualMedal != null)
            {
                Button = new Rect(0, 45, 128, 128);
                GUI.Button(Button, "", ActualMedal);
            }

        }
    }

    void ClearRoundHighscore()
    {
        PlayerPrefs.SetInt("Round", 0);   
        PlayerPrefs.Save(); // Make sure to save the changes

        //Realod Scene
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
    void ClearAllHighscore()
    {     
        PlayerPrefs.SetInt("All", 0);
        PlayerPrefs.Save(); // Make sure to save the changes

        //Realod Scene
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

}

using System.Collections;
using System.Collections.Generic;
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

    //Setting up the Medals

    public GUIStyle BronzeMedal;
    public int BronzePoints = 50;
    public bool gotBronze;

    public GUIStyle SilverMedal;
    public int SilverPoints = 80;
    public bool gotSilver;

    public GUIStyle GoldMedal;
    public int GoldPoints = 100;
    public bool gotGold;

    public GUIStyle PlatinMedal;
    public int PlatinPoints = 130;
    public bool gotPlatin;

    public bool hasAMedal;
    public GUIStyle ActualMedal;
    public GUIStyle WonMedal;

    AudioSource MedalSource;

    float WonMedalSizeW = 0;
    float WonMedalSizeH = 0;
    float initialScale = 0;
    float upScale = 128;

    float WonMedalPosX = Screen.width / 2 - 64;
    float WonMedalPosY = Screen.height / 2 - 64;
    float initialPosX = Screen.width / 2 - 64;
    float initialPosY = Screen.height / 2 - 64;
    float endPosX = 0;
    float endPosY = 45;

    float timeElapsed;
    float lerpDuration = 0.5f;


    private void Start()
    {
        RoundString = SheepHits.ToString();
        playingGame = false;

        //Load highscores with PlayerPrefs
        int defaultValue = 0; // Replace this with a default value if the keys does not exist        
        maxRoundHits = PlayerPrefs.GetInt("Round", defaultValue);
        allHits = PlayerPrefs.GetInt("All", defaultValue);
        MedalSource = GetComponent<AudioSource>();
        // Set allready won medal       
        SetActualMedal();
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

            Button = new Rect(0, 45, 128, 128);
            GUI.Button(Button, "", ActualMedal);

            Button = new Rect(WonMedalPosX, WonMedalPosY, WonMedalSizeW, WonMedalSizeH);
            GUI.Button(Button, "", WonMedal);

            CheckForNewMedal();
        }
        else
        {
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

            if (hasAMedal)
            {
                Button = new Rect(0, 45, 128, 128);
                GUI.Button(Button, "", ActualMedal);
            }
        }
    }

    void CheckForNewMedal()
    {
        if (SheepHits >= BronzePoints && !gotBronze) { gotBronze = true; WonMedal = BronzeMedal; MedalSource.Play(); StartCoroutine(MedalAnim()); }
        if (SheepHits >= SilverPoints && !gotSilver) { gotSilver = true; WonMedal = SilverMedal; MedalSource.Play(); StartCoroutine(MedalAnim()); }
        if (SheepHits >= GoldPoints && !gotGold) { gotGold = true; WonMedal = GoldMedal; MedalSource.Play(); StartCoroutine(MedalAnim()); }
        if (SheepHits >= PlatinPoints && !gotPlatin) { gotPlatin = true; WonMedal = PlatinMedal; MedalSource.Play(); StartCoroutine(MedalAnim()); }
    }

    IEnumerator MedalAnim()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            WonMedalSizeW = Mathf.Lerp(initialScale, upScale, timeElapsed / lerpDuration);
            WonMedalSizeH = Mathf.Lerp(initialScale, upScale, timeElapsed / lerpDuration);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        //WonMedalSizeW = upScale;
        //WonMedalSizeH = upScale;

        //yield return new WaitForSeconds(1);

        StartCoroutine(MedalAnim2());
    }
    IEnumerator MedalAnim2()
    {
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            WonMedalPosX = Mathf.Lerp(initialPosX, endPosX, timeElapsed / lerpDuration);
            WonMedalPosY = Mathf.Lerp(initialPosY, endPosY, timeElapsed / lerpDuration);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

         

        WonMedalSizeW = initialScale;
        WonMedalSizeH = initialScale;
        WonMedalPosX = initialPosX;
        WonMedalPosY = initialPosY;

        ActualMedal = WonMedal;
    }

    void SetActualMedal()
    {
        if (maxRoundHits >= 0 && maxRoundHits < BronzePoints) { hasAMedal = false; } else { hasAMedal = true; }
        if (maxRoundHits >= BronzePoints && maxRoundHits < SilverPoints) { ActualMedal = BronzeMedal; gotBronze = true; }
        if (maxRoundHits >= SilverPoints && maxRoundHits < GoldPoints) { ActualMedal = SilverMedal; gotBronze = true; gotSilver = true; }
        if (maxRoundHits >= GoldPoints && maxRoundHits < PlatinPoints) { ActualMedal = GoldMedal; gotBronze = true; gotSilver = true; gotGold = true; }
        if (maxRoundHits >= PlatinPoints) { ActualMedal = PlatinMedal; gotBronze = true; gotSilver = true; gotGold = true; gotPlatin = true; }
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

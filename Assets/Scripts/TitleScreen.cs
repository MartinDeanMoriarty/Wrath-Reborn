using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class TitleScreen : MonoBehaviour
{
    [Header("Title settings")]
    [Tooltip("Object that holds the title image")]
    public GameObject Title;
    [Tooltip("Player Object that holds the bold")]
    public GameObject Bold;
    [Tooltip("Object that holds all the white clouds")]
    public GameObject CloudsWhite;
    [Tooltip("Object that holds all the dark clouds")]
    public GameObject CloudsDark;
    [Tooltip("Audio1 Object")]
    public GameObject Audio1;
    [Tooltip("Audio2 Object")]
    public GameObject Audio2;
    [Tooltip("All the single white clouds")]
    public List<GameObject> WhiteClouds;
    [Tooltip("The main light source")]
    public Light MainLightSource;
    GameController GameController;
    int value1;
    int value2;
    bool startInput;

    private void Start()
    {
        GameController = gameObject.GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        //Hit Space to play
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0) && Title.activeSelf)
        {
            //Set fleeing=true in CloudController.cs
            for (int i = 0; i < WhiteClouds.Count; i++)
            {
                WhiteClouds[i].GetComponent<CloudController>().fleeing = true;
            }
            MainLightSource.intensity = 1;
            Title.SetActive(false);
            Audio1.SetActive(false);
            CloudsDark.SetActive(true);
            Audio2.SetActive(true);
            Bold.SetActive(true);
            GameController.playingGame = true;
        }

        //Hit Escape to END game
        if (Input.GetKey(KeyCode.Escape) && Title.activeSelf == false)
        {
            EndGame();
        }

        //Hit Escape to EXIT game
        if (Input.GetKey(KeyCode.Escape) && Title.activeSelf == true)
        {
            Application.Quit();
        }
    }

    //END game, not EXIT!
    public void EndGame()
    {
        //Set up what to save with PlayerPrefs
        if (GameController.SheepHits > GameController.maxRoundHits)
        {
            value1 = GameController.SheepHits;
        }
        else
        {
            value1 = GameController.maxRoundHits;
        }
        int value2 = GameController.allHits;

        PlayerPrefs.SetInt("Round", value1);
        PlayerPrefs.SetInt("All", value2);
        PlayerPrefs.Save(); // Make sure to save the changes

        //Realod Scene
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}

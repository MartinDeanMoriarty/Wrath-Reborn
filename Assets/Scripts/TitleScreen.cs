using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        GameController = gameObject.GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Title.activeSelf)
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
            GameController.payingGame = true;
        }

        if (Input.GetKey(KeyCode.Escape) && Title.activeSelf == false)
        {
            EndGame();
        }

        if (Input.GetKey(KeyCode.Escape) && Title.activeSelf == true)
        {
            Application.Quit();
        }

    }

    public void EndGame()
    {
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

        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}

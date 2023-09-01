using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCloudsEffect : MonoBehaviour
{
    [Header("Dark Clouds Effect")]
    [Tooltip("Max. flicker intervall")]
    public float maxIntervall = 3f;
    private SpriteRenderer ThisSprite;
    float flickerTimer;
    float flickerTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Get the sprite attached to this object
        ThisSprite = GetComponent<SpriteRenderer>();
        //Random flicker range
        flickerTimer = Random.Range(0, maxIntervall);
    }

    // Update is called once per frame
    void Update()
    {
        if (ThisSprite == null) return;
        flickerTimer -= Time.deltaTime;
        if (flickerTimer <= flickerTime)
        {
            ThisSprite.color = Color.yellow;
            flickerTimer = Random.Range(0, maxIntervall);
        }
        else
        {
            ThisSprite.color = Color.white;
        }
    }
}

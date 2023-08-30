using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCloudsEffect : MonoBehaviour
{
    public SpriteRenderer ThisSprite;
    float flickerTimer;
    float flickerTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        ThisSprite = GetComponent<SpriteRenderer>();

        flickerTimer = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {

        if (ThisSprite == null) return;

        flickerTimer -= Time.deltaTime;
        
        if (flickerTimer <= flickerTime)
        {

            ThisSprite.color = Color.yellow;
            flickerTimer = Random.Range(0, 3);

        } 
        else
        {
            ThisSprite.color = Color.white;
        }


    }
}

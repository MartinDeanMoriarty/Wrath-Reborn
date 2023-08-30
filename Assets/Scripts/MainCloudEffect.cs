using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCloudEffect : MonoBehaviour
{
    public SpriteRenderer ThisSprite;

    // Start is called before the first frame update
    void Start()
    {
        ThisSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThisSprite.color = Color.yellow;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ThisSprite.color = Color.white;
        }
        

    }
}

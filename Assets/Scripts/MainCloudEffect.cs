using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCloudEffect : MonoBehaviour
{
    [Header("Effect of the main cloud behind the bolt")]
    private SpriteRenderer ThisSprite;

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
        else
        {
            ThisSprite.color = Color.white;         
        }
    }
}

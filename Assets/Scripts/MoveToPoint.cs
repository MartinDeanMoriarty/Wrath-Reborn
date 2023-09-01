using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveToPoint : MonoBehaviour
{
    [Header("This Moves the dark clouds in position when game starts")]
    [Tooltip("Speed of clouds")]
    public float Speed = 10F;
    [Tooltip("The target to move to")]
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        var step = Speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step); 
    }
}

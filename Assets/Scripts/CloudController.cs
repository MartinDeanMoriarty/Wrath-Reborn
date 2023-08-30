using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CloudController : MonoBehaviour
{ 
    [Header("Cloud settings")]
    [Tooltip("Speed of clouds")]
    public float Speed = 0.2F;
    [Tooltip("The point for the clouds to flee when wrath starts")]
    public Transform FleeingPoint;
    [Header("DoNotChange")]
    [Tooltip("DoNotChange")]
    public bool fleeing = false;
    private float FleeingSpeed = 5F;   
    private bool Rotating;
    private int RotateDir;
    private float MaxX = 15;
    private float MinX = -15;
    private float MaxZ = 15;
    private float MinZ = -15;
    private float FlyingTime;
    private float RotationTime;

    void Start()
    {
        FlyingTime = Random.Range(5, 8);
        RotationTime = Random.Range(1, 3);
        Rotating = false;
        RotateDir = Random.Range(1, 3);
    }

    void Update()
    {
        if (!fleeing)
        {

            if (transform.position.x > MaxX | transform.position.x < MinX | transform.position.z > MaxZ | transform.position.z < MinZ)
            {
                transform.Rotate(0, -90, 0);
            }

            Vector3 vector3rot = Vector3.right;

            if (Rotating == true && RotateDir == 1)
            {

                RotationTime -= Time.deltaTime;
                transform.Rotate(0, 1 * Speed * Time.deltaTime, 0);
                if (RotationTime <= 0)
                {
                    Rotating = false;
                    RotationTime = Random.Range(1, 5);
                }
            }


            if (Rotating == true && RotateDir == 2)
            {
                RotationTime -= Time.deltaTime;
                transform.Rotate(0, -1 * Speed * Time.deltaTime, 0);

                if (RotationTime <= 0)
                {
                    Rotating = false;
                    RotationTime = Random.Range(1, 5);
                }
            }


            if (Rotating == false)
            {

                FlyingTime -= Time.deltaTime;
                transform.Translate(Vector3.right * Speed * Time.deltaTime);
                if (FlyingTime <= 0)
                {
                    RotateDir = Random.Range(1, 3);
                    Rotating = true;
                    FlyingTime = Random.Range(5, 50);
                }
            }
        }
        else
        {
            var step = FleeingSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, FleeingPoint.position, step);
        }
    }
}

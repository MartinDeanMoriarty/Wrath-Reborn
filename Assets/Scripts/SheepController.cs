using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SheepController : MonoBehaviour
{

    private string walkForwardAnimation = "walk_forward";
    //private string walkBackwardAnimation = "walk_backwards";
    //private string runForwardAnimation = "run_forward";
    private string turn90LAnimation = "turn_90_L";
    private string turn90RAnimation = "turn_90_R";
    //private string trotAnimation = "trot_forward";
    //private string sittostandAnimation = "sit_to_stand";
    //private string standtositAnimation = "stand_to_sit";

    //Set duration of walking//
    public float WalkingTime;
    public float RotationTime;
    //Set Speed of walking//
    private float Speed = 2;
    private float RotationSpeed = 2;
    //Is object rotating or not//
    public bool Rotating;
    //1 clockwise and 2 counter clockwise//
    private int RotateDir;
    //Set Limits to walk x and z axis//
    private float MaxX = 20;
    private float MinX = -20;
    private float MaxZ = 20;
    private float MinZ = -20;

    //When using define animator//
    Animator Anim;
    //Get the animator set walking time between 5 and 8 set rotating to false and rotate direction to 1 or 2//
    void Start()
    {
        Anim = GetComponent<Animator>();
        WalkingTime = Random.Range(5, 10);
        RotationTime = Random.Range(1, 3);
        Rotating = false;
        RotateDir = Random.Range(1, 3);
     
    }
    //On Collision turn -90 degrees//
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            transform.Rotate(0, -45, 0);
            Anim.Play(turn90LAnimation);
        }
    }
    //When rotate is false use walking when rotate is true rotate and change direction. Use limits to make object turn -90 degrees//
    //Use anmitions if needed//
    void Update()
    {
        if (transform.position.x > MaxX | transform.position.x < MinX | transform.position.z > MaxZ | transform.position.z < MinZ)
        {
            transform.Rotate(0, -90, 0);
            Anim.Play(turn90LAnimation);
        }


        if (Rotating == true && RotateDir == 1)
        {
            Anim.Play(turn90LAnimation);
            RotationTime -= Time.deltaTime;
            transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
            if (RotationTime <= 0)
            {
                Rotating = false;
                RotationTime = Random.Range(1, 3);
            }
        }


        if (Rotating == true && RotateDir == 2)
        {
            RotationTime -= Time.deltaTime;
            transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
            Anim.Play(turn90RAnimation);
            if (RotationTime <= 0)
            {
                Rotating = false;
                RotationTime = Random.Range(1, 3);
            }
        }


        if (Rotating == false)
        {
            Anim.Play(walkForwardAnimation);
            WalkingTime -= Time.deltaTime;
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            if (WalkingTime <= 0)
            {
                RotateDir = Random.Range(1, 3);
                Rotating = true;
                WalkingTime = Random.Range(5, 21);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LightningController : MonoBehaviour
{
    [Header("Bolt settings")]
    [Tooltip("Projectile. Same as the main bolt")]
    public GameObject projectile;
    [Tooltip("Projectile. Same as the main bolt")]
    public float speed = 10f;
    [Tooltip("Fire Sound")]
    public List<AudioSource> BoltSounds;
    [Tooltip("Transform of the player/bolt")]
    public Transform Player;
    [Header("DoNotChangeInInspector")]
    public Vector3 mousePosition;
    public Vector3 worldMousePosition;
    public Vector3 direction;
    public float angle;
    public Vector3 hitPoint;
    public Transform objectHit;
    private int soundCount = 0;

    void Update()
    {
        //Get and convert cursor position for bolt rotation
        mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        direction = worldMousePosition - gameObject.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Rotate the bolt to the crosshair
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));

        //Shooting action
        if (Input.GetMouseButtonDown(0))
        {
            //Raycast for the bolt/projectile target
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                // The raycast hit something
                hitPoint = hit.point; // This is the exact world coordinate where the raycast hit
                objectHit = hit.transform;
            }
            //Play AudioSource
            if (soundCount < BoltSounds.Count)
            {
                BoltSounds[soundCount].Play();
                soundCount++;
                if (soundCount == BoltSounds.Count)
                {
                    soundCount = 0;
                }
            }
            //Instantiate the bolt
            GameObject projectileInstance = Instantiate(projectile, Player.position, gameObject.transform.rotation);
            float dist = Vector3.Distance(hitPoint, Player.position);
            //Fire the bolt          
            projectileInstance.GetComponent<Rigidbody>().AddForce((hitPoint - Player.position).normalized * speed * dist, ForceMode.Impulse);
        }
    }
}

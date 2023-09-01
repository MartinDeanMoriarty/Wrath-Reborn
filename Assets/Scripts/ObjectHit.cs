using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    [Header("Bolt Objects Collision")]
    [Tooltip("Typicly the broken/destroyed version of the main object.")]
    public GameObject ObjectToSwap;
    [Tooltip("Debress Prefab to spawn when terrain got hit")]
    public GameObject DebressToSpawn;
    [Tooltip("Explosion Prefab to spawn when terrain got hit")]
    public GameObject ExplosionToSpawn;
    [Tooltip("Fx Prefab to spawn when terrain got hit")]
    public List<GameObject> FXSpawn;
    private Vector3 hitPoint;
    private Transform objectHit;

    //On collition instantiate Prefabs
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bolt"))
        {
            Instance();
        }
    }

    private void Update()
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
    }
    void Instance()
    {
        //The broken/destroyed version of the main object
        Instantiate(ObjectToSwap, gameObject.transform.position, gameObject.transform.rotation);
        //Debress Prefab 
        Instantiate(DebressToSpawn, hitPoint, Quaternion.identity);
        //Explosion Prefab
        GameObject explosion = Instantiate(ExplosionToSpawn, hitPoint, Quaternion.identity);
        Destroy(explosion, 1);
        //Fx Prefab
        Vector3 fxSpanPoint = hitPoint;
        fxSpanPoint.y = 0;
        Instantiate(FXSpawn[Random.Range(0, FXSpawn.Count)], fxSpanPoint, Quaternion.identity);
        Destroy(gameObject);
    }
}

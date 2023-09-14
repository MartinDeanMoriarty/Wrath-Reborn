using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHit : MonoBehaviour
{
    [Header("Bolt Sheep Collision")]
    [Tooltip("Gibbings Prefab to spawn when sheep got hit")]
    public GameObject GibToSpawn;
    [Tooltip("Blood decal Prefab to spawn when sheep got hit")]
    public List<GameObject> DecalToSpawn;
    [Tooltip("Explode Prefab to spawn when sheep got hit")]
    public GameObject ExplodeSpawn;
    //Predefine GameController
    GameController GameController;

    // Start is called before the first frame update
    void Start()
    {
        //Get GameController
        GameController = FindAnyObjectByType<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If Sheep got hit by Bolt
        if (collision.gameObject.CompareTag("Bolt"))
        {            
            GameController.SheepHits += 1;
            GameController.sheepCount -= 1;
            GameController.allHits += 1;
            Instance();
            Destroy(gameObject);
        }
    }

    void Instance()
    {
        //Gibbings Prefab
        GameObject gibs = Instantiate(GibToSpawn, gameObject.transform.position, Quaternion.identity);
        Destroy(gibs, 4);
        //Explode Prefab
        GameObject explosion = Instantiate(ExplodeSpawn, gameObject.transform.position, Quaternion.identity);
        Destroy(explosion, 2);
        //Blood Decal Prefab
        Vector3 decalSpanPoint = gameObject.transform.position;
        decalSpanPoint.y = 0.02F;
        Instantiate(DecalToSpawn[Random.Range(0, DecalToSpawn.Count)], decalSpanPoint, Quaternion.identity);
    }
}

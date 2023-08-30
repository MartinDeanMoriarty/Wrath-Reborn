using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHit : MonoBehaviour
{
    //Gibbings Prefab
    public GameObject GibToSpawn;
    //Blood Effect Prefab
    //public GameObject FxToSpawn;
    //Blood Decal Prefab
    public List<GameObject> DecalToSpawn;


    public GameObject ExplodeSpawn;

    GameController GameController;

    // Start is called before the first frame update
    void Start()
    {
        GameController = FindAnyObjectByType<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bolt"))
        {
            Destroy(gameObject);
            GameController.SheepHits += 1;
            GameController.sheepCount -= 1;
            GameController.allHits += 1;
            Instance();
        }        
    }

    void Instance()
    {
         
        //Gibbings Prefab
        Instantiate(GibToSpawn, gameObject.transform.position, Quaternion.identity);
        GameObject explosion = Instantiate(ExplodeSpawn, gameObject.transform.position, Quaternion.identity);
        Destroy(explosion,2);
        //Blood Effect Prefab
        //Instantiate(FxToSpawn, gameObject.transform.position, Quaternion.identity);
        //Blood Decal Prefab
        Vector3 decalSpanPoint = gameObject.transform.position;
        decalSpanPoint.y = 0.02F;
        Instantiate(DecalToSpawn[Random.Range(0, DecalToSpawn.Count)], decalSpanPoint, Quaternion.identity);         

    }
}

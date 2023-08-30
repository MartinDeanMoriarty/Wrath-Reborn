using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    public GameObject ObjectToSwap;
    public GameObject DebressToSpawn;
    public GameObject ExplosionToSpawn;
    public List<GameObject> FXSpawn;
    public Vector3 hitPoint;
    public Transform objectHit;

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


        Instantiate(ObjectToSwap, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(DebressToSpawn, hitPoint, Quaternion.identity);
        GameObject explosion = Instantiate(ExplosionToSpawn, hitPoint, Quaternion.identity);
        Destroy(explosion, 1);
      
        Vector3 fxSpanPoint = hitPoint;
        fxSpanPoint.y = 0;
        Instantiate(FXSpawn[Random.Range(0, FXSpawn.Count)], fxSpanPoint, Quaternion.identity);
        Destroy(gameObject);

    }
}

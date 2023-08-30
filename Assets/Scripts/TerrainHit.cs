using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHit : MonoBehaviour
{
    public GameObject DebressToSpawn;
    public GameObject ExplosionToSpawn;
    public List<GameObject> DecalToSpawn;
    public Vector3 hitPoint;

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

        }
    }
    void Instance()
    {
     

        //Debress Prefab
        Instantiate(DebressToSpawn, hitPoint, Quaternion.identity);
        GameObject explosion = Instantiate(ExplosionToSpawn, hitPoint, Quaternion.identity);
        Destroy(explosion, 2);
        //Decal Prefab
        Vector3 decalSpanPoint = hitPoint;
        decalSpanPoint.y = 0.02F;
        Instantiate(DecalToSpawn[Random.Range(0, DecalToSpawn.Count)], decalSpanPoint, Quaternion.identity);

    }
}

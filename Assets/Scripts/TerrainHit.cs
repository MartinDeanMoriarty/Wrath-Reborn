using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHit : MonoBehaviour
{
    [Header("Bolt Terrain Collision")]
    [Tooltip("Debress Prefab to spawn when terrain got hit")]
    public GameObject DebressToSpawn;
    [Tooltip("Explosion Prefab to spawn when terrain got hit")]
    public GameObject ExplosionToSpawn;
    [Tooltip("Ground decal Prefab to spawn when terrain got hit")]
    public List<GameObject> DecalToSpawn;
    public Vector3 hitPoint;

    //On collision instantiate the Prefabs from above
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
        //Explosion Prefab
        GameObject explosion = Instantiate(ExplosionToSpawn, hitPoint, Quaternion.identity);
        Destroy(explosion, 2);
        //Decal Prefab
        Vector3 decalSpanPoint = hitPoint;
        decalSpanPoint.y = 0.02F;
        Instantiate(DecalToSpawn[Random.Range(0, DecalToSpawn.Count)], decalSpanPoint, Quaternion.identity);
    }
}

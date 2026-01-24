using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    public List<GameObject> Spawns = new List<GameObject>();
    public Transform SpawnPoint;
    public float RandRange=0.5f;

    [SerializeField] ParticleSystem spawnParticles;
    [SerializeField] AudioSource spawnSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public void SpawnObject(int id)
    {
        Vector3 point = SpawnPoint.position;
        point += new Vector3(
            Random.Range(-RandRange,RandRange),
            Random.Range(-RandRange,RandRange),
            Random.Range(-RandRange,RandRange)
            );
        GameObject obj= Instantiate(Spawns[id], point, Quaternion.identity);
        obj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0.2f, 0.5f), Random.Range(1.5f, 3.0f), Random.Range(0.2f, 0.5f)), ForceMode.Impulse);

        spawnParticles.transform.position = point;
        spawnParticles.Play();
        spawnSFX.Play();

        Debug.Log("Spawned item : " + id);
    }

    
}

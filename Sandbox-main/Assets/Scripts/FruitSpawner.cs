using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    public List<GameObject> Spawns = new List<GameObject>();
    public Transform SpawnPoint;
    public float RandRange=0.5f;

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
        Debug.Log("Spawned item : " + id);
    }

    
}

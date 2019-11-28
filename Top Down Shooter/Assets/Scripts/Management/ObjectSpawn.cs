using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public float objectSpawnTime;
    public GameObject[] powerups;
    Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnObject", 0, objectSpawnTime);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnObject()
    {
        spawnPos = new Vector3(Random.Range(-12,12),0.5f,Random.Range(-21,7));
        Instantiate(powerups[Random.Range(0,powerups.Length)],spawnPos,Quaternion.identity);
    }

}

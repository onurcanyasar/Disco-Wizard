using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemies;

    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (true)
        { 
            Instantiate(enemies[Random.Range(0,2)], spawnPoint.position, spawnPoint.rotation);
            
            yield return new WaitForSeconds(10);
        }
    }
}

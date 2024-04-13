using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSpawner : MonoBehaviour
{
    public GameObject sand;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("l"))
        {
            Instantiate(sand, transform.position, Quaternion.identity);
        }
    }
}

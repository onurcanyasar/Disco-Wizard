using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightHandler : MonoBehaviour
{
    public float speed;
    void Start()
    {
        speed = 40f;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.Rotate(Vector3.forward, speed * Time.deltaTime, 0);
    }
}

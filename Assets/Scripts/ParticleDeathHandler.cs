using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeathHandler : MonoBehaviour, IKillable
{
    private ParticleSystem pr;
    void Start()
    {
        pr = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pr.IsAlive())
        {
            Kill();
        }      
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}

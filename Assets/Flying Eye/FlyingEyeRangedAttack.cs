using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeRangedAttack : MonoBehaviour
{
    public GameObject proj;
    public Transform shootPoint;
    
    void InstantiateProj()
    {
        Instantiate(proj,shootPoint.position,shootPoint.rotation);
    } 
}

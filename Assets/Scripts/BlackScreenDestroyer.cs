using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenDestroyer : MonoBehaviour
{
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerLevelHandler : MonoBehaviour
{
    private EnemyCounter enemyCounter;
    public GameObject destroyer;
    void Start()
    {
        enemyCounter = GameObject.Find("EnemyCounter").GetComponent<EnemyCounter>();
        StartCoroutine(checkLevelFinished());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator checkLevelFinished()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (enemyCounter.EnemyCount <= 0)
            {
                destroyer.SetActive(true);
                break;
            }    
        }
        
    }
}

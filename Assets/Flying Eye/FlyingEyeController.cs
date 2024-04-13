using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class FlyingEyeController : MonoBehaviour
{
    public AIPath path;

    public bool isPlayerInRange;

    public Animator anim;

    private bool isAttackStarted = false;
    // Start is called before the first frame update
    void Awake()
    {
        path = GetComponent<AIPath>();
        path.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (path.desiredVelocity.x >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }else if (path.desiredVelocity.x <= -0.01f)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }


        if (isPlayerInRange)
        {
            path.enabled = false;

            if (!isAttackStarted)
            {
                StartCoroutine(Attack());
                Debug.Log("Attack started");
                isAttackStarted = true;
            }
        }

        else
        {
            path.enabled = true;
        }
        
    }

    IEnumerator Attack()
    {
        while (true)
        {
            anim.SetTrigger("Ranged");
            Debug.Log("flying Eye attacked from range" );
            
            yield return new WaitForSeconds(2);
            if(!isPlayerInRange)
                break;
        }
        
        yield break;
    }
}

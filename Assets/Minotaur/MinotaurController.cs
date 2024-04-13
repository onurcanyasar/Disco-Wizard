using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class MinotaurController : MonoBehaviour
{
    private AIPath aiPath;
    private Animator anim;
    private EnemyHealth enemyHealth;

    public Transform healthBar;
    public bool isPlayerInRange;
    private bool isStarted = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        aiPath = GetComponent<AIPath>();
        enemyHealth = GetComponent<EnemyHealth>();
        aiPath.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && !enemyHealth.isInvulnerable)
        {
            aiPath.enabled = true;
            if (!isStarted)
            {
                anim.SetTrigger("Start");
                isStarted = true;
            }
        }
        else
        {
            aiPath.enabled = false;
            isStarted = false;
        }
        
        anim.SetFloat("Speed",Mathf.Abs(aiPath.desiredVelocity.x));

        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            healthBar.transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            healthBar.transform.rotation = Quaternion.Euler(0,0,0);

        }
    }
    
    
}

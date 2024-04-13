using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MinoSecondPhaseStart : StateMachineBehaviour
{
    private EnemyHealth enemyHealth;
    private SpriteRenderer sprite;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyHealth = animator.GetComponent<EnemyHealth>();
        sprite = animator.GetComponent<SpriteRenderer>();
        sprite.DOColor(Color.red, 0.9f).SetLoops(2, LoopType.Yoyo);
        enemyHealth.isInvulnerable = true;
        
    }

     // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

     // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyHealth.isInvulnerable = false;

    }

    
}

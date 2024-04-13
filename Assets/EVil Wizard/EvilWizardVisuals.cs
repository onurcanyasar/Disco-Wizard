using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EvilWizardVisuals : MonoBehaviour
{
    // Start is called before the first frame update
    public AIPath path;
    public Animator anim;


    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed",Mathf.Abs(path.desiredVelocity.x));

        if (path.desiredVelocity.x >= 0.01f)
        {
            transform.parent.rotation = Quaternion.Euler(0,0,0);
        }else if (path.desiredVelocity.x <= -0.01f)
        {
            transform.parent.rotation = Quaternion.Euler(0,180,0);
        }
    }

    public void WizAttack()
    {
//        Debug.Log("Wizard Attacked");
        transform.parent.GetComponent<EvilWizardAttack>().Attack();
    }
}

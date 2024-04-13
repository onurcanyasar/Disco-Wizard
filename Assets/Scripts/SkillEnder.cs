using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEnder : MonoBehaviour
{

    void End()
    {
        Destroy(gameObject.name.Equals("Minotaur") ? gameObject : transform.parent.gameObject);
    }

    void RainDamage()
    {
        transform.parent.GetComponent<RainController>().inflictDamage();
    }

    void TornadoDamage()
    {
        transform.parent.GetComponent<TornadoController>().inflictDamage();

    }

    void LightningDamage()
    {
        transform.parent.GetComponent<LightningController>().inflictDamage();

    }
    
}

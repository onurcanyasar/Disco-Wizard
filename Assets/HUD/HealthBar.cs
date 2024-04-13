using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image fill;

    public PlayerHealthAndMana playerHealthAndMana;
    // Start is called before the first frame update
    void Start()
    {
       // playerHealthAndMana = GameObject.Find("AfroWizard").GetComponent<PlayerHealthAndMana>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(float health)
    {
        fill.fillAmount = health/playerHealthAndMana.maxHP;
    }
}

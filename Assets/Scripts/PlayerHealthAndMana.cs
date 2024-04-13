using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthAndMana : MonoBehaviour
{
    public HealthBar healthBar;
    public ManaBar manaBar;
    
    public float maxHP = 100;
    public float maxMana = 200;
    public float currHP;
    public float currMana;

    public float manaRecoveryAmount;
    public float manaRecoveryRate;

    private Coroutine manaRecovery;
    private bool isManaRecoveryStarted = false;

    private Sequence colorSeq;
    private SpriteRenderer sprite;

    public GameObject playerGroup;
    public GameObject HUD;
    private bool isEnded;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        currHP = maxHP;
        healthBar.setHealth(currHP);

        currMana = maxMana;
        manaBar.setMana(currMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (currHP <= 0 && !isEnded)
        {
            isEnded = true;
            Destroy(playerGroup);
            Destroy(HUD);
            SceneManager.LoadScene("StartScreen");

        }

        if (!isManaRecoveryStarted)
        {

            if (currMana < maxMana)
            {
                StartCoroutine("RecoverMana");
                isManaRecoveryStarted = true;
            }
        }
        if(currMana >= maxMana)
        {

            if (isManaRecoveryStarted)
            {
                isManaRecoveryStarted = false;
            }
        }
    }

    private IEnumerator RecoverMana()
    {
        while (currMana < maxMana)
        {
            currMana += manaRecoveryAmount;
            //Debug.Log("Mana recovered");
            manaBar.setMana(currMana);
            
            if (currMana > maxMana)
            {
                    currMana = maxMana;
                    isManaRecoveryStarted = false;
            }
            yield return new WaitForSeconds(manaRecoveryRate);
        }

        isManaRecoveryStarted = false;
    }

    public void TakeDamage(float damage)
    {
        currHP -= damage;
        colorSeq = DOTween.Sequence();
        colorSeq.Append(sprite.DOColor(Color.red, 0.04f).SetEase(Ease.Flash));
        colorSeq.Append(sprite.DOColor(Color.white, 0.03f).SetEase(Ease.Flash));
        
        if (currHP <= 0)
        {
            currHP = 0;
        }
        healthBar.setHealth(currHP);

    }

    public void SpendMana(float mana)
    {
        currMana -= mana;
        if (currMana <= 0)
        {
            currMana = 0;
        }
        manaBar.setMana(currMana);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{

    public float fireCD = 1;
    public float iceCD = 3;
    public float lightningCD = 1;
    public float tornadoCD = 10;
    public float rainCD = 1;
    public float globalCD = 1;

    public GameObject fireballObject;
    public GameObject tornadoObject;
    public GameObject lightningObject;
    public GameObject rainObject;

    public Transform castingPoint;
    
    public Image fire;
    public Image ice;
    public Image lightning;
    public Image tornado;
    public Image rain;
    
    private bool isFireAcquired = true;
    private bool isIceAcquired = true;
    private bool isLightningAcquired = true;
    private bool isTornadoAcquired = true;
    private bool isRainAcquired = true;
    
    private bool isFireOnCooldown = false;
    private bool isIceOnCooldown = false;
    private bool isLightningOnCooldown = false;
    private bool isTornadoOnCooldown = false;
    private bool isRainOnCooldown = false;
    private bool isGlobalCooldown = false;

    private bool isFireUsed;
    private bool isLightningUsed;
    private bool isTornadoUsed;
    private bool isRainUsed;
    

    public GameObject lightningTemp;
    public Animator anim;
    private GameObject wizard;
    private PlayerController pc;
    private PlayerHealthAndMana playerHealthAndMana;
    
    private Camera cam;
    private Vector3 cursor;
    private Vector2 rainStartPoint;

    public float fireMana;
    public float lightningMana;
    public float tornadoMana;
    public float rainMana;

    private AudioSource source;

    public AudioClip fireSound;
    public AudioClip lightningSound;
    public AudioClip tornadoSound;
    public AudioClip rainSound;
    
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        cam = Camera.main;
        fire.fillAmount = 0;
        ice.fillAmount = 0;
        lightning.fillAmount = 0;
        tornado.fillAmount = 0;
        rain.fillAmount = 0;
        
        wizard = anim.gameObject;
        pc = wizard.GetComponent<PlayerController>();
        playerHealthAndMana = wizard.GetComponent<PlayerHealthAndMana>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isFireAcquired && !isFireOnCooldown && playerHealthAndMana.currMana >= fireMana)
        {
            Vector3 cursorPosition = Input.mousePosition;
            cursorPosition.z = cam.nearClipPlane;
            cursor = cam.ScreenToWorldPoint(cursorPosition);
            cursor.z = 0;

            //var relative = AngleDir(wizard.transform.position, cursor); not working so changed it
            float relative = cursor.x - wizard.transform.position.x; //onur
            if (relative < 0) 
            {
                pc.TurnWizard("L");
            }
            else
            {
                pc.TurnWizard("R");
            }
            
            anim.SetTrigger("cast");
            
            Instantiate(fireballObject, castingPoint.position, castingPoint.rotation);
            source.PlayOneShot(fireSound);
            playerHealthAndMana.SpendMana(fireMana);
            
            if (FireballController.activationFlags[2])
            {
                Invoke("FireballBarrage",0.2f);
                Invoke("FireballBarrage",0.4f);
            }

            isFireUsed = true;
            isFireOnCooldown = true;
            fire.fillAmount = 1;
            
        }
        
        if (isFireOnCooldown && isFireUsed)
        {
            fire.fillAmount -= 1 / fireCD * Time.deltaTime;
            if (fire.fillAmount <= 0)
            {
                fire.fillAmount = 0;
                isFireOnCooldown = false;
                isFireUsed = false;
            }
        }
        
        // if (Input.GetKeyDown(KeyCode.Alpha2) && isIceAcquired && !isIceOnCooldown)
        // {
        //     // create ice spike
        //     isIceOnCooldown = true;
        //     ice.fillAmount = 1;
        // }
        //
        // if (isIceOnCooldown)
        // {
        //     ice.fillAmount -= 1 / iceCD * Time.deltaTime;
        //     if (ice.fillAmount <= 0)
        //     {
        //         ice.fillAmount = 0;
        //         isIceOnCooldown = false;
        //     }
        // }
        
        if (Input.GetKey(KeyCode.Alpha2) && isLightningAcquired && !isLightningOnCooldown && playerHealthAndMana.currMana >= lightningMana)
        { 
            Vector3 cursorPosition = Input.mousePosition;
            cursorPosition.z = cam.nearClipPlane;
            cursor = cam.ScreenToWorldPoint(cursorPosition);
            cursor.z = 0;
            
            //var relative = AngleDir(wizard.transform.position, cursor); not working correct so changed it

            float relative = cursor.x - wizard.transform.position.x;
            if (relative < 0) 
            {
                pc.TurnWizard("L");
            }
            else
            {
                pc.TurnWizard("R");
            }
            
            
            anim.SetTrigger("cast");
            
            lightningTemp = Instantiate(lightningObject, castingPoint.position, castingPoint.rotation);
            source.PlayOneShot(lightningSound);

            playerHealthAndMana.SpendMana(lightningMana);
            
            if (LightningController.activationFlags[2])
            {
                var rot = castingPoint.rotation * Quaternion.Euler(0, 0, 45);
                for (int i = 0; i < 7; i++)
                {
                    Instantiate(lightningObject, castingPoint.position, rot);
                    rot = castingPoint.rotation * Quaternion.Euler(0,0,45*(i+2));
                }
            }
            isLightningOnCooldown = true;
            isLightningUsed = true;
            lightning.fillAmount = 1;
        }
        
        if (isLightningOnCooldown && isLightningUsed)
        {
            lightning.fillAmount -= 1 / lightningCD * Time.deltaTime;
            if (lightning.fillAmount <= 0)
            {
                lightning.fillAmount = 0;
                isLightningOnCooldown = false;
                isLightningUsed = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && isTornadoAcquired && !isTornadoOnCooldown && playerHealthAndMana.currMana >= tornadoMana)
        {
            Vector3 cursorPosition = Input.mousePosition;
            cursorPosition.z = cam.nearClipPlane;
            cursor = cam.ScreenToWorldPoint(cursorPosition);
            cursor.z = 0;
            // turn wizard left or right wrt cursor position
            //var relative = AngleDir(wizard.transform.position, cursor); not working correct so changed it

            float relative = cursor.x - wizard.transform.position.x;
            if (relative < 0) 
            {
                pc.TurnWizard("L");
            }
            else
            {
                pc.TurnWizard("R");
            }
            
            
            anim.SetTrigger("cast");

            Instantiate(tornadoObject, castingPoint.position, castingPoint.rotation);
            source.PlayOneShot(tornadoSound);

            playerHealthAndMana.SpendMana(tornadoMana);
           
            isTornadoOnCooldown = true;
            isTornadoUsed = true;
            tornado.fillAmount = 1;
        }
        
        if (isTornadoOnCooldown & isTornadoUsed)
        {
            tornado.fillAmount -= 1 / tornadoCD * Time.deltaTime;
            if (tornado.fillAmount <= 0)
            {
                tornado.fillAmount = 0;
                isTornadoOnCooldown = false;
                isTornadoUsed = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4) && isRainAcquired && !isRainOnCooldown && playerHealthAndMana.currMana >= rainMana)
        {
            
            
            Vector3 cursorPosition = Input.mousePosition;
            cursorPosition.z = cam.nearClipPlane;
            cursor = cam.ScreenToWorldPoint(cursorPosition);
            cursor.z = 0;
            
            //var relative = AngleDir(wizard.transform.position, cursor); not working correct so changed it

            float relative = cursor.x - wizard.transform.position.x;
            
            if (relative < 0) 
            {
                pc.TurnWizard("L");
            }
            else
            {
                pc.TurnWizard("R");
            }

            RaycastHit2D floor = Physics2D.Raycast(cursor, Vector2.down, Mathf.Infinity,LayerMask.GetMask("Cell"));
            isRainOnCooldown = true;
            isRainUsed = true;
            rain.fillAmount = 1;

            if (floor.collider != null)
            {
                anim.SetTrigger("cast");
                // how high will be the start point
                rainStartPoint = floor.point + (Vector2.up * 30);
                Instantiate(rainObject, new Vector3(cursor.x,rainStartPoint.y,0), castingPoint.parent.rotation);
                source.PlayOneShot(rainSound);

                playerHealthAndMana.SpendMana(rainMana);
               
                if (RainController.activationFlags[2])
                {
                    Invoke("RainBarrage",0.2f);
                    Invoke("RainBarrage",0.4f);
                }
            }
            
            
        }
        
        if (isRainOnCooldown && isRainUsed)
        {
            rain.fillAmount -= 1 / rainCD * Time.deltaTime;
            if (rain.fillAmount <= 0)
            {
                rain.fillAmount = 0;
                isRainOnCooldown = false;
                isRainUsed = false;
            }
        }
    }

    void FireballBarrage()
    {
        Instantiate(fireballObject, castingPoint.position, castingPoint.rotation);

    }

    void RainBarrage()
    {
        Instantiate(rainObject, new Vector3(cursor.x,rainStartPoint.y,0), castingPoint.rotation);

    }
    
    public static float AngleDir(Vector2 A, Vector2 B)
    {
        return A.x * -B.y + A.y * B.x;
    }

   
}

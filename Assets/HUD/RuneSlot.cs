using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RuneSlot : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public bool isCollected = false;
    public Image border;
    public string tooltipHeader;
    public string tooltipText;
    public static RuneSlot selectedRune;
    
    public int runeNumber;
    public string runeName;


    // Start is called before the first frame update
    void Awake()
    {
        runeNumber = int.Parse( new string(gameObject.name.Where(char.IsDigit).ToArray()));
        runeName = gameObject.name[0].ToString();
        tooltipText = tooltipHeader+"\n"+tooltipText;
        
        if (isCollected)
        {
            border.color = Color.yellow;
            GetComponent<Image>().color = Color.white;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isCollected)
        {
            border.color = Color.yellow;
            GetComponent<Image>().color = Color.white;
        }
    }

    public void ActivateRune()
    {
        switch (runeName)
        {
            case "F":
                FireballController.activationFlags[runeNumber - 1] = true;
                Debug.Log("F"+runeNumber+" is activated");
                break;
            case "L":
                LightningController.activationFlags[runeNumber - 1] = true;
                Debug.Log("L" + runeNumber + " is activated");
                break;
            case "T":
                TornadoController.activationFlags[runeNumber - 1] = true;
                Debug.Log("T" + runeNumber + " is activated");
                break;
            
            case "R":
                RainController.activationFlags[runeNumber - 1] = true;
                Debug.Log("R" + runeNumber + " is activated");
                break;
        }
    }

    public void DeactivateRune()
    {
        switch (runeName)
        {
            case "F":
                FireballController.activationFlags[runeNumber - 1] = false;
                Debug.Log("F" + runeNumber + " is DEactivated");
                break;
            
            case "L":
                LightningController.activationFlags[runeNumber - 1] = false;
                Debug.Log("L" + runeNumber + " is DEactivated");
                break;
            
            case "T":
                TornadoController.activationFlags[runeNumber - 1] = false;
                Debug.Log("T" + runeNumber + " is DEactivated");
                break;
           
            case "R":
                RainController.activationFlags[runeNumber - 1] = false;
                Debug.Log("R" + runeNumber + " is DEactivated");
                break;
        }
        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isCollected)
        {
            TooltipManager.ShowTooltipStatic(tooltipText);
            Debug.Log("mouse is over");
        
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isCollected)
        {
            TooltipManager.HideTooltipStatic();
            Debug.Log("mouse is not over");
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (ActiveSlot.isSlotSelected && isCollected)
        {
            selectedRune = this;
        }
    }
}

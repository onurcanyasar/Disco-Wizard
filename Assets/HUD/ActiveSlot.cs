using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    public int slotNo;
    public Image border;
    public bool isSlotEmpty;
    public RuneSlot selectedRune;
    public static bool isSlotSelected;
    public InventoryManager inventoryManager;
    public bool isThisSlotSelected;
    public bool isSelectable;
    public RuneSlot rune;
    private TextMeshProUGUI text;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        border.color = Color.yellow;
        isSlotEmpty = true;
        isSelectable = true;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isThisSlotSelected)
        {
            if (RuneSlot.selectedRune != null && !inventoryManager.selectedRunes.Contains(RuneSlot.selectedRune))
            {
                if (rune != null)
                {
                    rune.DeactivateRune();
                }
                rune = RuneSlot.selectedRune;
                rune.ActivateRune();
                RuneSlot.selectedRune = null;
                
                isSlotEmpty = false;
                isSlotSelected = false;
                isThisSlotSelected = false;
                
                border.color = Color.yellow;
                image.color = Color.white;
                image.sprite = rune.GetComponent<Image>().sprite;

                text.text = rune.GetComponentInChildren<TextMeshProUGUI>().text;
                
                inventoryManager.selectedRunes[slotNo] = rune;
                foreach (var slot in inventoryManager.activeSlots)
                {
                    slot.isSelectable = true;
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelectable)
        {
            if (isSlotSelected)
            {
                border.color = Color.yellow;
                isSlotSelected = false;
                isThisSlotSelected = false;
                foreach (var slot in inventoryManager.activeSlots)
                {
                    slot.isSelectable = true;
                }
            }
            else
            {
                border.color = Color.red;
                isSlotSelected = true;
                isThisSlotSelected = true;
                foreach (var slot in inventoryManager.activeSlots)
                {
                    if (!slot.isThisSlotSelected)
                    {
                        slot.isSelectable = false;
                    }
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSlotEmpty)
        {
            TooltipManager.ShowTooltipStatic("Choose a slot and select a rune for it");    
        }
        else
        {
            TooltipManager.ShowTooltipStatic(rune.tooltipText);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.HideTooltipStatic();
    }
}

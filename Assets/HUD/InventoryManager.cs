using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    
    public List<RuneSlot> selectedRunes;
    public List<RuneSlot> collectedRunes;
    public List<ActiveSlot> activeSlots;

    public static bool isInventoryOpen = false;

    public GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        inventory.transform.position = new Vector3(1000,1000,0);
    }

    public static bool IsMouseOverInventory()
    {
        var result = EventSystem.current.IsPointerOverGameObject();
        if (result)
        {
            Debug.Log("cursor is over Inventory");
        }
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventory.transform.localPosition = new Vector3(0,0,0);
                isInventoryOpen = true;
                inventory.SetActive(true); //edited by onur
            } 
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
            {
                inventory.transform.position = new Vector3(1000,1000,0);
                isInventoryOpen = false;
                TooltipManager.HideTooltipStatic();
                inventory.SetActive(false); //edited by onur
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }
    public RectTransform background;
    public TextMeshProUGUI tm;
    private RectTransform rectTransform;
    private Vector3 cursorLocation;
    public RectTransform canvas;

    void Awake()
    {
        Instance = this;
        
        rectTransform = GetComponent<RectTransform>();
        
        HideTooltip();
        
    }

    void SetText(string text)
    {
        tm.SetText(text);
        tm.ForceMeshUpdate();

        Vector2 textSize = tm.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8,8); 

        background.sizeDelta = textSize + paddingSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 ancPos = Input.mousePosition / canvas.localScale.x;
        if (ancPos.x + background.rect.width > canvas.rect.width)
        {
            ancPos.x = canvas.rect.width - background.rect.width;
        }
        if (ancPos.y + background.rect.height > canvas.rect.height)
        {
            ancPos.y = canvas.rect.height - background.rect.height;
        }
        
        rectTransform.anchoredPosition = ancPos;
    }

    public static void ShowTooltipStatic(string text)
    {
        Instance.ShowTooltip(text);
    }

    public static void HideTooltipStatic()
    {
        Instance.HideTooltip();
    }

    private void ShowTooltip(string text)
    {
        gameObject.SetActive(true);
        SetText(text);
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    
    
}

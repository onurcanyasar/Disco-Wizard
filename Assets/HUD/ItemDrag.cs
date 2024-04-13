using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrag : MonoBehaviour
{

    private Vector3 cursorLocation;

    private void Awake()
    {
        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = Camera.main.nearClipPlane;
        cursorLocation = Camera.main.ScreenToWorldPoint(cursorPosition);
        cursorLocation.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnMouseDrag()
    {
        Vector3 cursorPosition = Input.mousePosition;
        cursorPosition.z = Camera.main.nearClipPlane;
        cursorLocation = Camera.main.ScreenToWorldPoint(cursorPosition);
        cursorLocation.z = 0;
        transform.position = cursorLocation;
    }
}

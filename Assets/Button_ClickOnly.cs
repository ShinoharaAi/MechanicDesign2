using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_ClickOnly : MonoBehaviour, IPointerClickHandler
{
    public event Action onClick;

    private void Awake()
    {
        onClick += Test;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(); 
    }

    void Test()
    {
        Debug.Log("I was pressed"); 
    }

}

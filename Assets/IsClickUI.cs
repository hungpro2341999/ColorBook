using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IsClickUI : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public static bool IsClick;
    public void OnPointerDown(PointerEventData eventData)
    {
        IsClick = true;
    }

  
    public void OnPointerUp(PointerEventData eventData)
    {
        IsClick = false;

    }
}
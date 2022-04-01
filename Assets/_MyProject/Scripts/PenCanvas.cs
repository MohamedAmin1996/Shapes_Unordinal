using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PenCanvas : MonoBehaviour, IPointerClickHandler
{
    public Action onPenCanvasLeftClick;
    public Action onPenCanvasRightClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            // Left Click
            onPenCanvasLeftClick?.Invoke();
        }
        else if (eventData.pointerId == -2)
        {
            // Right Click
            onPenCanvasRightClick?.Invoke();
        }
    }
}

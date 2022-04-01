using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class DotController : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public Action<DotController> onDragEvent;
    public Action<DotController> OnRightClickEvent;
    public Action<DotController> OnLeftClickEvent;
    public LineController line;
    public int index;
    public void OnDrag(PointerEventData eventData)
    {
        onDragEvent?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -2)
        {
            // Right Click
            OnRightClickEvent?.Invoke(this);
        }
        else if (eventData.pointerId == -1)
        {
            // LeftClick
            OnLeftClickEvent?.Invoke(this);
        }
    }

    public void SetLine(LineController line)
    {
        this.line = line;
    }
}

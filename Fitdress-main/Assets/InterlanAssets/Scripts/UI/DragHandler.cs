using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action<PointerEventData> onBeginDrag;
    public event Action<PointerEventData> onDrag;
    public event Action<PointerEventData> onEndDrag;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag?.Invoke(eventData);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(eventData);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        onEndDrag?.Invoke(eventData);
    }
}

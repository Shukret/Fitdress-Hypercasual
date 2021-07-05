using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public event Action<PointerEventData> onClick;
    public event Action<PointerEventData> onDoubleClick;
    public event Action<PointerEventData> onDown;
    public event Action<PointerEventData> onUp;
    
    private Timer _MouseSingleClickTimer = new Timer();

    private void Awake()
    {
        _MouseSingleClickTimer.Interval = 300;
        _MouseSingleClickTimer.Elapsed += SingleClick;
    }
    
    void SingleClick(object o, System.EventArgs e)
    {
        _MouseSingleClickTimer.Stop();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(eventData);
        
        if (_MouseSingleClickTimer.Enabled == false)
        {
            _MouseSingleClickTimer.Start();
            return;
        }
        else
        {
            _MouseSingleClickTimer.Stop();
            
            onDoubleClick?.Invoke(eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onDown?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onUp?.Invoke(eventData);
    }
}

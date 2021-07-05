using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAudioEvent : MonoBehaviour, IPointerClickHandler
{
    public AudioAction action;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioController.PlayAction(action);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioEvent : MonoBehaviour
{
    public AudioAction action;
    
    public void Play()
    {
        AudioController.PlayAction(action);
    }
}

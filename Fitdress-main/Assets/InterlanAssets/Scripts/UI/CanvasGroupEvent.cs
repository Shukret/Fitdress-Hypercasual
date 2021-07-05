using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using UnityEngine;

public class CanvasGroupEvent : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeValue;
    public float duration;

    public bool raycastTargetOnEnd;
    
    public GameState eventState;

    private void Awake()
    {
        GameEvents.onGameStateChange += GameEventsOnGameStateChange;
    }

    private void OnDisable()
    {
        GameEvents.onGameStateChange -= GameEventsOnGameStateChange;

    }

    private void GameEventsOnGameStateChange(GameState state)
    {
        if (eventState == state)
        {
            Fade();

            canvasGroup.blocksRaycasts = raycastTargetOnEnd;
        }
    }

    private void Fade()
    {
        canvasGroup.DOKill();
        canvasGroup.DOFade(fadeValue, duration);
    }
}

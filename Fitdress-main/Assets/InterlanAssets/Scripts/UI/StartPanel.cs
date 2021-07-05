using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartPanel : MonoBehaviour
{
    public ClickHandler clickHandler;
    public CanvasGroup canvasGroup;

    public float fadeDuration;
    
    private void OnEnable()
    {
        RegisterEvents();
    }
    
    private void OnDisable()
    {
        UnregisterEvents();
    }

    private void RegisterEvents()
    {
        clickHandler.onClick += ClickHandlerOnOnClick;
    }

    private void UnregisterEvents()
    {
        clickHandler.onClick += ClickHandlerOnOnClick;
    }
    
    private void ClickHandlerOnOnClick(PointerEventData obj)
    {
        GameEvents.ChangeGameState(GameState.Play);
        UnregisterEvents();
        Hide();
    }

    private void Show()
    {
        
    }
    
    private void Hide()
    {
        canvasGroup.DOFade(0, fadeDuration).OnComplete(() =>
        {
            canvasGroup.gameObject.SetActive(false);
        });
    }
}

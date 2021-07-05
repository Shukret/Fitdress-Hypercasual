using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetSizeUI : MonoBehaviour
{
    public Text text;
    public RectTransform panel;

    public Vector2 exitPos;
    
    private void Start()
    {
        text.text = LevelController.CurrentLevel.targetBodySize.ToString();
        
        GameEvents.onGameStateChange += GameEventsOnGameStateChange;
    }

    private void GameEventsOnGameStateChange(GameState state)
    {
        if (state == GameState.Finish)
        {
            panel.DOAnchorPos(exitPos, 0.5f);
        }
    }
}

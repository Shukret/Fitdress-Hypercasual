using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class Curtains : MonoBehaviour
{
    public RectTransform curtains;
    void Awake()
    {
        GameEvents.onGameStateChange += OnGameStateChange;
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }

    private void OnDestroy()
    {
        GameEvents.onGameStateChange -= OnGameStateChange;
        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
    }
    
    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Open();
    }
    
    private void OnGameStateChange(GameState state)
    {
        if (state == GameState.Fail)
        {
            Close();
        }
    }

    private void Open()
    {
        var bottom = curtains.rect.height;
        DOTween.To(() => curtains.rect.yMax, x =>
        {
            curtains.SetTop(x);
            curtains.SetBottom(-x);
        }, bottom, .5f);
    }

    private void Close()
    {
        DOTween.To(() => curtains.rect.yMax, x =>
        {
            curtains.SetTop(x);
            curtains.SetBottom(-x);
        }, 0, .5f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoScale : MonoBehaviour
{
    public Vector3 scale;
    public float duration;
    public int loop;
    public LoopType loopType;
    public Ease ease;

    private Tween tween;
    private void OnEnable()
    {
        tween = transform.DOScale(scale, duration).SetLoops(loop, loopType).SetEase(ease);
    }

    private void OnDisable()
    {
        tween.Kill();
    }
}

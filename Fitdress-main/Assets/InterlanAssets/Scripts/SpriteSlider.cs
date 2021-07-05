using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SpriteSlider : MonoBehaviour
    {
        public Transform source;
        [Min(0)]
        public float startScale = 0;
        [Min(0)]
        public float endScale = 1;

        [Range(0,1f)]
        public float value;
        
        public void SetValue(float time)
        {
            var scale = source.localScale;
            source.DOKill();
            source.DOScaleX(Mathf.Lerp(startScale, endScale, time), 0.25f);
            source.localScale = scale;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (source)
            {
                SetValue(value);
            }
        }
#endif
    }
}
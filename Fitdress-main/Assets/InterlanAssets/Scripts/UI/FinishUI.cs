using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    public class FinishUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        public RectTransform panel;

        public Button button;
        
        public static FinishUI Create()
        {
            var effect = Instantiate(GameAssets.i.finishUi);
            
            return effect;
        }

        private void Start()
        {
            button.onClick.AddListener(GameController.Instance.Restart);
        }

        private void OnEnable()
        {
            canvasGroup.DOFade(1, 1f);
            panel.transform.DOScale(1, 1f);
        }
        
    }
}
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class LevelUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public Text level;
        private void Start()
        {
            level.text = LevelController.LevelName;
            
            GameEvents.onGameStateChange += GameEventsOnGameStateChange;
        }

        private void OnDisable()
        {
            GameEvents.onGameStateChange -= GameEventsOnGameStateChange;
        }

        private void GameEventsOnGameStateChange(GameState state)
        {
            if (state == GameState.Play)
            {
                Fade();
            }
        }

        private void Fade()
        {
            canvasGroup.GetComponent<RectTransform>().DOAnchorPosY(0, 0.75f);
            canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
            {
                canvasGroup.gameObject.SetActive(false);
            });
        }
    }
}
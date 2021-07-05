using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SettingsItemFadeUI : MonoBehaviour
    {
        public SettingsType settingsType;
        public CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup.alpha = Settings.GetStatus(settingsType) ? 1 : 0.25f;
        }

        private void UpdateImage()
        {
            if (Settings.GetStatus(settingsType))
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void Change()
        {
            Settings.Change(settingsType);
            
            UpdateImage();
        }

        private void Show()
        {
            canvasGroup.DOFade(1, 0.5f);
        }

        private void Hide()
        {
            canvasGroup.DOFade(0.25f, 0.5f);
        }
    }
}
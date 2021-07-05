using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class DefeatUI : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        public RectTransform panel;

        public Button button;
        
        public static DefeatUI Create()
        {
            var effect = Instantiate(GameAssets.i.defeatedUi);
            
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
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class DropDown : MonoBehaviour
    {
        public RectTransform source;
        
        public float startHeight;
        public float endHeight;
        
        public CanvasGroup holder;
        public CanvasGroup[] childrens;

        private bool toggle;
        public void Toggle()
        {
            toggle = !toggle;

            if (toggle)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        public void Open()
        {
            source.DOKill();
            source.DOSizeDelta(new Vector2(source.sizeDelta.x, endHeight), 0.25f).OnComplete(() =>
            {
                for (int i = 0; i < childrens.Length; i++)
                {
                    childrens[i].gameObject.SetActive(true);
                }
                
                holder.DOKill();
                holder.DOFade(1, 0.25f);
            });
        }

        public void Close()
        {
            holder.DOKill();
            holder.DOFade(0, 0.25f).OnComplete(() =>
            {
                for (int i = 0; i < childrens.Length; i++)
                {
                    var obj = childrens[i].gameObject;
                    obj.SetActive(false);
                }
            });
            
            source.DOKill();
            source.DOSizeDelta(new Vector2(source.sizeDelta.x, startHeight), 0.25f).SetDelay(0.25f);
        }
    }
}
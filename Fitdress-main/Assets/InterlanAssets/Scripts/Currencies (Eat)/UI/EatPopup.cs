using System;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EatPopup : MonoBehaviour
    {
        public Text text;
        public float fadeDuration;
        public float speed;

        public static EatPopup Create(Vector3 worldPos)
        {
            var anchorPos = Camera.main.WorldToScreenPoint(worldPos);
            var popup = Instantiate(GameAssets.i.eatPopup, anchorPos, Quaternion.identity, GameAssets.i.parent);

            return popup;
        }
        
        private void Start()
        {
            text.transform.DOPunchScale(Vector3.one * 0.25f, 0.5f);
            text.DOFade(0, fadeDuration).SetDelay(0.5f).OnComplete(() => Destroy(gameObject));
        }

        private void Update()
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
}
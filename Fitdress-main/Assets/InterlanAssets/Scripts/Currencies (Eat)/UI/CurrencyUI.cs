using System;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CurrencyUI : MonoBehaviour
    {
        public Currency.Type type;
        public Transform parent;
        public Text currencyText;
        public ParticleSystem partSys;
        
        private void Awake()
        {
            RegisterEvents();

            UpdateCurrency(type);
        }

        private void OnDisable()
        {
            UnregisterEvents();
        }
        
        #region RegisterEvents

        private void RegisterEvents()
        {
            Currency.onCurrencyChange += OnCurrencyChange;
        }

        private void UnregisterEvents()
        {
            Currency.onCurrencyChange -= OnCurrencyChange;
        }

        #endregion
        
        private void OnCurrencyChange(Currency.Type type, Currency.ChangeType changeType)
        {
            if (changeType == Currency.ChangeType.Add)
            {
                if (this.type == type)
                {
                    partSys.Play();
                    parent.localScale = Vector3.one;
                    parent.DOKill();
                    parent.DOPunchScale(Vector3.one * 0.5f, 0.33f);
                }
            }
            
            if (this.type == type)
            {
                UpdateCurrency(type);
            }
        }
        
        private void UpdateCurrency(Currency.Type type)
        {
            switch (type)
            {
                case Currency.Type.Crystals:
                    currencyText.text = Currency.Crystals.ToString();
                    break;
                case Currency.Type.Food:
                    currencyText.text = Currency.Humans.ToString();
                    break;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using MoreMountains.NiceVibrations;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using GameAnalyticsSDK;

namespace Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        public static PlayerController main;
        public event Action onExtraPoundChange; 
        
        [SerializeField] private EatTrigger eatTrigger;

        public BodySize size;
        public GameObject sliderHolder;
        public TMP_Text sizeText;
        public SpriteSlider sizeSlider;
        public SkinnedMeshRenderer[] skinnedMeshRenderers;

        public ClothController clothController;
        public PlayerMovement playerMovement;
        public Animator animator;
        
        private const int maxExtraPounds = 60;
        private const int minExtraPounds = 0;
        private int _extraPounds = minExtraPounds;
        public int ExtraPounds => _extraPounds;
        
        private Coroutine[] fatCoroutines;

        private bool _invincible;
        
        private void Awake()
        {
            GameAnalytics.Initialize();
            main = this;
            
            RegisterEvents();

            fatCoroutines = new Coroutine[skinnedMeshRenderers.Length];
            sizeText.text = size.ToString();
        }

        private void OnDisable()
        {
            UnregisterEvents();
        }
        
        #region RegisterEvents

        private void RegisterEvents()
        {
            GameEvents.onGameStateChange += OnGameStateChange;
            eatTrigger.onEat += OnEat;
        }

        private void UnregisterEvents()
        {
            GameEvents.onGameStateChange -= OnGameStateChange;
        }

        #endregion

        private void OnEat(IFood food)
        {
            if (food.type == Currency.Type.Food)
            {
                if (food.eatObject.GetComponent<Food>())
                {
                    GainExtraPounds(4);

                    EatEffect.Create(food.eatObject.transform.position);
                    
                    AudioController.PlayAction(AudioAction.PickUp);
                    
                    MMVibrationManager.Haptic (HapticTypes.RigidImpact, true, this);
                }
            }
        }
        
        private void OnGameStateChange(GameState state)
        {
            if (state == GameState.Play)
            {
                playerMovement.StartMove();
            }

            if (state == GameState.Finish)
            {
                sliderHolder.transform.DOLocalMoveZ(-3, 1f).OnComplete(() =>
                {
                    sliderHolder.SetActive(false);
                });
            }
        }
        
        private void GainExtraPounds(int amount)
        {
            _extraPounds += amount;
            if (_extraPounds >= maxExtraPounds)
            {
                _extraPounds = maxExtraPounds;
            }

            RecountPounds();
            
            onExtraPoundChange?.Invoke();
        }

        public void LoseExtraPounds(int amount)
        {
            _extraPounds -= amount;
            if (_extraPounds < minExtraPounds)
            {
                _extraPounds = minExtraPounds;
            }
            
            RecountPounds();

            onExtraPoundChange?.Invoke();
        }

        private void RecountPounds()
        {
            var newSize = GetBodySize(_extraPounds);
            if (newSize != size)
            {
                sizeText.transform.localScale = Vector3.one;
                sizeText.transform.DOKill();
                sizeText.transform.DOPunchScale(Vector3.one * 0.35f, 0.5f);
                
                MMVibrationManager.Haptic(HapticTypes.MediumImpact, true, this);
            }

            size = newSize;
            sizeSlider.SetValue((float) (_extraPounds - Convert.ToInt32(size)) / 10);
            sizeText.text = size.ToString();

            ChangeFat(size);
        }

        public BodySize GetBodySize(int extraPounds)
        {
            var result = BodySize.XXSmall;
            if (extraPounds > Convert.ToInt32(BodySize.XLarge))
            {
                result = BodySize.XLarge;
            }
            else if (extraPounds > Convert.ToInt32(BodySize.Large))
            {
                result = BodySize.Large;
            }
            else if (extraPounds > Convert.ToInt32(BodySize.Medium))
            {
                result = BodySize.Medium;
            }
            else if (extraPounds > Convert.ToInt32(BodySize.Small))
            {
                result = BodySize.Small;
            }
            else if (extraPounds > Convert.ToInt32(BodySize.XSmall))
            {
                result = BodySize.XSmall;
            }

            return result;
        }

        private void ChangeFat(BodySize value)
        {
            switch (value)
            {
                case BodySize.XXSmall:
                    Fat(0);
                    animator.SetFloat("Fat", 0f);
                    break;
                case BodySize.XSmall:
                    Fat(5);
                    animator.SetFloat("Fat", 0f);
                    break;
                case BodySize.Small:
                    Fat(10);
                    animator.SetFloat("Fat", 0f);
                    break;
                case BodySize.Medium:
                    Fat(30);
                    animator.SetFloat("Fat", 0f);
                    break;
                case BodySize.Large:
                    Fat(60);
                    animator.SetFloat("Fat", 0f);
                    break;
                case BodySize.XLarge:
                    Fat(100);
                    animator.SetFloat("Fat", 1f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Fat(float value)
        {
            for (int i = 0; i < fatCoroutines.Length; i++)
            {
                if (fatCoroutines[i] != null)
                {
                    StopCoroutine(fatCoroutines[i]);
                }
            }
            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                fatCoroutines[i] = StartCoroutine(FatIE(skinnedMeshRenderers[i], value));
            }
        }

        private IEnumerator FatIE(SkinnedMeshRenderer skinnedMeshRenderer, float value)
        {
            var startValue = skinnedMeshRenderer.GetBlendShapeWeight(0);
            for (float t = 0; t < 1; t += 6 * Time.deltaTime)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(startValue, value, t));
                yield return null;
            }
            skinnedMeshRenderer.SetBlendShapeWeight(0, value);
        }

        #region Damage

        private void Die()
        {
            GameEvents.ChangeGameState(GameState.Fail);
            playerMovement.StopMove();
            GameAnalytics.NewProgressionEvent (GAProgressionStatus.Fail, ("Level " + LevelController.CurrentLevel.ToString()));
        }
        
        void IDamageable.Damage(Transform aTransform)
        {
            if (!_invincible)
            {
                Die();
            }
            else
            {
                Destroy(aTransform.gameObject);
            }
        }

        #endregion
    }

    public enum BodySize
    {
        XXSmall = 0, 
        XSmall = 10, 
        Small = 20,
        Medium = 30, 
        Large = 40,
        XLarge = 50 
    }
}


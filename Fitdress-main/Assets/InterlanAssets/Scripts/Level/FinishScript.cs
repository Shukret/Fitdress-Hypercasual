using System;
using System.Collections;
using Cinemachine;
using DG.Tweening;
using Game.UI;
using MoreMountains.NiceVibrations;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class FinishScript : MonoBehaviour
    {
        public event Action<FinishScript, PlayerController> onPassed;

        public Animator male;
        public ParticleSystem effect;
        public CinemachineVirtualCamera defeatCV;
        public CinemachineVirtualCamera danceCV;

        public Renderer clothRenderer;
        
        [SerializeField] private Transform stay; 
        [SerializeField] private Transform dance; 
        [SerializeField] private Transform cloth;

        private bool _init;

        public void Init()
        {
            _init = true;
        }

        private PlayerController _playerController;
        private bool _levelCompleted;
        public void Init(PlayerController playerController)
        {
            GameEvents.ChangeGameState(GameState.Finish);
            
            if (_init)
            {
                _playerController = playerController;
                _playerController.playerMovement.moveTarget = stay;
                _playerController.playerMovement.onComplete += OnClothPlace;

                if (_playerController.size == LevelController.CurrentLevel.targetBodySize)
                {
                    _levelCompleted = true;

                    LevelController.OpenNextLevel();
                }
            }
            else
            {
                Debug.LogError("FinishScript not initialized!");
            }
        }

        private void OnClothPlace()
        {
            _playerController.playerMovement.onComplete -= OnClothPlace;
            _playerController.GetComponent<PlayerAnimationController>().RunToStay();

            if (_levelCompleted)
            {
                cloth.DORotate(Vector3.zero, .75f).SetDelay(.25f);
                cloth.DOMove(_playerController.clothController.startCloth.position, .75f).SetDelay(.25f).OnComplete(() =>
                {
                    _playerController.clothController.finishCloth.gameObject.SetActive(true);
                    _playerController.clothController.startCloth.gameObject.SetActive(false);

                    StarsEffect.Create(_playerController.clothController.finishCloth.position);
                    
                    cloth.gameObject.SetActive(false);
                    _playerController.playerMovement.moveTarget = dance;
                    _playerController.GetComponent<PlayerAnimationController>().Walk();
                    _playerController.playerMovement.onComplete += OnDancePlace;
                    
                    danceCV.Priority = 100;
                    
                    MMVibrationManager.Haptic (HapticTypes.RigidImpact, true, this);
                });
                
                MMVibrationManager.Haptic (HapticTypes.Success, true, this);
                AudioController.PlayAction(AudioAction.Finish);
                
                GameEvents.Win();
            }
            else
            {
                defeatCV.Priority = 100;
                male.SetTrigger("Defeat");
                _playerController.GetComponent<PlayerAnimationController>().Defeat();
                
                MMVibrationManager.Haptic (HapticTypes.Failure, true, this);
                AudioController.PlayAction(AudioAction.Fail);

                GameEvents.Fail();
                
                DefeatUI.Create();
            }
        }
        
        private void OnDancePlace()
        {
            _playerController.playerMovement.onComplete -= OnDancePlace;
            _playerController.GetComponent<PlayerAnimationController>().Dance();
            
            male.SetTrigger("Claps");
            effect.Play();
            
            FinishUI.Create();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_init)
            {
                if (other.CompareTag("Player"))
                {
                    var playerController = other.GetComponentInParent<PlayerController>();
                    
                    onPassed?.Invoke(this, playerController);
                }
            }
        }
    }
}

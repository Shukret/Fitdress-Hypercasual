using System;
using System.Collections;
using DG.Tweening;
using Player;
using TMPro;
using UnityEngine;

namespace Game
{
    public class Treadmill : MonoBehaviour
    {
        public Trigger trigger;

        [Min(1)]
        public int loseExtraPounds;
        [Min(1)]
        public int loseCount;
        
        public Transform trainTransform;
        public TMP_Text text;
        
        private PlayerController playerController;
        private void Start()
        {
            playerController = PlayerController.main;

            trigger.onTriggerEnter += TriggerOnTriggerEnter;
            playerController.onExtraPoundChange += PlayerControllerOnExtraPoundChange;
        }

        private void PlayerControllerOnExtraPoundChange()
        {
            var pounds = playerController.ExtraPounds;
            pounds -= loseExtraPounds * loseCount;

            text.text = playerController.GetBodySize(pounds).ToString();
        }

        private Transform startTarget;
        private void TriggerOnTriggerEnter(Collider other)
        {
            trigger.onTriggerEnter -= TriggerOnTriggerEnter;
            playerController.onExtraPoundChange -= PlayerControllerOnExtraPoundChange;
            
            playerController = other.GetComponentInParent<PlayerController>();

            startTarget = playerController.playerMovement.moveTarget;
            
            playerController.playerMovement.moveTarget = trainTransform;
            playerController.playerMovement.onComplete += OnTrack;
        }

        private void OnTrack()
        {
            playerController.playerMovement.onComplete -= OnTrack;
            playerController.GetComponent<PlayerAnimationController>().FastRun();

            StartCoroutine(TrainIE());
        }

        private IEnumerator TrainIE()
        {
            var delaySeconds = 2f / loseCount;
            var delay = new WaitForSeconds(delaySeconds);

            for (var t = 0; t < loseCount; t++)
            {
                if (playerController.ExtraPounds == 0)
                {
                    break;
                }
                
                playerController.LoseExtraPounds(loseExtraPounds);
 
                yield return delay;
            }
            
            Done();
        }

        private void Done()
        {
            playerController.GetComponent<PlayerAnimationController>().SlowRun();
            playerController.playerMovement.moveTarget = startTarget;

            Destroy(gameObject);
        }
    }
}
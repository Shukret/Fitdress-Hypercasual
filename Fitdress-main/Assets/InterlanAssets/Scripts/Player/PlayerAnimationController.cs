using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public Animator animator;
        
        private void Start()
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            //playerMovement.onStart += StayToRun;
            //playerMovement.onComplete += RunToStay;
        }
        
        public void FastRun()
        {
            animator.SetTrigger("FastRun");
        }
        
        public void SlowRun()
        {
            animator.SetTrigger("SlowRun");
            playerMovement.speed = 5f;
        }
        
        public void Dance()
        {
            animator.SetTrigger("Dance");
        }
        
        public void Defeat()
        {
            animator.SetTrigger("Defeat");
        }

        public void Walk()
        {
            animator.SetTrigger("Walk");
            playerMovement.speed = 5f;
        }
        
        public void RunToStay()
        {
            animator.SetTrigger("RunToStay");
        }
    }
}
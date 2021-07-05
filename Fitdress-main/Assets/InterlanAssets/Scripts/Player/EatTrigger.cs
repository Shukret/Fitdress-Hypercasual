using System;
using DG.Tweening;
using Game;
using MoreMountains.NiceVibrations;
using UI;
using UnityEngine;

namespace Player
{
    public class EatTrigger : MonoBehaviour
    {
        public Action<IFood> onEat;
        public bool eatAll;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Eat") || eatAll && other.CompareTag("MaybeEat"))
            {
                Eat(other);
            }
        }

        private void Eat(Collider other)
        {
            var eat = other.GetComponent<IFood>();
            if (eat != null)
            {
                onEat?.Invoke(eat);
            }
            
            MMVibrationManager.Haptic (HapticTypes.RigidImpact, true, this);
            Destroy(other.gameObject);
        }
    }
}
using System;
using System.Collections;
using DG.Tweening;
using Game;
using UnityEngine;

namespace Player
{
    public class VacuumTrigger : MonoBehaviour
    {
        public Transform end;
        public float smoothTime;
        
        public bool vacuumAll;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Eat") || vacuumAll && other.CompareTag("MaybeEat"))
            {
                StartCoroutine(VacuumIE(other));
            }
        }

        private IEnumerator VacuumIE(Collider other)
        {
            var eat = other.gameObject;
            var velocity = Vector3.zero;
            while (eat != null && eat.transform.position.z - end.position.z > 0)
            {
                var position = eat.transform.position;
                var newPos = Vector3.SmoothDamp(position, end.position, ref velocity, smoothTime);
                
                eat.transform.position = newPos;

                yield return null;
            }
        }
    }
}
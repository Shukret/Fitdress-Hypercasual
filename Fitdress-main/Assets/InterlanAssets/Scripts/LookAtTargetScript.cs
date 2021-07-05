using System;
using UnityEngine;

namespace Game
{
    public class LookAtTargetScript : MonoBehaviour
    {
        public Transform target;

        public float rotationSpeed;
        
        private void Update()
        {
            if (target)
            {
                LookAt(target);
            }
        }
        
        private void LookAt(Transform target)
        {
            Quaternion lookOnLook =
                Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * rotationSpeed);
        }
    }
}
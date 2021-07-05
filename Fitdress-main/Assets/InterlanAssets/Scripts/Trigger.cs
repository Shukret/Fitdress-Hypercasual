using System;
using UnityEngine;

namespace Game
{
    public class Trigger : MonoBehaviour
    {
        public event Action<Collider> onTriggerEnter;
        
        public string targetTag;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(targetTag))
            {
                onTriggerEnter?.Invoke(other);
            }
        }
    }
}
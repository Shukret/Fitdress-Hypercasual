using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Food : MonoBehaviour, IFood
    {
        public Currency.Type type => Currency.Type.Food;
        public GameObject eatObject => gameObject;

        public Vector3 scale;
        
        public void Init(GameObject prefab)
        {
            //var randomIndex = Random.Range(0, prefabs.Length - 1);
            var food = Instantiate(prefab, transform);
            food.transform.localScale += scale;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
#endif

    }
}
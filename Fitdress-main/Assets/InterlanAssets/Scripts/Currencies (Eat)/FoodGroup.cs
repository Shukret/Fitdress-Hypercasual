using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public abstract class FoodGroup : MonoBehaviour
    {
        private void Start()
        {
            Init();
        }

        protected abstract void Init();
    }
}
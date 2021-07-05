using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Help
{
    public class TargetFPS : MonoBehaviour
    {
        public int targetFrameRate;
        private void Start()
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}

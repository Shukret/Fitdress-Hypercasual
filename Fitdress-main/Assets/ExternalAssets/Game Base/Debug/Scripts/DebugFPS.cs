using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyDebug
{
    public class DebugFPS : MonoBehaviour
    {
        public Text Text;

        private void Update()
        {
            Text.text = $"FPS: {Mathf.Round(1.0f / Time.deltaTime)}";
        }
    }
}
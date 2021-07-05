using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera startCamera;
        [SerializeField] private CinemachineVirtualCamera mainCamera;
        
        private void Awake()
        {
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnregisterEvents();
        }
        
        #region RegisterEvents

        private void RegisterEvents()
        {
            GameEvents.onCameraStateChange += OnCameraStateChange;
            GameEvents.onGameStateChange += OnGameStateChange;
        }

        private void UnregisterEvents()
        {
            GameEvents.onCameraStateChange -= OnCameraStateChange;
            GameEvents.onGameStateChange -= OnGameStateChange;
        }

        #endregion
        
        private void OnGameStateChange(GameState state)
        {
            if (state == GameState.Play)
            {
                GameEvents.ChangeCameraState(CameraState.Play);
            }
        }
        
        private void OnCameraStateChange(CameraState state)
        {
            if (state == CameraState.Play)
            {
                mainCamera.Priority = 10;
            }
        }
    }
}


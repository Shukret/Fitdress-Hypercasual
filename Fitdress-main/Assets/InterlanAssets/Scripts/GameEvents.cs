using UnityEngine;

namespace Game
{
    public static class GameEvents
    {
        public delegate void OnGameStateChange(GameState state);
        public static event OnGameStateChange onGameStateChange;

        public delegate void OnCameraStateChange(CameraState state);
        public static event OnCameraStateChange onCameraStateChange;
        
        public delegate void OnWin();
        public static event OnWin onWin;
        
        public delegate void OnFail();
        public static event OnFail onFail;
        
        private static GameState gameState;
        private static CameraState cameraState;
        
        public static void ChangeGameState(GameState state)
        {
            if (gameState != state || gameState == GameState.Start)
            {
                gameState = state;
                onGameStateChange?.Invoke(state);
            }
        }
        
        public static void ChangeCameraState(CameraState state)
        {
            cameraState = state;
            onCameraStateChange?.Invoke(state);
        }

        public static void Win()
        {
            onWin?.Invoke();
        }
        
        public static void Fail()
        {
            onFail?.Invoke();
        }
    }
}
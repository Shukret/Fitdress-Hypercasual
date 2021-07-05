using System;
using System.Collections;
using System.Threading.Tasks;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        
        [FormerlySerializedAs("snake")] public Transform player;
        public Transform startText;
        public Vector3 offsetStartText;
        
        private void Awake()
        {
            Instance = this;
            
            RegisterEvents();
            Currency.Init();
        }

        private void Start()
        {
            player.position = Vector3.zero;
            startText.position = Vector3.zero + offsetStartText;
            
            GameEvents.ChangeGameState(GameState.Start);
        }

        private void OnDisable()
        {
            UnregisterEvents();
        }
        
        #region RegisterEvents

        private void RegisterEvents()
        {
            GameEvents.onGameStateChange += OnGameStateChange;
        }

        private void UnregisterEvents()
        {
            GameEvents.onGameStateChange -= OnGameStateChange;
        }

        #endregion

        private void OnGameStateChange(GameState state)
        {
            if (state == GameState.Fail)
            {
                StartCoroutine(LoadSceneIE());
            }
            else if (state == GameState.Finish)
            {
                Currency.Save();
            }
        }

        private IEnumerator LoadSceneIE()
        {
            yield return new WaitForSeconds(1f);
            Restart();
        }

        public void Restart()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
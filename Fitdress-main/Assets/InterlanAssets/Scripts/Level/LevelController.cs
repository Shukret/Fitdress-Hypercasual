using System;
using BayatGames.SaveGameFree;
using UnityEngine;
using GameAnalyticsSDK;

namespace Game
{
    public class LevelController : MonoBehaviour
    {
        public LevelContainer levelContainer;
        public Transform levelParent;

        public static Level CurrentLevel { get; private set; }
        public static string LevelName { get; private set; }

        private static LevelController Instance;
        private static int currentLevelIndex = 0;
        
        const string saveKey = "level";
        private void Awake()
        {
            if (SaveGame.Exists(saveKey))
            {
                currentLevelIndex = SaveGame.Load<int>(saveKey, SaveGamePath.PersistentDataPath);
            }

            var index = currentLevelIndex;

            if (index < 0)
            {
                index = 0;
            }

            if (index > levelContainer.Levels.Length - 1)
            {
                index = 0;
            }
            
            Load(index);
            GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, ("Level " + (index).ToString()));
            Instance = this;
        }

        private void Load(int index)
        {
            if (CurrentLevel)
            {
                Destroy(CurrentLevel.gameObject);
            }

            CurrentLevel = Instantiate(levelContainer.Levels[index], levelParent);

            LevelName = levelContainer.Levels[index].name;
        }

        public static void OpenNextLevel()
        {
            GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, ("Level " + currentLevelIndex.ToString()));
            var index = currentLevelIndex + 1;
            
            if (index < 0)
            {
                index = 0;
            }

            if (index > Instance.levelContainer.Levels.Length - 1)
            {
                index = 0;
            }
            
            SaveGame.Save<int>(saveKey, index, SaveGamePath.PersistentDataPath);
        }
    }
}
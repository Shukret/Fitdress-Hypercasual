using Game;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    public class EditorCheats : MonoBehaviour
    {
        [MenuItem("Cheats/Load next level")]
        public static void LoadNextLevel()
        {
            LevelController.OpenNextLevel();
            GameController.Instance.Restart();
        }
        
        [MenuItem("Cheats/Restart")]
        public static void Restart()
        {
            GameController.Instance.Restart();
        }
    }
}
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LevelContainer", menuName = "LevelContainer", order = 0)]
    public class LevelContainer : ScriptableObject
    {
        [SerializeField] private Level[] levels;

        public Level[] Levels => levels;
    }
}
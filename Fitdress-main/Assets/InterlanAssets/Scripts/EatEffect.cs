using Game;
using UnityEngine;

namespace Game
{
    public class EatEffect : MonoBehaviour
    {
        public static GameObject Create(Vector3 worldPos)
        {
            var effect = Instantiate(GameAssets.i.eatEffect, worldPos, Quaternion.identity);

            return effect;
        }
    }
    
    public class StarsEffect : MonoBehaviour
    {
        public static GameObject Create(Vector3 worldPos)
        {
            var effect = Instantiate(GameAssets.i.starsEffect, worldPos, Quaternion.identity);

            return effect;
        }
    }
}
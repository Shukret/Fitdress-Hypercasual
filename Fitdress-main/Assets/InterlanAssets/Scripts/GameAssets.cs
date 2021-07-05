using System;
using Game.UI;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class GameAssets : MonoBehaviour
    {
        public static GameAssets i;
        public Transform parent;

        [Header("UI")]
        public EatPopup eatPopup;
        public DefeatUI defeatedUi;
        public FinishUI finishUi;
        [Header("Particle System")]
        public GameObject eatEffect;
        public GameObject starsEffect;
        
        public void Awake()
        {
            Destroy(i);
            
            i = this;
        }
    }
}